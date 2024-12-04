using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors();
var corsPolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

app.UseCors(corsPolicy);  

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("http://localhost:5001/swagger/v1/swagger.json", "Report Service API");
    c.SwaggerEndpoint("http://localhost:5002/swagger/v1/swagger.json", "Survey Service API");
    c.SwaggerEndpoint("http://localhost:5003/swagger/v1/swagger.json", "Billing Service API");
    c.SwaggerEndpoint("http://localhost:5004/swagger/v1/swagger.json", "Identity Service API");
    c.SwaggerEndpoint("http://localhost:5005/swagger/v1/swagger.json", "Apartment Service API");
});

await app.UseOcelot();

app.Run();
