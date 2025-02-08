using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
