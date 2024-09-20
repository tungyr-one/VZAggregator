using System.Net;
using Microsoft.EntityFrameworkCore;
using VZAggregator.Data;
using VZAggregator.Entities.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container.

var services = builder.Services;

services.AddControllers();

services.AddApplicationServices(builder.Configuration);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// conventional
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// app.Map("/home") =>

// app.UseExceptionHandler("/Home/Error");

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/hello", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});


// app.UseDeveloperExceptionPage();
// app.UseSession();

using var scope = app.Services.CreateScope();

try
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await DemoDataSeeder.SeedAsync(context);
}
catch(Exception ex)
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}
finally
{
    app.Run();
}
