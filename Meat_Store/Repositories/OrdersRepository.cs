using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Meat_Store.Repositories
{
    public class OrdersRepository : IAllOrders
    {
        private readonly ShopContext _shopContext;
        private readonly ShopCart shopCart;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public OrdersRepository(ShopContext shopContext, ShopCart shopCart, IHttpContextAccessor httpContextAccessor, IdentityContext identityContext)
        {
            _shopContext = shopContext;
            this.shopCart = shopCart;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Meat> GetOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CheckIfExist()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = new List<ShopCartItem>();

            if (String.IsNullOrEmpty(userId))
            {
                items = shopCart.listShopitems.ToList();
            }
            else
            {
                items = _shopContext.ShopCartItems.Where(sh => sh.ShopCartId == userId).ToList();
            }
            var order_elements = new Dictionary<int, int>();
            var table_elements = new Dictionary<int, int>();
            foreach (var item in items)
            {
                if (!order_elements.ContainsKey(item.MeatId))
                {
                    order_elements.Add(item.MeatId, 1);
                    continue;
                }
                order_elements[item.MeatId] += 1;
            }
            foreach (var item in order_elements)
            {
                table_elements[item.Key] = _shopContext.Meats.FirstOrDefault(m => m.Id == item.Key).Portion;
                if (item.Value > table_elements[item.Key])
                {
                    return false;
                }
            }
            return true;
        }

        public bool CreateOrder(Order order, Delivery delivery)// контролити кількість продукції
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = new List<ShopCartItem>();

            if (String.IsNullOrEmpty(userId))
            {
                items = shopCart.getShopCartItems();    
            }
            else
            {
                items = _shopContext.ShopCartItems.Where(sh => sh.ShopCartId == userId).ToList();
            }
            var order_elements = new Dictionary<int, int>();
            var table_elements = new Dictionary<int, int>();
            foreach (var item in items)
            {
                if (!order_elements.ContainsKey(item.MeatId))
                {
                    order_elements.Add(item.MeatId, 1);
                    continue;
                }
                order_elements[item.MeatId] += 1;
            }
            foreach (var item in order_elements)
            {
                _shopContext.Meats.FirstOrDefault(m => m.Id == item.Key).Portion -= item.Value;
                _shopContext.SaveChanges();
            }
            order.OrderTime = System.DateTime.Now;
            order.UserId = userId;
            delivery.OrderTime = System.DateTime.Now;

            _shopContext.Deliveries.Add(delivery);
            _shopContext.SaveChanges();

            order.DeliveryId = _shopContext.Deliveries.FirstOrDefault(d => d.OrderTime == delivery.OrderTime).Id;

            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();

            Order order_ = _shopContext.Orders.FirstOrDefault(or => or.OrderTime == order.OrderTime);

            foreach (var el in items)
            {
                var product = new OrderDetail
                {
                    MeatId = el.MeatId,
                    Price = el.Price,
                    OrderId = order_.Id
                };
                _shopContext.OrderDetails.Add(product);
                _shopContext.SaveChanges();
            }

            shopCart.ClearShopCart();

            return true;
        }
    }
}
