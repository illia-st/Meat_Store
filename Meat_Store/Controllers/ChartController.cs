using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Meat_Store.Models;

namespace Meat_Store.Controllers
{

    public class ChartController : Controller
    {
        private readonly ShopContext shopContext;

        public ChartController(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }


        public JsonResult JsonData()
        {
            var categories = shopContext.Categories.ToList();
            List<BlogPieChart> catBook = new List<BlogPieChart>();

            //catBook.Add(new[] { "Категорія", "Кількість страв" });
            foreach(var cat in categories)
            {
                catBook.Add(new BlogPieChart {CategoryName = cat.CategoryName, PostCount = shopContext.Meats.Count(m => m.CategoryId == cat.Id)});
            }
            
            return Json( new { JSONList = catBook });
        }
    }
}
