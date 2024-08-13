using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using HotelCenter.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var hotelsNumbers = _context.HotelNumbers.Include(h => h.Hotel).ToList();
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            HotelNumberVM hotelNumberVM = new HotelNumberVM
            {
                HotelList = _context.Hotels.ToList().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                })
            };
            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Create(HotelNumberVM hotel)
        {
            bool roomNumberExists = _context.HotelNumbers.Any(h => h.Hotel_Number == hotel.HotelNumber.Hotel_Number);
            if (ModelState.IsValid && !roomNumberExists)
            {
                _context.HotelNumbers.Add(hotel.HotelNumber);
                _context.SaveChanges();
                TempData["success"] = $"Otel başarılı şekilde eklendi.";
                return RedirectToAction("Index");
            }
            if (roomNumberExists)
            {
                TempData["error"] = "Bu oda numarası zaten mevcut.";
            }

            hotel.HotelList = _context.Hotels.ToList().Select(h => new SelectListItem
            {
                Text = h.Name,
                Value = h.Id.ToString()
            });

            return View(hotel);
        }

        public IActionResult Update(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new HotelNumberVM
            {
                HotelList = _context.Hotels.ToList().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                HotelNumber = _context.HotelNumbers.FirstOrDefault(h => h.Hotel_Number == hotelNumberId)
            };
            if (hotelNumberVM.HotelNumber == null)
            { 
                return RedirectToAction("Error","Home");
            }
            return View(hotelNumberVM);
        }
        
        [HttpPost]
        public IActionResult Update(HotelNumberVM hotelNumberVM)
        {
            if (ModelState.IsValid)
            {
                _context.HotelNumbers.Update(hotelNumberVM.HotelNumber);
                _context.SaveChanges();
                TempData["success"] = "Bungalov başarılı şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            hotelNumberVM.HotelList = _context.Hotels.ToList().Select(h => new SelectListItem
            {
                Text = h.Name,
                Value = h.Id.ToString()
            });
            return View(hotelNumberVM);
        }

        public IActionResult Delete(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new HotelNumberVM
            {
                HotelList = _context.Hotels.ToList().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                HotelNumber = _context.HotelNumbers.FirstOrDefault(h => h.Hotel_Number == hotelNumberId)
            };
            if (hotelNumberVM.HotelNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(hotelNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(HotelNumberVM hotelNumberVM)
        {
            HotelNumber? hotelToDelete = _context.HotelNumbers.FirstOrDefault(h => h.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);
            if (hotelToDelete is not null)
            {
                _context.HotelNumbers.Remove(hotelToDelete);
                _context.SaveChanges();
                TempData["success"] = "Bungalov numarası başarılı şekilde silinidi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Silinecek otel bulunamadı.";
            return View();
        }
    }

}
