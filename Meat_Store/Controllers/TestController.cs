using Microsoft.AspNetCore.Mvc;

namespace Meat_Store.Controllers
{
    public class TestController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
