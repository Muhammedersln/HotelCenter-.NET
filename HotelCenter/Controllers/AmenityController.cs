using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Application.Common.Utility;
using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using HotelCenter.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelCenter.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Hotel");
            return View(amenities);
        }
        public IActionResult Create()
        {
            AmenityVM AmenityVM = new AmenityVM
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                })
            };
            return View(AmenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM hotel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(hotel.Amenity);
                _unitOfWork.Save();
                TempData["success"] = $"Başarılı şekilde eklendi.";
                return RedirectToAction("Index");
            }

            hotel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
            {
                Text = h.Name,
                Value = h.Id.ToString()
            });

            return View(hotel);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM AmenityVM = new ()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(h => h.Id == amenityId)
            };
            if (AmenityVM.Amenity == null)
            { 
                return RedirectToAction("Error","Home");
            }
            return View(AmenityVM);
        }
        
        [HttpPost]
        public IActionResult Update(AmenityVM AmenityVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(AmenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Başarılı şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            AmenityVM.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
            {
                Text = h.Name,
                Value = h.Id.ToString()
            });
            return View(AmenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM AmenityVM = new AmenityVM
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(h => h.Id == amenityId)
            };
            if (AmenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(AmenityVM);
        }
        [HttpPost]
        public IActionResult Delete(AmenityVM AmenityVM)
        {
            Amenity? hotelToDelete = _unitOfWork.Amenity.Get(h => h.Id == AmenityVM.Amenity.Id);
            if (hotelToDelete is not null)
            {
                _unitOfWork.Amenity.Remove(hotelToDelete);
                _unitOfWork.Save();
                TempData["success"] = "Bungalov  başarılı şekilde silinidi.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Silinecek otel bulunamadı.";
            return View();
        }
    }

}
