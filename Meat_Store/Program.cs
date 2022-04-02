using Meat_Store.Models;
using Microsoft.EntityFrameworkCore;
using Meat_Store.Interfaces;
using Meat_Store.Repositories;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ShopContext>(option => option.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")    
));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddTransient<IAllMeat, MeatRepository>();
builder.Services.AddTransient<IAllCategories, CategoriesRepository>();
builder.Services.AddTransient<IAllOrders, OrdersRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShopCart.GetCart(sp));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

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

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home_Page}/{id?}");
    
app.Run();
