using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_2_BuiHoangPhuc.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User>
        {
        new User { Id = 1, FullName = "Phuc Bui", Email = "phucbui@gmail.com", PhoneNumber = "0123456789" },
        new User { Id = 2, FullName = "Phuc 26", Email = "phuc26@gmail.com", PhoneNumber = "9876543210" }
        };
        public IActionResult Index()
        {
            ViewBag.Users = users;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User newUser)
        {
            newUser.Id = users.Max(c => c.Id) + 1;
            users.Add(newUser);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user == null) return NotFound();

            if (ModelState.IsValid)
            {
                user.FullName = updatedUser.FullName;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;
                return RedirectToAction("Index");
            }
            return View(updatedUser);
        }

        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return RedirectToAction("Index");
        }
    }
}
