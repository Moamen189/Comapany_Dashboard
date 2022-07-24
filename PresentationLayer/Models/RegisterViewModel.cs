using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "Email is Required")]
        [EmailAddress (ErrorMessage ="Invailed Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength (5 , ErrorMessage ="Minimum Length is 5")]

        public string Password { get; set; }

        [Required(ErrorMessage = " Confirm Password is Required")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5")]
        [Compare("Password", ErrorMessage ="Password is Diffrent Please Try Again")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}
