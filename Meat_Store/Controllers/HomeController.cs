using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Meat_Store.Interfaces;

namespace Meat_Store.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Home_Page()
        {
            return View();
        }
    }
}