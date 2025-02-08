using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Controllers
{
    [Authorize]
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<string> userRoles = User?.Claims.Where(c => c.Type == "role").Select(c => c.Value);

            bool isAdmin = userRoles.Contains("AdminPermission");

            if (isAdmin)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Booking", new { area = "User" });
        }
    }
}
