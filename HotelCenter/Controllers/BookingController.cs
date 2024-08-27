using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace HotelCenter.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult FinalizeBooking(int hotelId, DateOnly date, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId= claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId);


            Booking booking = new()
            {
                HotelId = hotelId,
                Hotel = _unitOfWork.Hotel.Get(u => u.Id == hotelId, includeProperties:"HotelAmenity"),
                CheckInDate = date,
                Nights = nights,
                CheckOutDate = date.AddDays(nights),
                UserId = userId,
                Phone=user.PhoneNumber,
                Email=user.Email,
                Name=user.Name

            };
            booking.TotalCost = booking.Hotel.Price * nights;

            return View(booking);
        }
    }
}
