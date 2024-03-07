using HHJobsCalculator.Core.Engine;
using HHJobsCalculator.Engine;
using HHJobsCalculator.WebApi.Middleware;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{   
    options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "HHJobsCalculator.Core.xml"));
    options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"HHJobsCalculator.WebApi.xml"));    
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HH Job Calculator", Version = "v1" });
    options.UseAllOfToExtendReferenceSchemas();
    options.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// add DI for engine
builder.Services.AddSingleton<IJobsCalculator, JobsCalculator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);   
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
