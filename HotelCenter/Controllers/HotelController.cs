using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelCenter.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var hotels = _unitOfWork.Hotel.GetAll();
            return View(hotels);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Hotel.Add(hotel);
                _unitOfWork.Save();
                TempData["success"] = $"{hotel.Name} başarılı şekilde eklendi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Otel eklenirken bir hata oluştu.";
            return View(hotel);
        }

        public IActionResult Update(int hotelId)
        {
            Hotel? hotel = _unitOfWork.Hotel.Get(h => h.Id == hotelId);
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
                _unitOfWork.Hotel.Update(hotel);
                _unitOfWork.Save();
                TempData["success"] = $"{hotel.Name} başarılı şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Güncellenecek otel bulunamadı.";
            return View(hotel);
        }
        public IActionResult Delete(int hotelId)
        {
            Hotel? hotel = _unitOfWork.Hotel.Get(h => h.Id == hotelId);
            if (hotel == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(hotel);
        }
        [HttpPost]
        public IActionResult Delete(Hotel hotel)
        {
            Hotel? hotelToDelete = _unitOfWork.Hotel.Get(h => h.Id == hotel.Id);
            if (hotelToDelete is not null)
            {
                _unitOfWork.Hotel.Remove(hotelToDelete);
                _unitOfWork.Save();
                TempData["success"] = $"{hotelToDelete.Name} başarılı şekilde silinidi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Silinecek otel bulunamadı.";
            return View(hotel);
        }
    }

}
