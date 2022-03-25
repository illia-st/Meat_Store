using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
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
        
        public IActionResult CheckOut()
        {
            cart.listShopitems = cart.getShopCartItems();

            if (cart.listShopitems.Count == 0)
            {
                return RedirectToAction("ErrorOrder", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Ваш кошик порожній"
                }));
            }
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(DeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(allOrders.CreateOrder(new Order()
                {
                    Name = (string)model.order.Name.Clone(),
                    Surname = (string)model.order.Surname.Clone(),
                    Email = (string)model.order.Email.Clone(),
                    PhoneNumber = (string?)model.order.PhoneNumber?.Clone()
                }, model.delivery))
                {
                    return RedirectToAction("Complete");
                }
                return RedirectToAction("ErrorOrder", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Зменшіть кількість товару в кошику"
                }));
            }
            return View(model);
        }
        
        public IActionResult ErrorOrder(string error)
        {
            ViewBag.Message = error;
            return View();
        }
        public IActionResult Complete()
        {
            ViewBag.Message = "Ви Успішно оформили замовлення";
            return View();
        }
    }
}
