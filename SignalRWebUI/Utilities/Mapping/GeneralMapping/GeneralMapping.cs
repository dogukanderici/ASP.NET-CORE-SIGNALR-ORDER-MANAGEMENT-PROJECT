using AutoMapper;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalR.Dtos.Dtos.CategoryDtos;
using SignalR.Dtos.Dtos.ContactDtos;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalR.Dtos.Dtos.Discounts;
using SignalR.Dtos.Dtos.FeatureDtos;
using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalR.Dtos.Dtos.MessageDtos;
using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalR.Dtos.Dtos.NotificationDtos;
using SignalR.Dtos.Dtos.OrderDetailDtos;
using SignalR.Dtos.Dtos.OrderDtos;
using SignalR.Dtos.Dtos.ProductDtos;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.ContactDtos;
using SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos;
using SignalRWebUI.Areas.Admin.Dtos.DiscountDtos;
using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos;
using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos;

namespace SignalRWebUI.Utilities.Mapping.GeneralMapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ResultAboutDto, ResultAdminAboutDto>().ReverseMap();
            CreateMap<ResultAboutDto, UpdateAdminAboutDto>().ReverseMap();

            CreateMap<ResultBookingDto, ResultAdminBookingDto>().ReverseMap();
            CreateMap<ResultBookingDto, UpdateAdminBookingDto>().ReverseMap();

            CreateMap<ResultCategoryDto, ResultAdminCategoryDto>().ReverseMap();
            CreateMap<ResultCategoryDto, UpdateAdminCategoryDto>().ReverseMap();

            CreateMap<ResultContactDto, ResultAdminContactDto>().ReverseMap();
            CreateMap<ResultContactDto, UpdateAdminContactDto>().ReverseMap();

            CreateMap<ResultDailyDiscountDto, ResultAdminDailyDiscountDto>().ReverseMap();
            CreateMap<ResultDailyDiscountDto, UpdateAdminDailyDiscountDto>().ReverseMap();

            CreateMap<ResultFeatureDto, ResultAdminFeatureDto>().ReverseMap();
            CreateMap<ResultFeatureDto, UpdateAdminFeatureDto>().ReverseMap();

            CreateMap<ResultMessageDto, ResultAdminMessageDto>().ReverseMap();
            CreateMap<ResultMessageDto, UpdateAdminMessageDto>().ReverseMap();

            CreateMap<ResultNotificationDto, ResultAdminNotificationDto>().ReverseMap();
            CreateMap<ResultNotificationDto, UpdateAdminNotificationDto>().ReverseMap();

            CreateMap<ResultRestaurantTableDto, ResultAdminRestaurantTableDto>().ReverseMap();
            CreateMap<ResultRestaurantTableDto, UpdateAdminRestaurantTableDto>().ReverseMap();

            CreateMap<ResultProductDto, ResultAdminProductDto>().ReverseMap();
            CreateMap<ResultProductDto, UpdateAdminProductDto>().ReverseMap();

            CreateMap<ResultSocialMediaDto, ResultAdminSocialMediaDto>().ReverseMap();
            CreateMap<ResultSocialMediaDto, UpdateAdminSocialMediaDto>().ReverseMap();

            CreateMap<ResultTestimonialDto, ResultAdminTestimonialDto>().ReverseMap();
            CreateMap<ResultTestimonialDto, UpdateAdminTestimonialDto>().ReverseMap();

            CreateMap<ResultWorkingHourDto, ResultAdminWorkingHourDto>().ReverseMap();
            CreateMap<ResultWorkingHourDto, UpdateAdminWorkingHourDto>().ReverseMap();

            CreateMap<ResultHelpDeskDto, ResultAdminHelpDeskDto>().ReverseMap();
            CreateMap<ResultHelpDeskDto, UpdateAdminHelpDeskDto>().ReverseMap();

            CreateMap<ResultDiscountDto, ResultAdminDiscountDto>().ReverseMap();
            CreateMap<ResultDiscountDto, UpdateAdminDiscountDto>().ReverseMap();

            CreateMap<ResultOrderDto, ResultAdminOrderDto>().ReverseMap();
            CreateMap<ResultOrderDto, UpdateAdminOrderDto>().ReverseMap();

            CreateMap<ResultOrderDetailDto, ResultAdminOrderDetailDto>().ReverseMap();

            CreateMap<ResultMoneyCaseDto, ResultAdminMoneyCaseDto>().ReverseMap();
            CreateMap<ResultMoneyCaseDto, UpdateAdminMoneyCaseDto>().ReverseMap();
        }
    }
}
