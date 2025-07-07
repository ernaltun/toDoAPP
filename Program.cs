using Microsoft.EntityFrameworkCore;
using toDoAPP.Interface;
using Microsoft.AspNetCore.Mvc;
using toDoAPP.Models;
using toDoAPP.Repository;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.File("logs/logDoc.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Services.AddRazorPages();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddDbContext<DataContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")));
builder.Services.AddTransient<ITaskInterface, TaskRepository>();
builder.Services.AddTransient<IUserInterface, UserRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/User/Login";
	options.LogoutPath = "/User/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
});
var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
