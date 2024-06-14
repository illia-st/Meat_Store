using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Meat_Store.Models;

namespace Meat_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ShopContext shopContext;

        public ChartController(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var categories = shopContext.Categories.ToList();
            List<object> catBook = new List<object>();

            catBook.Add(new[] { "Категорія", "Кількість страв" });
            foreach(var cat in categories)
            {
                catBook.Add(new object[] {cat.CategoryName, shopContext.Meats.Count(m => m.CategoryId == cat.Id)});
            }

            return new JsonResult(catBook);
        }
        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var receivings = shopContext.Receives.ToList();

            List<object> delBook = new List<object>();

            delBook.Add(new[] { "Тип отримання", "Кількість отримань" });
            foreach(var rec in receivings)
            {
                delBook.Add(new object[] { rec.Delivery_servise, shopContext.Deliveries.Count(d => d.DeliveryType == rec.Id) });
            }
            
            return new JsonResult(delBook);
        }
        [HttpGet("AmountOfProduct")]
        public JsonResult AmountOfProduct()
        {
            var categories = shopContext.Categories.ToList();

            List<object> list = new List<object>();

            list.Add(new object[] { "Категорія", "Кількість товару за категорією" });
            foreach(var cat in categories)
            {
                var dishes = shopContext.Meats.Where(m => m.CategoryId == cat.Id).ToList();
                int sum = 0;

                foreach(var dish in dishes)
                {
                    sum += dish.Portion;
                }
                list.Add(new object[] {cat.CategoryName, sum});
            }
            return new JsonResult(list);
        }
    }
}
