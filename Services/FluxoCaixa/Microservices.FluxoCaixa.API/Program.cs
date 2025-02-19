
using MassTransit;
using Microservices.FluxoCaixa.API.Consumer;
using Serilog;
using Microservices.FluxoCaixa.Infrastructure;
using Microservices.FluxoCaixa.Application;
using Common.Logger;
using Event.Messages.Common;
using Microservices.FluxoCaixa.Infrastructure.Persistence;
using Microservices.FluxoCaixa.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microservices.FluxoCaixa.API.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microservices.FluxoCaixa.API.Configuracao;
using Microservices.FluxoCaixa.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    //options.ListenAnyIP(80); // HTTP port
    options.ListenAnyIP(8080); // Additional HTTP port
});

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfiguration _configuration = new ConfigurationBuilder()
                              .AddJsonFile($"appsettings.json", true)
                              .AddJsonFile($"appsettings.{environmentName}.json", false)
                            .Build();

builder.Configuration
      .AddJsonFile($"appsettings.json", true)
      .AddJsonFile($"appsettings.{environmentName}.json", false)
      .AddEnvironmentVariables()
      .Build(); ;
builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.Configure<Configurations>(_configuration);

var rabbitserver = _configuration.GetValue<string>("EventBusSettings:DbServer") ?? "rabbitmq";
var rabbitport = _configuration.GetValue<string>("EventBusSettings:DbPort") ?? "5672";
var rabbituser = _configuration.GetValue<string>("EventBusSettings:DbUser") ?? "guest";
var rabbitpassword = _configuration.GetValue<string>("EventBusSettings:DbPassword") ?? "guest";
var rabbitConfiguration = $"amqp://{rabbituser}:{rabbitpassword}@{rabbitserver}:{rabbitport}";


builder.Services.AddMassTransit(config => {

    config.AddConsumer<LancamentoEfetuadoConsumer>();
    // MassTransit-RabbitMQ Configuration
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(rabbitConfiguration);
        cfg.ReceiveEndpoint(EventBusConstants.LancamentoContaCorrenteQueue, c => {
            c.ConfigureConsumer<LancamentoEfetuadoConsumer>(ctx);
        });
    });
});

var server = _configuration.GetValue<string>("DatabaseSettings:DbServer") ?? "localhost";
var port = _configuration.GetValue<string>("DatabaseSettings:DbPort") ?? "27017";
var mongoConfiguration = $"mongodb://{server}:{port}";
//HealthChecks
//builder.Services.AddHealthChecks()
//        .AddMongoDb(mongoConfiguration, "MongoDb Health", HealthStatus.Degraded);
//builder.Services.AddHealthChecks()
//        .AddDbContextCheck<FluxoCaixaContext>();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(_configuration);
builder.Services.AddScoped<IAutorizacao, Autorizacao>();

var secretKeyName = _configuration["JwtSecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidIssuer = "localhost:5178",
                ValidAudience = "localhost:5178",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyName)),
            };
        });


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddScoped<LancamentoEfetuadoConsumer>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MigrateDatabase<FluxoCaixaContext>((context, services) =>
{
    var logger = services.GetService<ILogger<FluxoCaixaContextSeed>>();
    FluxoCaixaContextSeed
        .SeedAsync(context, logger)
        .Wait();
});



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        // Fluent API
        options
            .WithTitle("Fluxo de caixa API")
            .WithSidebar(false)
            .WithOAuth2Authentication(oauth =>
            {
                oauth.ClientId = "your-client-id";
                oauth.Scopes = ["profile"];
            });
        options.Servers =
        [
            new ScalarServer("http://[::]:8080")
             
        ];
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecurityScheme = "Bearer"
        };

        // Object initializer
        options.Title = "Fluxo de caixa API";
        options.ShowSidebar = false;
    });

}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseHealthChecks("/health");

app.Run();
