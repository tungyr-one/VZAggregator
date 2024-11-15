using api.Controllers.Middleware;
using api.Extensions;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VZAggregator.Data;
using VZAggregator.Models.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var services = builder.Services;

services.AddControllers();
services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
builder.Services.AddRazorPages();

var app = builder.Build();

var config = builder.Configuration;
var env = app.Environment;

if (env.IsDevelopment())
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    // app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHsts();
}

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();


using var scope = app.Services.CreateScope();

try
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(); 
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>(); 
    await context.Database.MigrateAsync();
    await DemoDataSeeder.SeedAsync(context, userManager, roleManager);
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
