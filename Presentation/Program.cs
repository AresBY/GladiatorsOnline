using Gladiators.Business.Services.Implementations;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data;
using Gladiators.Data.Repository.Implementations;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUnityWebGL", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5500",
            "https://gladiators-online.onrender.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5179);
});

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("Using connection string: " + cs);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(cs));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGladiatorService, GladiatorService>();
builder.Services.AddScoped<IMarketSlaveService, MarketSlaveService>();
builder.Services.AddScoped<IPlayerSlaveService, PlayerSlaveService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGladiatorRepository, GladiatorRepository>();
builder.Services.AddScoped<IMarketSlaveRepository, MarketSlaveRepository>();
builder.Services.AddScoped<IPlayerSlaveRepository, PlayerSlaveRepository>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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