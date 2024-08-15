using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelCenter.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _hostingEnvironment;

        public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
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
                if(hotel.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(hotel.Image.FileName);
                    string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, @"img\BungalovImage");

                    using var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create);
                    hotel.Image.CopyTo(fileStream);

                    hotel.ImageUrl = @"\img\BungalovImage\" + fileName;
                }
                else
                {
                    hotel.ImageUrl = "https://placehold.co/600x400";
                }

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

                if (hotel.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(hotel.Image.FileName);
                    string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, @"img\BungalovImage");

                    if(!string.IsNullOrEmpty(hotel.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, hotel.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create);
                    hotel.Image.CopyTo(fileStream);

                    hotel.ImageUrl = @"\img\BungalovImage\" + fileName;
                }
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

                if (!string.IsNullOrEmpty(hotelToDelete.ImageUrl))
                {
                    string oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, hotelToDelete.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
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
