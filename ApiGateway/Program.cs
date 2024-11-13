using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
    .WithOrigins("http://localhost:5042")  
    .AllowAnyMethod() 
    .AllowAnyHeader());  

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("http://localhost:5112/swagger/v1/swagger.json", "Survey Service API");
});

await app.UseOcelot();

app.Run();
