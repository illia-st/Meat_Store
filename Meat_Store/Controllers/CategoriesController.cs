using Microsoft.AspNetCore.Mvc;
using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;

namespace Meat_Store.Controllers
{
    public class CategoriesController: Controller
    {
        private IAllCategories categories;

        public CategoriesController(IAllCategories categories)
        {
            this.categories = categories;
        }

        public ViewResult ViewCategories()
        {
            IEnumerable<Category> all_categories = categories.All_Categories.OrderBy(c => c.Id);

            var viewCategories = new CategoriesViewModel()
            {
                All_categories = all_categories
            };

            return View(viewCategories);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
