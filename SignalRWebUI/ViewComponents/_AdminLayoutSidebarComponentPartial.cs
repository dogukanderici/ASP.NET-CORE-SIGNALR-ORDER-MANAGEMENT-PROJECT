using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents
{
    public class _AdminLayoutSidebarComponentPartial : ViewComponent
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
