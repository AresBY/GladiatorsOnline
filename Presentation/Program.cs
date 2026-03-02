using Gladiators.Business.Factories;
using Gladiators.Business.Providers;
using Gladiators.Business.Services.Implementations;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Implementations;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ---------- CORS ----------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUnityWebGL", policy =>
    {
        policy
            .AllowAnyOrigin()   // временно для теста
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ---------- Порт для Kestrel ----------
var port = Environment.GetEnvironmentVariable("PORT") ?? "5179";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

// ---------- Конфигурация ----------
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ---------- Connection string ----------
string cs;

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    // Продакшн / Render
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var username = userInfo[0];
    var password = userInfo[1];

    cs = new Npgsql.NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = username,
        Password = password,
        SslMode = Npgsql.SslMode.Require
    }.ToString();

    Console.WriteLine("Using production DATABASE_URL: " + cs);
}
else
{
    // Локальная разработка
    cs = builder.Configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine("Using local DefaultConnection: " + cs);
}

// ---------- DbContext ----------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(cs));

// ---------- DI: Providers ----------
var path = Path.Combine(builder.Environment.ContentRootPath, "Resources", "achievements.json");
path = Path.GetFullPath(path);

var definitions = JsonSerializer.Deserialize<List<AchievementDefinition>>(File.ReadAllText(path)) ?? new List<AchievementDefinition>();
builder.Services.AddSingleton<IReadOnlyList<AchievementDefinition>>(definitions);
builder.Services.AddSingleton<IAchievementConfigProvider>(new AchievementConfigProvider(definitions));
// ---------- DI: Services ----------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGladiatorService, GladiatorService>();
builder.Services.AddScoped<IMarketSlaveService, MarketSlaveService>();
builder.Services.AddScoped<IPlayerSlaveService, PlayerSlaveService>();
builder.Services.AddScoped<ISlaveGenerator, SlaveGenerator>();
builder.Services.AddScoped<IBattleService, BattleService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();


// ---------- DI: Factories ----------
builder.Services.AddScoped<FighterFactory>();

// ---------- DI: Repositories ----------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGladiatorRepository, GladiatorRepository>();
builder.Services.AddScoped<IMarketSlaveRepository, MarketSlaveRepository>();
builder.Services.AddScoped<IPlayerSlaveRepository, PlayerSlaveRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();



// ---------- Controllers & Swagger ----------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- Автомиграции ----------
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate(); // автомиграция
        Console.WriteLine("DB connected successfully");
    }
}
catch (Exception ex)
{
    Console.WriteLine("DB connection error: " + ex.Message);
}

// ---------- Middleware ----------
app.UseCors("AllowUnityWebGL");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gladiator API V1");
    c.RoutePrefix = "swagger";
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();