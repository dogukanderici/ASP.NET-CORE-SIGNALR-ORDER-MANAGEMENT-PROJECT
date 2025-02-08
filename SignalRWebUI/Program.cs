using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SignalR.DataAccess.Concrete.Context;
using SignalR.IdentityServerApi.Settings;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Services.Concrete.IdentityServices;
using SignalRWebUI.Settings;
using System.Reflection;
using SignalRWebUI.Extentions;
using Microsoft.AspNetCore.Authorization;
using SignalR.Core.Utilities.Handlers;
using SignalR.Core.Utilities.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.LogoutPath = "/Logout/Index/";
        opt.AccessDeniedPath = "/Error/Unauthorized401";

        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "SignalRJwt";
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.LogoutPath = "/Logout/Index";
        opt.AccessDeniedPath = "/Login/Index";
        opt.Cookie.Name = "SignalRCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPermissionPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement("WritePermission"));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddDbContext<SignalRContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SignalRDbSettings"));
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IIdentityService, IdentityManager>();
builder.Services.AddHttpClient<IClientCredentialsTokenService, ClientCredentialsTokenService>();

var urlSetting = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
var customUrlSetting = builder.Configuration.GetSection("CustomUrlSettings").Get<CustomUrlSettings>();

// Dependency Injection Registration Konfigürasyonlarý Yazýlan DependencyInjectionRegistrationExtentions Class'da Yönetilir.
builder.Services.AddDependencyInjectionServices();

// Http Client Registration Ýþlemleri Konfigürasyonlarý Yazýlan HttpClientRegistrationExtentions Class'da Yönetilir.
builder.Services.AddHttpClients($"{urlSetting.BaseUrl}", $"{customUrlSetting.IdentityServerApiUrl}");

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<CustomUrlSettings>(builder.Configuration.GetSection("CustomUrlSettings"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseStatusCodePages(async statusCode =>
{
    if (statusCode.HttpContext.Response.StatusCode == 404)
    {
        statusCode.HttpContext.Response.Redirect("/Error/NotFound/");
    }
});

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Default}/{action=Index}/{id?}"
    );
});

app.Run();
