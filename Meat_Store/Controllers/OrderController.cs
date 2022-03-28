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
            if (!allOrders.CheckIfExist())
            {
                return RedirectToAction("ErrorOrder", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Зменшіть кількість елементів в кошику"
                }));
            }
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(DeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(!Equals(model.delivery.DeliveryType, 3) && Equals(model.delivery.City, null))
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", "Потрібно обрати місто при доставкі.");
                return View(model);
            }
            if (Equals(model.delivery.DeliveryType, 3) && !Equals(model.delivery.City, null))
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", "Не можна обирати місто при самовивозі.");
                return View(model);
            }
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
