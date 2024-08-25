using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Models;
using HotelCenter.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM model = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity"),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                Nights = 1
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity");
            foreach (var hotel in homeVM.HotelList)
            {
                if(hotel.Id % 2 == 0)
                {
                    hotel.IsAvailable = false;
                }
            }
            return View(homeVM);
        }

        public IActionResult GetHotelsByDate(DateOnly checkInDate, int nights)
        {
            var hotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity");
            foreach (var hotel in hotelList)
            {
                if (hotel.Id % 2 == 0)
                {
                    hotel.IsAvailable = false;
                }
            }

            HomeVM homeVM = new()
            {
                HotelList = hotelList,
                CheckInDate = checkInDate,
                Nights = nights
            };
            return PartialView("_HotelList",homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
