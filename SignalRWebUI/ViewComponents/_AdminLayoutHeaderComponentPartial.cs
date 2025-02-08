using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
