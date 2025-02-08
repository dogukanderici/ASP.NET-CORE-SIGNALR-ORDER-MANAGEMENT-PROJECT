using Autofac;
using SignalR.Business.Abstract;
using SignalR.Business.Concrete;
using SignalR.Core.Utilities.Handlers;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.EntityFrameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.DependencyResolvers.Autofac
{
    public class AutofacDependencyResolverModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // DataAccess DI
            builder.RegisterType<EfAboutDal>().As<IAboutDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfBookingDal>().As<IBookingDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfContactDal>().As<IContactDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfDailyDiscountDal>().As<IDailyDiscountDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfFeatureDal>().As<IFeatureDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfProductDal>().As<IProductDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfSocialMediaDal>().As<ISocialMediaDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfTestimonialDal>().As<ITestimonialDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfWorkingHourDal>().As<IWorkingHourDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderDetailDal>().As<IOrderDetailDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfMoneyCaseDal>().As<IMoneyCaseDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfRestaurantTableDal>().As<IRestaurantTableDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfBasketDal>().As<IBasketDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfNotificationDal>().As<INotificationDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfMessageDal>().As<IMessageDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfMainMessageDal>().As<IMainMessageDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfHelpDeskDal>().As<IHelpDeskDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfDiscountDal>().As<IDiscountDal>().InstancePerLifetimeScope();

            // Business DI
            builder.RegisterType<AboutManager>().As<IAboutService>();
            builder.RegisterType<BookingManager>().As<IBookingService>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<DailyDiscountManager>().As<IDailyDiscountService>();
            builder.RegisterType<FeatureManager>().As<IFeatureService>();
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<SocialMediaManager>().As<ISocialMediaService>();
            builder.RegisterType<TestimonialManager>().As<ITestimonialService>();
            builder.RegisterType<WorkingHourManager>().As<IWorkingHourService>();
            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>();
            builder.RegisterType<MoneyCaseManager>().As<IMoneyCaseService>();
            builder.RegisterType<RestaurantTableManager>().As<IRestaurantTableService>();
            builder.RegisterType<BasketManager>().As<IBasketService>();
            builder.RegisterType<NotificationManager>().As<INotificationService>();
            builder.RegisterType<MessageManager>().As<IMessageService>();
            builder.RegisterType<MainMessageManager>().As<IMainMessageService>();
            builder.RegisterType<HelpDeskManager>().As<IHelpDeskService>();
            builder.RegisterType<DiscountManager>().As<IDiscountService>();
            //builder.RegisterType<IdentityManager>().As<IIdentityService>().InstancePerLifetimeScope();
        }
    }
}
