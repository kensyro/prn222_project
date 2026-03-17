using Franchise.Repositories.HuyLT;
using Franchise.Services.HuyLT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Franchise.Repositories.HuyLT.DBContext.FranchiseManagementContext>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<Franchise.Repositories.HuyLT.InventoryRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.InventoryService>();
builder.Services.AddScoped<Franchise.Repositories.HuyLT.IngredientRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.IngredientService>();
builder.Services.AddScoped<Franchise.Repositories.HuyLT.FranchiseStoreRepository>();
builder.Services.AddScoped<Franchise.Services.HuyLT.FranchiseStoreService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<Franchise.Razor.WebApp.HuyLT.Hubs.FranchiseHub>("/franchiseHub");

app.Run();
