using HotelCenter.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelCenter.Web.ViewModels
{
    public class HotelNumberVM
    {
        public HotelNumber? HotelNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? HotelList { get; set; }
    }
}
