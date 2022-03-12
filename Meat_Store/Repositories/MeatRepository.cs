using Meat_Store.Models;
using Meat_Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meat_Store.Repositories
{
    public class MeatRepository: IAllMeat 
    {
        private readonly shopContext shop_context;

        public MeatRepository(shopContext shopContext)
        {
            this.shop_context = shopContext;
        }
        public IEnumerable<Meat> Meat_of_category => shop_context.Meats.Include(c => c.CategoryId);
        public IEnumerable<Meat> All_Meat => shop_context.Meats;
        public Meat GetProduct(int meat_id) => shop_context.Meats.FirstOrDefault(m => m.Id == meat_id);
    }
}
