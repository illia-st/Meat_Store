using Meat_Store.Models;
using Meat_Store.ViewModels;

namespace Meat_Store.Interfaces
{
    public interface IAllOrders
    {
        IEnumerable<Meat> GetOrder { get; set; }
        bool CreateOrder(Order order, Delivery delivery);
    }
}
