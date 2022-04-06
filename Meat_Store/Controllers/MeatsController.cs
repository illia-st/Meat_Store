using Microsoft.AspNetCore.Mvc;
using Meat_Store.Repositories;
using Meat_Store.ViewModels;
using Meat_Store.Interfaces;
using Meat_Store.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Meat_Store.Controllers
{
    public class MeatsController : Controller
    {
        private IAllMeat allmeat;
        private IAllCategories allCategories;
        private readonly ShopContext context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public MeatsController(IAllMeat allmeat, IAllCategories allCategories, 
            UserManager<User> userManager, SignInManager<User> signInManager, ShopContext context)
        {
            this.allmeat = allmeat;
            this.allCategories = allCategories;
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
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
        [Route("FavouritePosition/SinglePosition/{meat_id}")]
        public async Task<ViewResult> SinglePosition(int meat_id)
        {
            var posModel = new SinglePositionViewModel()
            {
                meat = allmeat.GetProduct(meat_id)
            };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int? fav_meat = context.FavoutirePositions.FirstOrDefault(fav => fav.MeatId == meat_id 
                    && fav.UserId == userId)?.MeatId;
                if (fav_meat == null || fav_meat == 0)
                {
                    posModel.ifFav = false;
                }
                else
                {
                    posModel.ifFav = true;
                }
            }
            return View(posModel);
        }
    }
}
