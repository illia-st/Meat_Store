using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Meat_Store.Controllers
{
    public class ShopCartController : Controller
    {
        private IAllMeat _meatRepository;
        private ShopCart _shopCart;

        public ShopCartController(IAllMeat meatRepository, ShopCart shopCart)
        {
            _meatRepository = meatRepository;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            var items = _shopCart.getShopCartItems();
            _shopCart.listShopitems = items;

            var obj = new ShopCartViewModel { shopCart = _shopCart };

            return View(obj);

        }
        [Route("Meats/addToShopCart/{id}")]
        [Route("Meats/addToCart/{id}")]
        [Route("FavouritePosition/addToCart/{id}")]
        public RedirectToActionResult addToCart(int id)
        {
            var curItem = _meatRepository.All_Meat.FirstOrDefault(i => i.Id == id);
            if (curItem != null && curItem.Portion != 0)
            {
                _shopCart.AddToCart(curItem, 1);
            }
            else if(curItem != null)
            {

                return RedirectToAction("ErrorOrder", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = curItem.Name.Trim() + " " + curItem.Error_msg.ToLower()
                }));
            }
            return RedirectToAction("Index");
        }
        [Route("ShopCart/DeleteItemFromCart/{Name}")]
        public IActionResult DeleteItemFromCart(string Name)
        {
            _shopCart.DeleteFromCart(Name);

            return RedirectToAction("Index");
        }
    }
}
