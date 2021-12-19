using Microsoft.AspNetCore.Mvc;

namespace Mobi.Controllers
{
    public class WebGLController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
