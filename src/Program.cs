using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;
using RinhAPI.Db;
using RinhAPI.Interfaces;
using RinhAPI.Repositories;
using RinhAPI.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<RinhaDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("RinhaDb")),
    poolSize: 100);

builder.Services.AddRequestTimeouts(opt => opt.DefaultPolicy = new RequestTimeoutPolicy{Timeout = TimeSpan.FromSeconds(60)});
builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ICLienteService, ClienteService>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapGet("/healthcheck", () => "Funfando");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
