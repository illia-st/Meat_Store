using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Meat_Store.Interfaces;

namespace Meat_Store.Controllers
{
    public class HomeController : Controller
    {
        private IAllMeat _meatRepository;

        public HomeController(IAllMeat meatRepository)
        {
            _meatRepository = meatRepository;
        }
        public ViewResult AllProducts()
        {
            var homemeat = new HomeViewModel()
            {
                All_Products = _meatRepository.All_Meat
            };

            return View(homemeat);
        }
    }
}