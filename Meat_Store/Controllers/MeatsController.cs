using Microsoft.AspNetCore.Mvc;
using Meat_Store.Repositories;
using Meat_Store.ViewModels;
using Meat_Store.Interfaces;
using Meat_Store.Models;

namespace Meat_Store.Controllers
{
    public class MeatsController : Controller
    {
        private IAllMeat allmeat;

        public MeatsController(IAllMeat allmeat)
        {
            this.allmeat = allmeat;
        }
        [Route("Meats/ListOfProducts")]
        public ViewResult ListOfProducts(string category)
        {
            IEnumerable<Meat> meats;
            if (String.IsNullOrEmpty(category))
            {
                meats = allmeat.All_Meat.OrderBy(x => x.CategoryId).ToList();
            }
            else
            {
                meats = allmeat.Meat_of_category.OrderBy(c => c.CategoryId).ThenBy(c => c.Id);
            }
            var homemeat = new MeatsViewModel()
            {
                All_Products = meats
            };

            return View(homemeat);
        }
    }
}
