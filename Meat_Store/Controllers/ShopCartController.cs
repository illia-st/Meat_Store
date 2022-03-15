using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        [Route("Meats/addToCart/{id}")]
        public RedirectToActionResult addToCart(int id)
        {
            var curItem = _meatRepository.All_Meat.FirstOrDefault(i => i.Id == id);
            if (curItem != null)
            {
                _shopCart.AddToCart(curItem, 1);
            }
            return RedirectToAction("Index");
        }
    }
}
