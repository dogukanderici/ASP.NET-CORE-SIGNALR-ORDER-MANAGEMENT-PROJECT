using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SignalR.Business.Abstract;
using SignalR.Business.Settings;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.NotificationDtos;
using SignalR.Dtos.Dtos.PropertySettingsDtos;
using SignalR.Entities.Concrete;
using System.Globalization;
using System.Net;

namespace SignalRApi.Hubs
{

    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IOrderService _orderService;
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        private readonly IMapper _mapper;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IMoneyCaseService moneyCaseService, IOrderService orderService, IRestaurantTableService restaurantTableService, IBookingService bookingService, INotificationService notificationService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _moneyCaseService = moneyCaseService;
            _orderService = orderService;
            _restaurantTableService = restaurantTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;

            _mapper = mapper;
        }

        public static int clientCount { get; set; } = 0;

        // Frontend'de Çağrılacak Metotlar

        public async Task SendStatistics()
        {
            await SendCategoryCount();
            await SendActiveCategoryCount();
            await SendProductCount();
            await SendAverageProductPrice();
            await SendMaxProductPrice();
            await SendMinProductPrice();
            await SendMoneyCaseStatistics();
            await SendTotalOrderCount();
            await SendActiveOrderCount();
            await SendCompletedOrderCount();
            await SendCanceledOrderCount();
            await SendLastOrderPrice();
            await SendTodayTotalOrderPrice();
        }

        public async Task SendProgressBarDatas()
        {
            await SendTotalMoneyCaseForProgress();
            await SendActiveOrderCountForProgress();
            await SendDailyTotalMoneyCaseForProgress();
            await SendMonthlyTotalIncomeOutcomeForProgress();
            await SendTotalActiveOrderCountForProggressBar();
        }

        public async Task GetBookingList()
        {
            IDataResult<List<Booking>> values = _bookingService.TGetAllData();

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveBookingList", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveBookingList", values.Data);
        }

        public async Task SendNotification()
        {
            await SendUnreadNotificationCount();
            await SendUnreadNotifications();
        }

        public async Task SendRestaurantTable()
        {
            await SendRestaurantTableStatus();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }

        // Yardımcı Metotlar
        #region

        //Dashboard
        public async Task SendCategoryCount()
        {
            IDataResult<int> value = _categoryService.GetCategoryActiveCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveCategoryCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveCategoryCount", value.Data);
        }

        public async Task SendActiveCategoryCount()
        {
            IDataResult<int> value = _categoryService.GetCategoryActiveCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveActiveCategoryCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveActiveCategoryCount", value.Data);
        }

        public async Task SendProductCount()
        {
            IDataResult<int> value = _productService.TGetProductCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveProductCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveProductCount", value.Data);
        }

        public async Task SendAverageProductPrice()
        {
            IDataResult<decimal> value = _productService.TGetAverageProductPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveAverageProductPrice", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveAverageProductPrice", value.Data);
        }

        public async Task SendMaxProductPrice()
        {
            IDataResult<PropertySettings> value = _productService.TGetMaxProductWithPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveMaxProductPrice", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveMaxProductPrice", value.Data.Name);
        }

        public async Task SendMinProductPrice()
        {
            IDataResult<PropertySettings> value = _productService.TGetMaxProductWithPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveMinProductPrice", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveMinProductPrice", value.Data.Name);
        }

        public async Task SendMoneyCaseStatistics()
        {
            IDataResult<List<PropertySettings>> value = _moneyCaseService.TGetTotalMoneyCaseAmount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveIncomeMoneyCase", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveIncomeMoneyCase", value.Data.Where(x => x.Name == "Income").Select(x => x.Value));
            await Clients.All.SendAsync("ReceiveOutcomeMoneyCase", value.Data.Where(x => x.Name == "Outcome").Select(x => x.Value));
            await Clients.All.SendAsync("ReceiveTotalMoneyCase", value.Data.Where(x => x.Name == "Difference").Select(x => x.Value));
        }

        public async Task SendTotalOrderCount()
        {
            IDataResult<int> value = _orderService.TGetTotalOrder();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveTotalOrderCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveTotalOrderCount", value.Data);
        }

        public async Task SendActiveOrderCount()
        {
            IDataResult<int> value = _orderService.TGetActiveOrderCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveActiveOrderCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveActiveOrderCount", value.Data);
        }

        public async Task SendCompletedOrderCount()
        {
            IDataResult<int> value = _orderService.TGetCompletedOrderCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveCompletedOrderCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveCompletedOrderCount", value.Data);
        }

        public async Task SendCanceledOrderCount()
        {
            IDataResult<int> value = _orderService.TGetCanceledOrderCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveCanceledOrderCount", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveCanceledOrderCount", value.Data);
        }

        public async Task SendLastOrderPrice()
        {
            IDataResult<decimal> value = _orderService.TGetLastOrderPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveLastOrderPrice", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveLastOrderPrice", value.Data);
        }

        public async Task SendTodayTotalOrderPrice()
        {
            IDataResult<decimal> value = _orderService.TGetTodayTotalPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveTodayTotalOrderPrice", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveTodayTotalOrderPrice", value.Data);
        }

        // ProgressBar

        public async Task SendTotalMoneyCaseForProgress()
        {
            IDataResult<List<PropertySettings>> value = _moneyCaseService.TGetTotalMoneyCaseAmount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveTotalMoneyCaseForProgress", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveTotalMoneyCaseForProgress", value.Data.Where(x => x.Name == "Difference").Select(x => x.Value.ToString("C", new CultureInfo("tr-TR"))));
        }

        public async Task SendActiveOrderCountForProgress()
        {
            IDataResult<int> value = _orderService.TGetActiveOrderCount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveActiveOrderCountForProgress", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveActiveOrderCountForProgress", value.Data);
        }

        public async Task SendDailyTotalMoneyCaseForProgress()
        {
            IDataResult<decimal> value = _orderService.TGetTodayTotalPrice();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveDailyTotalMoneyCaseForProgress", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveDailyTotalMoneyCaseForProgress", value.Data.ToString("C", new CultureInfo("tr-TR")));
        }

        public async Task SendMonthlyTotalIncomeOutcomeForProgress()
        {
            IDataResult<List<PropertySettings>> value = _moneyCaseService.TGetTotalMoneyCaseAmount();

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveMonthlyTotalIncomeOutcomeForProgress", "Bir Hata Oluştu!");
            }

            //await Clients.All.SendAsync("ReceiveMonthlyTotalIncomeOutcomeForProgress", new
            //{
            //    outcomeValue = value.Data.Where(x => x.Name == "Outcome").Select(x => x.Value.ToString("C", new CultureInfo("tr-TR"))),
            //    incomeValue = value.Data.Where(x => x.Name == "Income").Select(x => x.Value.ToString("C", new CultureInfo("tr-TR")))
            //});

            decimal income = value.Data.Where(x => x.Name == "Income").Select(x => x.Value).FirstOrDefault();
            decimal outcome = value.Data.Where(x => x.Name == "Outcome").Select(x => x.Value).FirstOrDefault();

            await Clients.All.SendAsync("ReceiveMonthlyTotalIncomeOutcomeForProgress", new
            {
                totalString = (income - outcome).ToString("C", new CultureInfo("tr-TR")),
                incomeString = income.ToString("C", new CultureInfo("tr-TR")),
                outcomeString = outcome.ToString("C", new CultureInfo("tr-TR")),
                outcomeValue = outcome,
                incomeValue = income
            });
        }

        public async Task SendTotalActiveOrderCountForProggressBar()
        {
            IDataResult<int> totalOrderValue = _orderService.TGetTotalOrder();
            IDataResult<int> activeOrderValue = _orderService.TGetActiveOrderCount();

            if ((totalOrderValue.SuccessCode != HttpStatusCode.OK) && (activeOrderValue.SuccessCode != HttpStatusCode.OK))
            {
                await Clients.All.SendAsync("ReceiveTotalActiveOrderCountForProggressBar", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveTotalActiveOrderCountForProggressBar", new { totalOrderValue = totalOrderValue.Data, activeOrderValue = activeOrderValue.Data });
        }

        // Notification

        public async Task SendUnreadNotificationCount()
        {
            IDataResult<int> value = _notificationService.GetUnreadNotificationCount();
            await Clients.All.SendAsync("ReceiveUnreadNotificationCount", value.Data);
        }

        public async Task SendUnreadNotifications()
        {
            IDataResult<List<Notification>> values = _notificationService.TGetAllData(n => n.Status == false, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                await Clients.All.SendAsync("ReceiveUnreadNotifications", "Bir Hata Oluştu!");
            }

            await Clients.All.SendAsync("ReceiveUnreadNotifications", _mapper.Map<List<ResultNotificationDto>>(values.Data));
        }

        // Restaurant Tables
        public Task SendRestaurantTableStatus()
        {
            IDataResult<List<RestaurantTable>> values = _restaurantTableService.TGetAllData(null, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return Clients.All.SendAsync("ReceiveRestaurantTableStatus", "Bir Hata Oluştu!");
            }

            return Clients.All.SendAsync("ReceiveRestaurantTableStatus", values.Data);

        }

        #endregion
    }
}
