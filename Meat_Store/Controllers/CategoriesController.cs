using Microsoft.AspNetCore.Mvc;
using Meat_Store.Interfaces;
using Meat_Store.Models;
using Meat_Store.ViewModels;
using ClosedXML.Excel;

namespace Meat_Store.Controllers
{
    public class CategoriesController: Controller
    {
        private IAllCategories categories;
        private ShopContext context;

        public CategoriesController(IAllCategories categories, ShopContext context)
        {
            this.categories = categories;
            this.context = context;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                bool if_new_cat = false;
                                var cat = context.Categories.FirstOrDefault(c => c.CategoryName == worksheet.Name);
                                if (cat == null)
                                {
                                    var cat_row = worksheet.Row(2);
                                    string desc = cat_row.Cell(1).Value.ToString();
                                    string img = cat_row.Cell(2).Value.ToString();

                                    var new_cat = new Category()
                                    {
                                        CategoryName = worksheet.Name,
                                        Description = desc,
                                        Img = img
                                    };
                                    context.Categories.Add(new_cat);
                                    context.SaveChanges();
                                    cat = context.Categories.FirstOrDefault(c => c.CategoryName == worksheet.Name);

                                    if_new_cat = true;
                                }
                                int header = 0, skiped = 0;
                                if (if_new_cat)
                                {
                                    skiped = 3;
                                    header = worksheet.Row(skiped).CellsUsed().Count();
                                }
                                else
                                {
                                    skiped = 1;
                                    header = worksheet.Row(skiped).CellsUsed().Count();
                                }

                                foreach (IXLRow row in worksheet.RowsUsed().Skip(skiped))
                                {
                                    var new_info = new List<string>();
                                    int used_cells = row.CellsUsed().Count();
                                    for (int i = 1; i <= used_cells; i++)
                                    {
                                        new_info.Add(row.Cell(i).Value.ToString());
                                    }

                                    if (if_new_cat || used_cells == header)
                                    {
                                        var meat = new Meat()
                                        {
                                            Name = new_info[0],
                                            Portion = Convert.ToInt32(new_info[1]),
                                            Price = Convert.ToInt32(new_info[2]),
                                            ShortDesc = new_info[3],
                                            LongDesc = new_info[4],
                                            Img = new_info[5],
                                            Error_msg = new_info[6],
                                            SizeOfPortion = Convert.ToInt32(new_info[7]),
                                            CategoryId = context.Categories.FirstOrDefault(c => c.CategoryName == cat.CategoryName).Id
                                        };
                                        context.Meats.Add(meat);
                                        continue;
                                    }

                                    int am = context.Meats.FirstOrDefault(m => m.Name == new_info[0]).Portion;
                                    if(am >= Convert.ToInt32(new_info[1]))
                                    {
                                        continue;
                                    }
                                    context.Meats.FirstOrDefault(m => m.Name == new_info[0]).Portion += Convert.ToInt32(new_info[1]) - am;
                                    
                                }
                            }
                        }
                    }
                }
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var categories = context.Categories.ToList();
                foreach(var cat in categories)
                {

                    var worksheet = workbook.Worksheets.Add(cat.CategoryName);
                    worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.BlueGray;

                    worksheet.Cell("A1").Value = "Name";
                    worksheet.Cell("B1").Value = "Portion";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var meats = context.Meats.Where(m => m.CategoryId == cat.Id).ToList();

                    int i = 2;

                    foreach(var m in meats)
                    {
                        worksheet.Cell(i, 1).Value = m.Name;
                        worksheet.Cell(i, 2).Value = m.Portion;
                        ++i;
                    }
                    var rngTable = worksheet.Range("A1:B" + (meats.Count + 1));
                    rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    worksheet.Columns().AdjustToContents();
                }
                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Наявна кількість товару.xlsx"
                    };
                }
            }
        }
    }
}
