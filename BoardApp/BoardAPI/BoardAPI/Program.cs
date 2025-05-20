using System.Reflection;
using BoardAPI.Repositories;
using BoardAPI.Repositories.Interfaces;
using BoardAPI.Services;
using BoardAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Billboard API",
        Version = "v1",
        Description = "The Billboard API for managing announcements",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddTransient<IAnnouncementsRepository, AnnouncementsRepository>();
builder.Services.AddTransient<IAnnouncementsService, AnnouncementsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billboard API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseExceptionHandler("/api/error");
app.UseStatusCodePagesWithReExecute("/api/error/{0}");

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();