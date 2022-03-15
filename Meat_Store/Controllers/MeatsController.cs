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
        private IAllCategories allCategories;
        public MeatsController(IAllMeat allmeat, IAllCategories allCategories)
        {
            this.allmeat = allmeat;
            this.allCategories = allCategories;
        }
        [Route("Categories/ListOfProducts/{category}")]
        public ViewResult ListOfProducts(int ?category)
        {
            string message = "Всі страви";
            IEnumerable<Meat> meats;
            meats = allmeat.All_Meat.Where(m => m.CategoryId == category).OrderBy(m => m.Id).ToList();
            if (meats.Count() == 0)
            {
                message = "Відсутні страви цієї категорії";
            }
            else 
            {
                message = allCategories.GetCategory(category).CategoryName;
            }
            var homemeat = new MeatsViewModel()
            {
                Message = message,
                meats = meats
            };
            
            return View(homemeat);
        }
        [Route("Meats/FullListOfProducts")]
        public ViewResult FullListOfProducts()
        {
            string message = "Всі страви";
            IEnumerable<Meat> meats = allmeat.All_Meat.OrderBy(x => x.CategoryId).ToList();
            var homemeat = new MeatsViewModel()
            {
                Message = message,
                meats = meats
            };

            return View(homemeat);
        }
        [Route("Meats/SinglePosition/{meat_id}")]
        [Route("Categories/ListOfProducts/1/SinglePosition/{meat_id}")]
        [Route("Categories/ListOfProducts/2/SinglePosition/{meat_id}")]
        [Route("Categories/ListOfProducts/3/SinglePosition/{meat_id}")]
        public ViewResult SinglePosition(int meat_id)
        {
            var viewProduct = new MeatsViewModel
            {
                Product = allmeat.GetProduct(meat_id)
            };
            return View(viewProduct);
        }
    }
}
