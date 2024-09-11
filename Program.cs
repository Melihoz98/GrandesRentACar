using GrandesRentACar.BusinessLogic;
using GrandesRentACar.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using GrandesRentACar.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register your data access services
builder.Services.AddScoped<ICarData, CarDataLogic>();
builder.Services.AddScoped<ICarAccess, CarAccess>();
builder.Services.AddScoped<ICarCopiesData, CarCopiesDataLogic>();
builder.Services.AddScoped<ICarCopiesAccess, CarCopiesAccess>();

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust session timeout as needed
    options.Cookie.HttpOnly = true; // Ensure cookie is only accessible via HTTP
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Configure PayPal settings

// Register other services if needed (e.g., PayPalService)
// builder.Services.AddScoped<PayPalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
