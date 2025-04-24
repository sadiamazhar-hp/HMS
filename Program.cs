using Microsoft.AspNetCore.Authentication.Cookies;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using V._3._0.App_Data;
using V._3._0.Controllers;
using V._3._0.Interfaces;
using V._3._0.Methods;
using V._3._0.Models;
using V._3._0.Repostories;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorPagesOptions(opt => opt.RootDirectory = "/Login/Login"); 

builder.Services.AddMvc();
builder.Services.AddScoped<IPatients, PatientData>();
builder.Services.AddScoped<IUser,UserRepo>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});


//builder.Services.AddScoped<IHosData, HosData>();
builder.Services.AddDbContext<HospitalData>
    (options => options.UseSqlServer("Data Source=DESKTOP-5UE9NQG\\MSSQLSERVERNEW; Database=HMS;User ID=sa;Password=12345;Encrypt=True;Trust Server Certificate=True;Multi Subnet Failover=False"));


// Authenticatin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//    c.RoutePrefix = string.Empty;
//});
// Configure Swagger to be available but not as the default route
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Change route to "swagger"
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages(); // Ensure Razor Pages are mapped

//    // Redirect the root URL ("/") to the Login page
//    endpoints.MapGet("/", async context =>
//    {
//        context.Response.Redirect("/Login/Login");
//    });
//});

//});
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");


app.Run();
