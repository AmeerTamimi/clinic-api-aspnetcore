using ClinicAPI.Configuration;
using Microsoft.Extensions.Options;
using static ClinicAPI.Configuration.ClinicSettings;
using ClinicAPI.GroupedRegistirations;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<ClinicSettings>()
    .Bind(builder.Configuration.GetSection(clinicName));

// DI (registering services and configuring Dependecies) Goes Here ->

// Service/Business Layer Services
builder.Services.AddBusinessServices();

// Infrastructure Layer Services
builder.Services.AddInfrastructureServices();



var app = builder.Build();

// MiddleWares Goes here -> 

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


app.MapGet("/", (IOptionsSnapshot<ClinicSettings> options) => {
    return options.Value;
});


app.Run();

