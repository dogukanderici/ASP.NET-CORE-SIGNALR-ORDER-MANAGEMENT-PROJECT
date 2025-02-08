using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOfferComponentPartial : ViewComponent
    {
        private readonly IDailyDiscountPS _dailyDiscount;
        private readonly IMapper _mapper;

        public _DefaultOfferComponentPartial(IDailyDiscountPS dailyDiscount, IMapper mapper)
        {
            _dailyDiscount = dailyDiscount;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var response = await _dailyDiscount.GetAllDataAsync(null);

            DefaultDailyDiscountViewModel model = new DefaultDailyDiscountViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
            }

            List<ResultDailyDiscountDto> values = _mapper.Map<List<ResultDailyDiscountDto>>(response.GetData);
            model.ResultDailyDiscounts = values;

            return View(model);
        }
    }
}
