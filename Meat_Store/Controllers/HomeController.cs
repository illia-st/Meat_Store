using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Meat_Store.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Meat_Store.Controllers
{
    //[Authorize(Roles = "admin, user")]
    public class HomeController : Controller
    {
        public ViewResult Home_Page()
        {
            return View();
        }
    }
}