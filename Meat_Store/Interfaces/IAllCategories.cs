using Meat_Store.Models;

namespace Meat_Store.Interfaces
{
    public interface IAllCategories
    {
        IEnumerable<Category> All_Categories { get; }
    }
}
