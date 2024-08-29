using DistributerManagementSystemBusinessLogic.Impl;
using DistributerManagementSystemBusinessLogic.Interface;
using DistributerManagementSystemModels;
using DistributerManagementSystemRepository;
using DistributerManagementSystemRepository.Common;
using DistributerManagementSystemRepository.Impl;
using DistributerManagementSystemRepository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adding connection strings
builder.Services.AddDbContext<DistributerManagementSystemContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DistributerManagementSystemContext"),
sqlServerOptions => sqlServerOptions.CommandTimeout(300)));

// Add services to the container.
builder.Services.AddControllersWithViews();

//Registering the services
builder.Services.AddScoped<IOrderbookService, OrderbookService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRetailerService, RetailerService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<ICreditManagementService, CreditManagementService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductPurchaseOrderbookService, ProductPurchaseOrderbookService>();


//registering the repositories
builder.Services.AddScoped<IOrderbookRepository, OrderbookRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRetailerRepository, RetailerRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<ICreditManagementRepository, CreditManagementRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductPurchaseOrderbookRepository, ProductPurchaseOrderbookRepository>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
