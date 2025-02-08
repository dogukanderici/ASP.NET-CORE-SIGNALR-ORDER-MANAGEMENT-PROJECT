using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignalR.IdentityServerApi;
using SignalR.IdentityServerApi.Data;
using SignalR.IdentityServerApi.Models;
using SignalR.IdentityServerApi.Services.Concrete;
using SignalR.IdentityServerApi.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalApiAuthentication();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMemoryCache();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential() // not recommended for production - you need to store your key material somewhere secure
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<UserCustomProfileService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

//app.MapGet("/", () => "IdentityServer is running!");

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
