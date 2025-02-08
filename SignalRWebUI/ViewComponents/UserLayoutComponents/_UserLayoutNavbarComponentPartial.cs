using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UserLayoutComponents
{
    public class _UserLayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string username = HttpContext.User.FindFirst("username")?.Value;
            string nameSurname = HttpContext.User.FindFirst("namesurname")?.Value;

            ViewBag.UserName = username;
            ViewBag.NameSurname = nameSurname;

            return View();
        }
    }
}
