using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod() 
    .AllowAnyHeader());  

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("http://localhost:5001/swagger/v1/swagger.json", "Report Service API");
    c.SwaggerEndpoint("http://localhost:5002/swagger/v1/swagger.json", "Survey Service API");
});

await app.UseOcelot();

app.Run();
