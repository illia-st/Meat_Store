using Meat_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Newtonsoft.Json;
using System.Security.Claims;
using Meat_Store.ViewModels;

namespace Meat_Store.Controllers
{
    public class FavouritePositionController : Controller
    {
        private readonly ShopContext context;
        private readonly UserManager<User> _userManager;

        public FavouritePositionController(ShopContext context, UserManager<User> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        
        [Route("FavouritePosition/AddToFavourite/{meat_id}")]
        public IActionResult AddToFavourite(int meat_id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            context.FavoutirePositions.Add(new FavoutirePosition()
            {
                MeatId = meat_id,
                UserId = userId
            });

            context.SaveChanges();

            return RedirectToAction("ListOfFavourite");
        }
        [Route("FavouritePosition/RemoveFromFavourite/{meat_id}")]
        public IActionResult RemoveFromFavourite(int meat_id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            context.FavoutirePositions.Remove(context.FavoutirePositions.FirstOrDefault(fav => fav.MeatId == meat_id
            && fav.UserId == userId));

            context.SaveChanges();

            return RedirectToAction("ListOfFavourite");
        }

        public IActionResult ListOfFavourite()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var list = context.FavoutirePositions.Where(fav => fav.UserId == userId).Select(fav => fav.MeatId).ToList();

            IEnumerable<Meat> meats = list.Select(fav => context.Meats.FirstOrDefault(m => m.Id == fav)).ToList();

            var model = new MeatsViewModel()
            {
                meats = meats,
                Message = "Улюблені страви"
            };

            return View(model);
        }
    }
}
