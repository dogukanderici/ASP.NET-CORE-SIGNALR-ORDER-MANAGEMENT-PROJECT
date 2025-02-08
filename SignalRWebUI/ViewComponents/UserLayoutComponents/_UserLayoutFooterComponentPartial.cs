using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UserLayoutComponents
{
    public class _UserLayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
