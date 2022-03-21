using Meat_Store.Interfaces;
using Meat_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Store.Controllers
{
    public class OrderController: Controller
    {
        private IAllOrders allOrders;
        private readonly ShopCart cart;
        public OrderController(IAllOrders allOrders, ShopCart cart)
        {
            this.allOrders = allOrders;
            this.cart = cart;
        }
        [Route("Order/CheckOut")]
        public IActionResult CheckOut()
        {
            cart.listShopitems = cart.getShopCartItems();
            if (cart.listShopitems.Count == 0)
            {
                return RedirectToAction("ErrorOrder", "Order");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            cart.listShopitems = cart.getShopCartItems();

            if (ModelState.IsValid)
            {
                if (allOrders.CreateOrder(order))// перенести цю частину коду на іншу сторінку(вже після обрання способу доставки)
                {
                    return RedirectToAction("Complete");// це поки тимчасово. Далі потрібно обрати доставку
                }
            }
            return View(order);
        }
        public IActionResult ErrorOrder()
        {
            ViewBag.Message = "Вам кошик порожній";
            return View();
        }
        public IActionResult Complete()
        {
            ViewBag.Message = "Ви Успішно оформили замовлення";
            return View();
        }
    }
}
