using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;

namespace Meat_Store.Repositories
{
    public class OrdersRepository : IAllOrders
    {
        private readonly ShopContext _shopContext;
        private readonly ShopCart shopCart;

        public OrdersRepository(ShopContext shopContext, ShopCart shopCart)
        {
            _shopContext = shopContext;
            this.shopCart = shopCart;
        }

        public IEnumerable<Meat> GetOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CheckIfExist()
        {
            shopCart.listShopitems = shopCart.getShopCartItems();
            var items = shopCart.listShopitems.ToList();
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
            shopCart.listShopitems = shopCart.getShopCartItems();
            var items = shopCart.listShopitems.ToList();
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
