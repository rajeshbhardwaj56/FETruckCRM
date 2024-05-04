using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class FactoringCompanyModel
    {

        public Int64 ShipperID { get; set; }

        [Required(ErrorMessage = "Factoring Company Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        public int StatusInd { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string strStatusInd { get; set; }
        public string InternalNotes { get; set; }
        [Required(ErrorMessage = "Remittance Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string RemittanceEmail { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool Isdeleted { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CurrencyList { get; set; }
        public List<SelectListItem> PaymentTermsList { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string strCountryID { get; set; }
        public Int64 CountryID { get; set; }
       // [Required(ErrorMessage = "State is required")]
        public string strStateID { get; set; }
        public Int64 StateID { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }

        public string ContactName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactEmail { get; set; }

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
        public string Fax { get; set; }
        public Int64 ShippingHours { get; set; }

        [MaxLength(999)]
        [MinLength(1)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string strShippingHours { get; set; }
        public Int64 Appointments { get; set; }
        public string strAppointments { get; set; }
        public string MajorInspectionDirections { get; set; }
        public int ShipperType { get; set; }


        public long FCID { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PrimaryContact { get; set; }
        
        public string TelephoneExtn { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string SecondaryContact { get; set; }
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid telephone number")]
        public string SecTelephone { get; set; }
        public string SecTelephoneExtn { get; set; }
        [Required(ErrorMessage = "Please Select Currency Settings")]

        public int CurrencySettings { get; set; }
        [Required(ErrorMessage = "Please Select Payment Terms")]
        public int PaymentTerms { get; set; }
        public string TaxID { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public int Status { get; set; }
        public bool IsDeletedInd { get; set; }



    }



}