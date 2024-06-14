using Meat_Store.Models;

namespace Meat_Store.Interfaces
{
    public interface IAllMeat
    {
        IEnumerable<Meat> Meat_of_category { get; }
        IEnumerable<Meat> All_Meat { get; }
        Meat GetProduct(int meat_id);
    }
}
