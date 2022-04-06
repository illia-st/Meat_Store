using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Meat_Store.Models
{
    public class ShopCart
    {
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ShopCart(ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = shopContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopitems { get; set; }
        public static ShopCart GetCart(IServiceProvider serviceProvider)
        {
            ISession session =
                serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<ShopContext>();
            var httpContext = serviceProvider.GetService<IHttpContextAccessor>();
            var shopCartid = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartid);

            return new ShopCart(context, httpContext) { ShopCartId = shopCartid };

        }

        public void AddToCart(Meat meat, int amount)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            for (int i = 0; i < amount; ++i)
            {
                this._context.Add(new ShopCartItem
                {
                    ShopCartId = userId == null ? ShopCartId : userId,
                    MeatId = meat.Id,
                    Price = meat.Price,
                    Name = meat.Name
                });
            }
            _context.SaveChanges();
        }
        public void DeleteFromCart(string Name)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (String.IsNullOrEmpty(userId))
            {
                var temp = _context.ShopCartItems.FirstOrDefault(i => i.Name == Name && i.ShopCartId == ShopCartId);
                _context.ShopCartItems.Remove(temp);
            }
            else
            {
                var temp = _context.ShopCartItems.FirstOrDefault(i => i.Name == Name && i.ShopCartId == userId);
                _context.ShopCartItems.Remove(temp);
            }
            _context.SaveChanges();
        }
        public List<ShopCartItem> getShopCartItems()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrEmpty(userId))
            {
                return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).ToList();
            }

            return _context.ShopCartItems.Where(c => c.ShopCartId == userId).ToList();
        }
        public void ClearShopCart()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = new List<ShopCartItem>();
            if (String.IsNullOrEmpty(userId))
            {
                items = _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).ToList();
            }
            else
            {
                items = _context.ShopCartItems.Where(c => c.ShopCartId == userId).ToList();
            }
            foreach (var item in items)
            {
                _context.ShopCartItems.Remove(item);
            }
            _context.SaveChanges();
        }
    }
}