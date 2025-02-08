using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminBaseController : Controller
    {
    }
}
