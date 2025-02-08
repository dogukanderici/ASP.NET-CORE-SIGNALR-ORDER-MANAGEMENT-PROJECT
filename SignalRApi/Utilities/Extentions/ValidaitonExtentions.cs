using FluentValidation;
using FluentValidation.AspNetCore;
using SignalR.Business.Utilities.Validations.AboutValidations;
using SignalR.Business.Utilities.Validations.BasketValidations;
using SignalR.Business.Utilities.Validations.BookingValidations;
using SignalR.Business.Utilities.Validations.CategoryValidations;
using SignalR.Business.Utilities.Validations.ContactValidations;
using SignalR.Business.Utilities.Validations.DailyDiscountValidations;
using SignalR.Business.Utilities.Validations.DiscountValidations;
using SignalR.Business.Utilities.Validations.FeatureValidations;
using SignalR.Business.Utilities.Validations.HelpDeskValidations;
using SignalR.Business.Utilities.Validations.MainMessageValidations;
using SignalR.Business.Utilities.Validations.MessageValidations;
using SignalR.Business.Utilities.Validations.NotificationValidations;
using SignalR.Business.Utilities.Validations.ProductValidations;
using SignalR.Business.Utilities.Validations.RestaurantTableValidations;
using SignalR.Business.Utilities.Validations.SocialMediaValidations;
using SignalR.Business.Utilities.Validations.TestimonialValidations;
using SignalR.Business.Utilities.Validations.WorkingHourValidations;

namespace SignalRApi.Utilities.Extentions
{
    public static class ValidaitonExtentions
    {
        public static IServiceCollection AddFluentValidations(this IServiceCollection service)
        {
            // DataAnnotation Yapısı Devre Dışı Bırakılır.
            service.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            // About
            service.AddValidatorsFromAssemblyContaining<CreateAboutValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateAboutValidation>();

            // Basket
            service.AddValidatorsFromAssemblyContaining<CreateBasketValidation>();

            // Booking
            service.AddValidatorsFromAssemblyContaining<CreateBookingValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateBookingValidation>();

            // Category
            service.AddValidatorsFromAssemblyContaining<CreateCategoryValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateCategoryValidation>();

            // Contact
            service.AddValidatorsFromAssemblyContaining<CreateContactValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateContactValidation>();

            // DailyDiscount
            service.AddValidatorsFromAssemblyContaining<CreateDailyDiscountValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateDailyDiscountValidation>();

            // Discount
            service.AddValidatorsFromAssemblyContaining<CreateDiscountValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateDiscountValidation>();

            // Feature
            service.AddValidatorsFromAssemblyContaining<CreateFeatureValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateFeatureValidation>();

            // HelpDesk
            service.AddValidatorsFromAssemblyContaining<CreateHelpDeskValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateHelpDeskValidation>();

            // MainMessage
            service.AddValidatorsFromAssemblyContaining<CreateMainMessageValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateMainMessageValidation>();

            // Message
            service.AddValidatorsFromAssemblyContaining<CreateMessageValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateMessageValidation>();

            // Notification
            service.AddValidatorsFromAssemblyContaining<CreateNotificationValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateNotificationValidation>();

            // Product
            service.AddValidatorsFromAssemblyContaining<CreateProductValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateProductValidation>();

            // RestaurantTable
            service.AddValidatorsFromAssemblyContaining<CreateRestaurantTableValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateRestaurantTableValidation>();

            // SocialMedia
            service.AddValidatorsFromAssemblyContaining<CreateSocialMediaValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateSocialMediaValidation>();

            // Testimonial
            service.AddValidatorsFromAssemblyContaining<CreateTestimonialValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateTestimonialValidation>();

            // WorkingHour
            service.AddValidatorsFromAssemblyContaining<CreateWorkingHourValidation>();
            service.AddValidatorsFromAssemblyContaining<UpdateWorkingHourValidation>();

            return service;
        }
    }
}
