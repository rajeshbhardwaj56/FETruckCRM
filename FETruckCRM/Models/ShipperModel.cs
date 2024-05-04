using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class ShipperModel
    {

        public Int64 ShipperID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string ShipperName { get; set; }
        public int StatusInd { get; set; }

       [Required(ErrorMessage = "Status is required")]
        public string strStatusInd { get; set; }
        public string Notes { get; set; }
        public string InternalNotes { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }
        
        public DateTime LastModifiedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool Isdeleted { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> AppointmentsList { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string AddressL1 { get; set; }
        public string Address { get; set; }
        public string AddressL2 { get; set; }
        public string AddressL3 { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string strCountryID { get; set; }
        public Int64 CountryID { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string strStateID { get; set; }
        public Int64 StateID { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }

        public string ContactName { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactEmail { get; set; }

        //[Required(ErrorMessage = "You must provide a phone number")]
        [Required(ErrorMessage = "Telephone is required")]
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid telephone number")]
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        [Display(Name = "Toll Free")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid toll Free number")]
        public string TollFree { get; set; }
        [Display(Name = "Fax")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Fax number")]
        public string  Fax { get; set; }
        public string ShippingHours { get; set; }

        //[MaxLength(999)]
        //[MinLength(1)]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string strShippingHours { get; set; }
        public Int64 Appointments { get; set; }
        public string strAppointments { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string MajorInspectionDirections { get; set; }
        public bool DuplicateInfo { get; set; }
        public int ShipperType { get; set; }
        public Int64 CustomerID { get; set; }
        public Int64 TotalRecords { get; set; }
        

    }



}