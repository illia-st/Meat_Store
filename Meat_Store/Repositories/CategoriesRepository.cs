using Meat_Store.Models;
using Meat_Store.Interfaces;

namespace Meat_Store.Repositories
{
    public class CategoriesRepository: IAllCategories
    {
        private readonly ShopContext shopContext;

        public CategoriesRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public IEnumerable<Category> All_Categories => shopContext.Categories;
    }
}
