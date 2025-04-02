using DataAccess.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add session support
builder.Services.AddSession();

// Add Identity for user authentication
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();  // Ensure token providers are added for features like email confirmation

// Add authorization policies
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
// Add MVC services
builder.Services.AddControllersWithViews();

// Add Razor Pages support
builder.Services.AddRazorPages();

// Add your services (like UnitOfWork and CartService)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configure the database context with the connection string
builder.Services.AddDbContext<ApplicationDbContext>(items =>
{
    items.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"));
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // HTTP Strict Transport Security for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();  // Enable session middleware
app.UseRouting();

app.UseAuthentication();
// Ensure that authorization is used only once
app.UseAuthorization();

// Map Razor Pages and MVC routes
app.MapRazorPages();  // This maps Razor Pages routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // MVC route for controllers and actions

app.Run();
