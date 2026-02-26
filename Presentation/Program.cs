using Gladiators.Business.Services.Implementations;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data;
using Gladiators.Data.Repository.Implementations;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // или конкретный фронтенд: "http://localhost:4200"
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// --- јвтоматическое применение миграций при старте ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // <--- здесь автомиграци€
}
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gladiator API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();