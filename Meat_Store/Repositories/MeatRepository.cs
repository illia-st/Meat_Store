using Meat_Store.Models;
using Meat_Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meat_Store.Repositories
{
    public class MeatRepository: IAllMeat 
    {
        private readonly ShopContext shopContext;

        public MeatRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }
        public IEnumerable<Meat> Meat_of_category => shopContext.Meats.Include(c => c.CategoryId);
        public IEnumerable<Meat> All_Meat => shopContext.Meats;
        public Meat GetProduct(int meat_id)
        {
            return shopContext.Meats.FirstOrDefault(m => m.Id == meat_id);
        }
    } 
}
