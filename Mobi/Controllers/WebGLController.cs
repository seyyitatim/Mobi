using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mobi.Controllers
{
    public class WebGLController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
