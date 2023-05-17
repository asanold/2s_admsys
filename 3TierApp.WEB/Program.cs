using _3TierApp.BLL.Infrastructure;
using _3TierApp.BLL.Interfaces;
using _3TierApp.BLL.Services;
using _3TierApp.DAL.EF;
using _3TierApp.DAL.Interfaces;
using _3TierApp.DAL.Repositories;
using _3TierApp.WEB.Util;
using Ninject;
using Ninject.Modules;
using Ninject.Web;
using Ninject.Web.Mvc;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IUnitOfWork>(s => new EFUnitOfWork("MyConnectionString"));
//CUserAdminisrationDBInitializer.Run("useradministration.db");
builder.Services.AddTransient<IUnitOfWork>(s => new EFCoreUnitOfWork("useradministration.db"));
//builder.Services.AddTransient<IUnitOfWork>(s => new EFUnitOfWork("MyConnectionString"));
//NinjectModule userModule = new UserModule();
//NinjectModule serviceModule = new ServiceModule("DefaultConnection");
//var kernel = new StandardKernel(userModule, serviceModule);
//DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

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
