using ClinicAPI.Configuration;
using Microsoft.Extensions.Options;
using ClinicAPI.Service;
using ClinicAPI.Presistence;
using ClinicAPI.Repositories;
using static ClinicAPI.Configuration.ClinicSettings;
using Microsoft.EntityFrameworkCore;
using ClinicAPI.GroupedRegistirations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<ClinicSettings>()
    .Bind(builder.Configuration.GetSection(clinicName));


//Service/Business Layer Services
builder.Services.AddBusinessServices();

// infrastructure Layer Services
builder.Services.AddInfrastructureServices();

var app = builder.Build();

app.Map("/", (IOptionsSnapshot<ClinicSettings> options) => {
    // note that the options . value is the instance of the ClinicSettings class
    // so i could reach any attribute easily , and there is no wrong-typed stuff

    return options.Value; // returning the whole instane of ClinicSettings as JSON
});



app.Run(async context =>
{
    context.Response.Headers["mero"] = "1";
});
