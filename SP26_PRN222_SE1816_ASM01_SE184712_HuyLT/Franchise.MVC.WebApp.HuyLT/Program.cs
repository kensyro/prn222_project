using Microsoft.AspNetCore.Authentication.Cookies;
using Franchise.MVC.WebApp.HuyLT.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/FranchiseStore/Login";
        options.AccessDeniedPath = "/Home/Error";
    });

// Register Filters
builder.Services.AddScoped<AuthenticationFilter>();

// Register DBContext
builder.Services.AddScoped<Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext>();

// Dependency Injection for FranchiseStore
builder.Services.AddScoped<Franchise.Repositories.HuyLT.FranchiseStoreRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.IFranchiseStoreService, Franchise.Services.HuyLT.FranchiseStoreService>();

// Dependency Injection for Ingredient
builder.Services.AddScoped<Franchise.Repositories.HuyLT.IngredientRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.IIngredientService, Franchise.Services.HuyLT.IngredientService>();

// Dependency Injection for Inventory
builder.Services.AddScoped<Franchise.Repositories.HuyLT.InventoryRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.IInventoryService, Franchise.Services.HuyLT.InventoryService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
