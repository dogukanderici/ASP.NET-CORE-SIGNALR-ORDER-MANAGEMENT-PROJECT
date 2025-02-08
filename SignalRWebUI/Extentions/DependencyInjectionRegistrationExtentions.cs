using SignalR.Business.Abstract;
using SignalR.Business.Concrete;
using SignalR.Core.Utilities.Handlers;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.EntityFrameworks;
using SignalRWebUI.Handlers;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Services.Concrete.IdentityServices;
using SignalRWebUI.Utilities.FileOperations;

namespace SignalRWebUI.Extentions
{
    public static class DependencyInjectionRegistrationExtentions
    {
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<ClientCredentialsTokenHandler>();
            services.AddScoped<IFileOperation, FileHelper>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IIdentityService, IdentityManager>();
            services.AddScoped<IClientCredentialsTokenService, ClientCredentialsTokenService>();
            services.AddScoped<ITokenCacheManagementService, TokenCacheManagementService>();
            services.AddScoped<IQRCodeGeneratorHandler, QRCodeGeneratorHandler>();

            return services;
        }
    }
}
