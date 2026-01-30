using ClinicAPI.Configurations;
using ClinicAPI.Registirations;
using Microsoft.Extensions.Options;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// ======================================== DI (registering services and configuring Dependecies) Goes Here =================================
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// ============================================================== MiddleWares Goes here ===================================================
app.UseExceptionHandler();

// No Http any moooooooooooooooooooooooooooooore
app.UseHttpsRedirection();

// Browser side redirect (or fix lets say)
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

