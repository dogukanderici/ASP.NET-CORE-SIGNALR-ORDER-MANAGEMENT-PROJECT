using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Areas.User.Controllers
{
    [Authorize]
    public class BaseUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
