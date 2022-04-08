using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Store.Controllers
{
    public class OrderController: Controller
    {
        private IAllOrders allOrders;
        private readonly ShopCart cart;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public OrderController(IAllOrders allOrders, ShopCart cart, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.allOrders = allOrders;
            this.cart = cart;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<IActionResult> CheckOut()
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
            if (User.Identity.IsAuthenticated)
            {
                var current_user = await _userManager.GetUserAsync(User);
                return View(new DeliveryViewModel()
                {
                    order = new FakeOrder()
                    {
                        Name = current_user.Name,
                        Surname = current_user.Surname,
                        Email = current_user.Email,
                        PhoneNumber = current_user.PhoneNumber
                    },
                    delivery = new Delivery()
                });
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
            ViewBag.Message = "Ви успішно оформили замовлення";
            return View();
        }
    }
}
