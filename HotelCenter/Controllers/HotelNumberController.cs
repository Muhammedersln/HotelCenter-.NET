using HotelCenter.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public HotelNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var hotelsNumbers = _unitOfWork.HotelNumber.GetAll(includeProperties: "Hotel");
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            HotelNumberVM hotelNumberVM = new HotelNumberVM
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
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
            bool roomNumberExists = _unitOfWork.HotelNumber.Any(h => h.Hotel_Number == hotel.HotelNumber.Hotel_Number);
            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.HotelNumber.Add(hotel.HotelNumber);
                _unitOfWork.Save();
                TempData["success"] = $"Otel başarılı şekilde eklendi.";
                return RedirectToAction("Index");
            }
            if (roomNumberExists)
            {
                TempData["error"] = "Bu oda numarası zaten mevcut.";
            }

            hotel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
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
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                HotelNumber = _unitOfWork.HotelNumber.Get(h => h.Hotel_Number == hotelNumberId)
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
                _unitOfWork.HotelNumber.Update(hotelNumberVM.HotelNumber);
                _unitOfWork.Save();
                TempData["success"] = "Bungalov başarılı şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            hotelNumberVM.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
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
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                HotelNumber = _unitOfWork.HotelNumber.Get(h => h.Hotel_Number == hotelNumberId)
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
            HotelNumber? hotelToDelete = _unitOfWork.HotelNumber.Get(h => h.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);
            if (hotelToDelete is not null)
            {
                _unitOfWork.HotelNumber.Remove(hotelToDelete);
                _unitOfWork.Save();
                TempData["success"] = "Bungalov numarası başarılı şekilde silinidi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Silinecek otel bulunamadı.";
            return View();
        }
    }

}
