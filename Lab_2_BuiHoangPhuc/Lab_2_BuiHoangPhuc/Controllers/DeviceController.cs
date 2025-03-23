using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_2_BuiHoangPhuc.Controllers
{
    public class DeviceController : Controller
    {

        private static List<Device> devices = new List<Device>
        {
        new Device { Id = 1, DeviceName = "Ekko E122", DeviceCode = "D001", CategoryId = 2, Status = "In use", DateOfEntry = new DateTime(2025, 3, 19) },
        new Device { Id = 2, DeviceName = "Gygabyte GE", DeviceCode = "D002", CategoryId = 1, Status = "Under maintenance", DateOfEntry = new DateTime(2025, 8, 9) },
        new Device { Id = 3, DeviceName = "Bàn phím cơ Hi", DeviceCode = "D003", CategoryId = 3, Status = "Broken", DateOfEntry = new DateTime(2025, 9, 1) },
        new Device { Id = 5, DeviceName = "Acer Nitro V", DeviceCode = "D004", CategoryId = 1, Status = "In use", DateOfEntry = new DateTime(2025, 2, 19) },
        new Device { Id = 6, DeviceName = "Bàn phím cơ AKKO", DeviceCode = "D005", CategoryId = 3, Status = "Broken", DateOfEntry = new DateTime(2025, 2, 20) },
        new Device { Id = 7, DeviceName = "Logitech G102", DeviceCode = "D006", CategoryId = 2, Status = "Under maintenance", DateOfEntry = new DateTime(2025, 3, 8) },
        new Device { Id = 8, DeviceName = "Logitech G302", DeviceCode = "D007", CategoryId = 2, Status = "Under maintenance", DateOfEntry = new DateTime(2025, 3, 8) }
        };

        private static List<Category> categories = new List<Category>
        {
                    new Category { Id = 1, Name = "Laptop" },
                    new Category { Id = 2, Name = "Mouse" },
                    new Category { Id = 3, Name = "Keyboard" }
        };
        public IActionResult Index(int? categoryId)
        {
            var filteredDevices = categoryId.HasValue
                ? devices.Where(d => d.CategoryId == categoryId.Value).ToList()
                : devices;

            var devicesWithCategoryNames = filteredDevices.Select(d => new
            {
                d.Id,
                d.DeviceName,
                d.DeviceCode,
                CategoryName = categories.FirstOrDefault(c => c.Id == d.CategoryId)?.Name ?? "Unknown",
                d.Status,
                d.DateOfEntry
            }).ToList();

            ViewBag.Devices = devicesWithCategoryNames;
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Device newDevice)
        {
            ViewBag.Categories = categories;
            newDevice.Id = devices.Max(d => d.Id) + 1;
            devices.Add(newDevice);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device == null) return NotFound();

            ViewBag.Categories = categories;
            return View(device);
        }

        [HttpPost]
        public IActionResult Edit(Device updatedDevice)
        {
            ViewBag.Categories = categories;
            var device = devices.FirstOrDefault(d => d.Id == updatedDevice.Id);
            if (device == null) return NotFound();
            device.DeviceName = updatedDevice.DeviceName;
            device.DeviceCode = updatedDevice.DeviceCode;
            device.CategoryId = updatedDevice.CategoryId;
            device.Status = updatedDevice.Status;
            device.DateOfEntry = updatedDevice.DateOfEntry;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device == null) return NotFound();

            devices.Remove(device);
            return RedirectToAction("Index");
        }
    }
}
