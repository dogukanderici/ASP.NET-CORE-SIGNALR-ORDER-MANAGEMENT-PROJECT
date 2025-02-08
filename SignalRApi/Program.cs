using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using SignalR.DataAccess.Concrete.Context;
using System.Reflection;
using SignalRApi.Hubs;
using SignalR.Business.Utilities.DependencyResolvers.Autofac;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SignalR.Core.Utilities.Requirements;
using SignalR.Core.Utilities.Handlers;
using SignalRApi.Settings;
using SignalRApi.Utilities.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL
        options.Audience = "ResourceFullPermission"; // IdentityServer'da tanýmlanan API resource
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = "role",
            NameClaimType = "name",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadWritePermissionPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement("ReadWritePermission"));
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPermissionPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement("ReadPermission"));
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WritePermissionPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement("WritePermission"));
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPermissionPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement("AdminPermission"));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();

builder.Services.AddDbContext<SignalRContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SignalRDbSettings"));
});

builder.Services.Configure<SendMailSettings>(builder.Configuration.GetSection("SendMailSettings"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacDependencyResolverModule());
    });

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddFluentValidations();


// Add services to the container.

builder.Services.AddControllers();
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

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");

app.Run();
