using ClinicAPI.Configuration;
using Microsoft.Extensions.Options;
using static ClinicAPI.Configuration.ClinicSettings;
using ClinicAPI.GroupedRegistirations;
using System.Diagnostics;
using ClinicAPI.CustomExceptions;
using System.Text.Json.Serialization;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ======================================== DI (registering services and configuring Dependecies) Goes Here =================================

builder.Services.AddOptions<ClinicSettings>()
    .Bind(builder.Configuration.GetSection(clinicName));

builder.Services.AddControllers()
                .AddJsonOptions(o =>
                            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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

var app = builder.Build();

// ============================================================== MiddleWares Goes here ===================================================

// This will add all Controllers Endpoints to our Route-Table , so Asp.net could pick The controllers' Endpoints and not give 404

app.MapControllers();

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
app.UseExceptionHandler();


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

// Early Block MW
app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    if (path.StartsWithSegments("/admin"))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("403 U can't access this Resource -> skill issue !");
        return;
    }
    else
    {
        await next();
    }
});

// Getting appsettings.json file Configuration (Public Stuff)
app.MapGet("/", (IOptionsSnapshot<ClinicSettings> options) => {
    return options.Value;
});

// Endpoint to get the route-table (which will be having All program endpoints -> i may use it for debugging)
app.MapGet("/route-table", (IServiceProvider sp) =>
{
    var endpoints = sp.GetRequiredService<EndpointDataSource>()
    .Endpoints.Select(sp => sp.DisplayName);

    return Results.Ok(endpoints);
});

app.MapGet("/__whoami", () => new {
    App = AppDomain.CurrentDomain.FriendlyName,
    Dir = AppContext.BaseDirectory
});


app.Run();

