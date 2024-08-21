using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelCenter.Web.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Şifreni Onayla")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string? PhoneNumber { get; set; }

        public string? RedirectUrl { get; set; }
        public string? Role { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
