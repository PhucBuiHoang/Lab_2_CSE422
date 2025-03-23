using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_2_BuiHoangPhuc.Controllers
{
    public class CategoryController : Controller
    {
        private static List<Category> categories = new List<Category>
        {
                    new Category { Id = 1, Name = "Laptop" },
                    new Category { Id = 2, Name = "Mouse" },
                    new Category { Id = 3, Name = "Keyboard" }
         };
        public IActionResult Index()
        {
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
                newCategory.Id = categories.Max(c => c.Id) + 1;
                categories.Add(newCategory);
                return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {
            var category = categories.FirstOrDefault(c => c.Id == updatedCategory.Id);
            if (category == null) return NotFound();

            if (ModelState.IsValid)
            {
                category.Name = updatedCategory.Name;
                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }

        public IActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            categories.Remove(category);
            return RedirectToAction("Index");
        }
    }
}
