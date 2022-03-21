using Meat_Store.Models;

namespace Meat_Store.Interfaces
{
    public interface IAllOrders
    {
        IEnumerable<Meat> GetOrder { get; set; }
        bool CreateOrder(Order order);
    }
}
