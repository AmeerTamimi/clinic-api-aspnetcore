using ClinicAPI.Configurations;
using Microsoft.Extensions.Options;
using static ClinicAPI.Configurations.ClinicSettings;
using ClinicAPI.Registirations;
using System.Diagnostics;
using ClinicAPI.CustomExceptions;
using System.Text.Json.Serialization;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ======================================== DI (registering services and configuring Dependecies) Goes Here =================================

// Options Pattern Here 
builder.Services.AddOptions<ClinicSettings>()
    .Bind(builder.Configuration.GetSection(clinicName));

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection(JwtSettings.JwtSettingsName));


builder.Services.AddControllers()
                .AddJsonOptions(o =>
                            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Try Catching the whole pipline From This Guy btw
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Rfc 9457 ,,, like spelling that name lowkey
builder.Services.AddProblemDetails();

// Service/Business Layer Services
builder.Services.AddBusinessServices();

// Infrastructure Layer Services
builder.Services.AddInfrastructureServices();

// Validators Services | Auto Fluent Validation
builder.Services.AddValidatorsServices();

// Adding The Db Context , (Since we injected it on ClinicDbContext/All Repos)
builder.Services.AddDbContext<ClinicDbContext>(options =>
{
    options.UseSqlite("Data Source = clinic.db");
});

// Adding the Authentication Configurations (Jwt as a scheme)
builder.Services.AddJwtAuthentication();

// Adding Authorization Rules
builder.Services.AddJwtAuthorization();

var app = builder.Build();

// ============================================================== MiddleWares Goes here ===================================================

app.UseExceptionHandler();

// This will add all Controllers Endpoints to our Route-Table , so Asp.net could pick The controllers' Endpoints and not give 404
app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

// Logging + Timing MW
app.Use(async (context, next) =>
{
    var method = context.Request.Method;
    var path = context.Request.Path;

    Console.WriteLine($"Method : {method} , Path : {path}");

    var sw = Stopwatch.StartNew();

    try
    {
        await next();
    }
    finally // so if the next() got issues (hopefully it doesnt) , we gurantee the log is safe
    {
        sw.Stop();

        var status = context.Response.StatusCode;
        var time = sw.ElapsedMilliseconds;

        Console.WriteLine($"Result : {status} , Time : {time}ms");
    }
    
});

// Adding headers to Requests MW
app.Use(async (context, next) =>
{

    context.Items["Name"] = "Ameer"; // will be shown to other MWS / Endpoints on the pipline

    context.Response.OnStarting(() =>
    {
        context.Response.Headers["X-Name"] = "Ameer"; // will be shown to the client
        return Task.CompletedTask;
    }); 

    await next();
});

// Getting appsettings.json file Configuration (Public Stuff)
app.MapGet("/", (IOptionsSnapshot<ClinicSettings> options) => {
    return options.Value; // return the whole configurations as a json object
});

// Endpoint to get the route-table (which will be having All program endpoints -> i may use it for debugging)
app.MapGet("/route-table", (IServiceProvider sp) =>
{
    var endpoints = sp.GetRequiredService<EndpointDataSource>()
    .Endpoints.Select(sp => sp.DisplayName);

    return Results.Ok(endpoints);
});

app.Run();

