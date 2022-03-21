using Meat_Store.Interfaces;
using Meat_Store.Models;

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

        public bool CreateOrder(Order order)// контролити кількість продукції
        {
            var items = shopCart.listShopitems.ToList();
            var order_elements = new Dictionary<int, int>();
            var table_elements = new Dictionary<int, int>();
            foreach (var item in items)
            {
                order_elements[item.Id] += 1;
            }
            foreach(var item in order_elements)
            {
                table_elements[item.Key] = _shopContext.Meats.FirstOrDefault(m => m.Id == item.Key).Portion;
                if(item.Value > table_elements[item.Key])
                {
                    return false;
                }
            }
            foreach (var item in order_elements)
            {
                _shopContext.Meats.FirstOrDefault(m => m.Id == item.Key).Portion -= item.Value;
                _shopContext.SaveChanges();
            }
            order.OrderTime = System.DateTime.Now;
            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();

            var order_ = from _order in _shopContext.Orders
                         where _order.OrderTime == order.OrderTime
                         select _order;

            

            Order tempOrder = new Order();
            foreach(var item in order_)
            {
                tempOrder = item;
            }

            foreach (var el in items)
            {
                var product = new OrderDetail
                {
                    MeatId = el.MeatId,
                    Price = el.Price,
                    OrderId = tempOrder.Id
                };
                _shopContext.OrderDetails.Add(product);
                _shopContext.SaveChanges();
            }
            return true;
        }
    }
}
