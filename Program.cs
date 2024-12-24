using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Interfaces;
using Cre8tfolioDAL;
using dotenv.net;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<IBlogRepository, BlogRepository>();

builder.Services.AddScoped<BlogService>();
//Dependecy injection in constructer/ program = wat je nodig hebt voor dependecy inversion
//Dependenct inversion is wat ik al wist

builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

builder.Services.AddScoped<PortfolioService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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


































