using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FETruckCRM.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(200)]
        [EmailAddress(ErrorMessage ="Please enter a valid email id.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "OTP is required")]
        //[DataType(DataType.Password)]
        //[StringLength(15, MinimumLength = 6)]
        //[Display(Name = "OTP: ")]
        public string OTP { get; set; }
    }
}