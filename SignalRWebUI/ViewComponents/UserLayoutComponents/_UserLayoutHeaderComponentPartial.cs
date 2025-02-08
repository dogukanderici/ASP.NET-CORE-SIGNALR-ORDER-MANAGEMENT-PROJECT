using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UserLayoutComponents
{
    public class _UserLayoutHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
