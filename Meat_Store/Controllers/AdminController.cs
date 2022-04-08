using Meat_Store.Models;
using Meat_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Store.Controllers
{
    public class AdminController: Controller
    {
        private ShopContext _context;

        public AdminController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult AddMeat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMeat(AdminViewModel model)
        {
            int? id = _context.Meats.FirstOrDefault(m => m.Name == model.meat.Name)?.Id;
            if(id != null)
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", "Така страва вже існує в БД");
                return View(model);
            }
            int? cat = _context.Categories.FirstOrDefault(c => c.CategoryName == model.category.CategoryName)?.Id;
            if(cat == null)
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", "Такої категорії не існує в БД");
                return View(model);
            }
            model.meat.CategoryId = Convert.ToInt32(cat);
            _context.Meats.Add(model.meat);
            _context.SaveChanges();

            return RedirectToAction("Home_Page", "Home");
        }

        public IActionResult DelMeat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DelMeat(Meat meat)
        {
            if (String.IsNullOrEmpty(meat.Name.Trim()))
            {
                return View(new Meat()
                {
                    Name = "Введіть назву страви"
                });
            }

            var new_meat = _context.Meats.FirstOrDefault(m => m.Name == meat.Name.Trim());
            if(new_meat == null)
            {
                return View(new Meat()
                {
                    Name = "Введіть коректну назву"
                });
            }
            _context.Meats.Remove(new_meat);
            _context.SaveChanges();// додати js функцію alert що свідчить про успіх
            string msg = "Ву успішно видалили позицію '" + new_meat.Name + "'.";
            ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", msg);


            return RedirectToAction("Home_Page", "Home");
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            int? id = _context.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName)?.Id;

            if(id != null)
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", "Ця категорія вже існує в БД");
                return View(category);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Home_page", "Home");
        }

        public IActionResult DelCategory()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DelCategory(Category cat)
        {
            if (String.IsNullOrEmpty(cat.CategoryName))
            {
                return View("Введіть назву категорії");
            }
            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == cat.CategoryName.Trim());

            if(category == null)
            {
                return View("Введіть коректно назву категорії");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            string msg = "Ви успішно додали категорію '" + cat.CategoryName.Trim() + "'";
            ViewBag.JavaScriptFunction = string.Format("ShowError('{0}');", msg);

            return RedirectToAction("Home_Page", "Home");
        }

    }
}
