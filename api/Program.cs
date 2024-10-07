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

// public void ConfigureServices(IServiceCollection services)
// {
//     services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//         .AddCookie(options =>
//         {
//             options.LoginPath = "/Account/Login"; // Путь к странице входа
//             options.LogoutPath = "/Account/Logout"; // Путь к странице выхода
//         });

//     services.AddControllersWithViews();
// }

// public void ConfigureServices(IServiceCollection services)
// {
//     services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//         .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = "your-issuer",
//                 ValidAudience = "your-audience",
//                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secure-key"))
//             };
//         });

//     services.AddControllersWithViews();
// }

//OAuth и сторонние аутентификации

// builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var services = builder.Services;

services.AddControllers();
services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

// app.MapHub<MessageHub>("hubs/message");

// public void ConfigureServices(IServiceCollection services)
// {
//    ...
//     services.AddAuthorization(options =>
//     {
//         options.AddPolicy("ShouldBeAdminOrModarator", policy => 
//         policy.RequireClaim("Adimn")), 
//         policy.RequireClaim("Moderator"));;
//     });
// }


// public void ConfigureServices(IServiceCollection services)
// {
//     services.AddAuthorization(options =>
//     {
//         options.AddPolicy("Over18", policy =>policy.RequireClaim(ClaimTypes.DateOfBirth, dob =>      
//         DateTime.Now.Year - Convert.ToDateTime(dob.Value).Year >= 18));
//     });
// }

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();


// conventional
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

// app.Map("/home") =>

// app.UseExceptionHandler("/Home/Error");

app.Map("/", () => "Index Page");
app.Map("/user", () => Console.WriteLine("Request Path: /user"));
app.Map("/user/handle", UserHandler);

string UserHandler()
{
    return "This is UserHandler";
}

app.Map("/hello/{name?}", async context =>
{
    var name = context.Request.RouteValues["name"]?.ToString() ?? "World";
    await context.Response.WriteAsync($"Hello {name}!");
});

app.Map("/hello/{num1}/{num2}", (int num1, int num2) =>
{
    int result = num1 * num2;
    return Results.Ok(result);
});

//RAZOR

app.Map("/request/{name}/{age}", async (context) =>
{
    string name = context.Request.RouteValues["name"]?.ToString() ?? "Unknown";
    if (!int.TryParse(context.Request.RouteValues["age"]?.ToString(), out int age))
    {
        await context.Response.WriteAsync("Invalid age provided.");
        return;
    }
    
    // Pass the name via query string or other means to the Razor page
    var url = $"/Hello?name={name}&age={age}";  // Assuming Hello.cshtml is the Razor page
    context.Response.Redirect(url);
});


// app.UseDeveloperExceptionPage();
// app.UseSession();

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
