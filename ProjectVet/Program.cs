using Microsoft.EntityFrameworkCore;
using ProjectVet.Areas.Admin.Services;
using ProjectVet.EfCore;
using ProjectVet.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectVet.Areas.Kullanici.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ProjectVet.SignalR;
using ProjectVet.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var str = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<KlinikContext>(x => x.UseSqlServer(str));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddScoped<AddPetsService>();

builder.Services.AddScoped<IRandevuService, RandevuService>();
builder.Services.AddScoped<IKullaniciRandevuService, KullaniciRandevuService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<KlinikContext>();

// Decorator for KullaniciRandevuService
builder.Services.AddScoped<IKullaniciRandevuService>(provider =>
{
    var context = provider.GetService<KlinikContext>();
    var httpContextAccessor = provider.GetService<IHttpContextAccessor>();   //Decorator Manuel Eklendi
    var originalService = new KullaniciRandevuService(context, httpContextAccessor);
    return new LoggingKullaniciRandevuServiceDecorator(originalService);
});
builder.Services.AddScoped<IAdminService>(provider => AdminService.GetInstance(provider.GetService<KlinikContext>()));
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IProjectVetFacade, ProjectVetFacade>();

// Add SignalR to the services
builder.Services.AddSignalR();

var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    //Bu K�s�mda ekliyoruz Randevuhuubu ve buray� sak�n silmeyinnn!
    endpoints.MapHub<RandevuHub>("/randevuhub");
});

app.Run();
