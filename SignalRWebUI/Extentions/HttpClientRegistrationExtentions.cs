using SignalRWebUI.Handlers;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Services.Concrete;
using SignalRWebUI.Services.Concrete.IdentityServices;

namespace SignalRWebUI.Extentions
{
    public static class HttpClientRegistrationExtentions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, string baseUrl, string identityServerBaseUrl)
        {

            // Identity İşlemleri

            #region

            services.AddHttpClient<IRoleDefinitionServicePS, RoleDefinitionServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(identityServerBaseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IUserRoleDefinitonServicePS, UserRoleDefinitionServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(identityServerBaseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IUserProfileChangeServicePS, UserProfileChangeServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(identityServerBaseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            #endregion

            // Api İşlemleri

            #region

            services.AddHttpClient<IDefaultAboutServicePS, DefaultAboutService>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IDefaultFeatureServicePS, DefaultFeatureService>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IDailyDiscountPS, DailyDiscountServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IContactServicePS, ContactServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<ISocialMediaServicePS, SocialMediaServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IWorkingHourServicePS, WorkingHourServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<ITestimonialServicePS, TestimonialServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IBookingServicePS, BookingServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<ICategoryServicePS, CategoryServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IProductServicePS, ProductServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IMessageServicePS, MessageServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<INotificationServicePS, NotificationServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IRestaurantTableServicePS, RestaurantTableServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IHelpDeskServicePS, HelpDeskServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<ISendMailServicePS, SendMailServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IBasketServicePS, BasketServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IDiscountServicePS, DiscountServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IOrderServicePS, OrderServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IOrderDetailServicePS, OrderDetailServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            services.AddHttpClient<IMoneyCaseServicePS, MoneyCaseServicePS>(opt =>
            {
                opt.BaseAddress = new Uri(baseUrl);
            })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            #endregion

            return services;
        }
    }
}
