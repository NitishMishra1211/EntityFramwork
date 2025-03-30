using DataAccess.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repository.IRepository;
using DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<ApplicationDbContext>(items =>
{
    items.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.s
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");

app.Run();
