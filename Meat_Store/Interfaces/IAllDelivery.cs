using Meat_Store.Models;

namespace Meat_Store.Interfaces
{
    public interface IAllDelivery
    {
        IEnumerable<string> types { get; set; } 
    }
}
