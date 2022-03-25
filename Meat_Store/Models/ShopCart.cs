using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
namespace Meat_Store.Models
{
    public class ShopCart
    {
        private readonly ShopContext _context;
        public ShopCart(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopitems { get; set; }
        public static ShopCart GetCart(IServiceProvider serviceProvider)
        {
            ISession session =
                serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<ShopContext>();
            var shopCartid = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartid);

            return new ShopCart(context) { ShopCartId = shopCartid };

        }

        public void AddToCart(Meat meat, int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                this._context.Add(new ShopCartItem
                {
                    ShopCartId = ShopCartId,
                    MeatId = meat.Id,
                    Price = meat.Price,
                    Name = meat.Name
                });
            }
            _context.SaveChanges();
        }
        public List<ShopCartItem> getShopCartItems()
        {
            var temp = _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).ToList();
            
            return temp;
        }
        public void ClearShopCart()
        {
            var items = _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId);
            foreach(var item in items)
            {
                _context.ShopCartItems.Remove(item);
            }
            _context.SaveChanges();
            this.listShopitems.Clear();
        }
    }
}
