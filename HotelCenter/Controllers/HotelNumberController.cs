using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelCenter.Web.Controllers
{
    public class HotelNumberController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HotelNumberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hotelsNumbers = _context.HotelNumbers.ToList();
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HotelNumber hotel)
        {
            ModelState.Remove("Hotel");
            if (ModelState.IsValid)
            {
                _context.HotelNumbers.Add(hotel);
                _context.SaveChanges();
                TempData["success"] = $"Otel başarılı şekilde eklendi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Otel eklenirken bir hata oluştu.";
            return View(hotel);
        }

        public IActionResult Update(int hotelId)
        {
            Hotel? hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            { 
                return RedirectToAction("Error","Home");
            }
            return View(hotel);
        }
        
        [HttpPost]
        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid && hotel.Id>0)
            {
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
                TempData["success"] = $"{hotel.Name} başarılı şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Güncellenecek otel bulunamadı.";
            return View(hotel);
        }
        public IActionResult Delete(int hotelId)
        {
            Hotel? hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(hotel);
        }
        [HttpPost]
        public IActionResult Delete(Hotel hotel)
        {
            Hotel? hotelToDelete = _context.Hotels.FirstOrDefault(h => h.Id == hotel.Id);
            if (hotelToDelete is not null)
            {
                _context.Hotels.Remove(hotelToDelete);
                _context.SaveChanges();
                TempData["success"] = $"{hotelToDelete.Name} başarılı şekilde silinidi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Silinecek otel bulunamadı.";
            return View(hotel);
        }
    }

}
