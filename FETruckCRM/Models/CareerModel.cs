using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FETruckCRM.Models
{
    public class CareerModel
    {


        public long CareerID { get; set; }
        [Required(ErrorMessage = "Career Name is required")]
        [StringLength(100)]
        [Display(Name = "Career Name: ")]
        public string CareerName { get; set; }
        public string Username { get; set; }
        
        public string Password { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [Display(Name = "Address: ")]
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public long CountryID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(100)]
        [Display(Name = "Country: ")]
        public string strCountryID { get; set; }
        public string CountryName { get; set; }
        public long StateID { get; set; }
        [Required(ErrorMessage = "State is required")]
        [StringLength(100)]
        [Display(Name = "State: ")]
        public string strStateID { get; set; }
        public string StateName { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        [Display(Name = "City: ")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        [StringLength(10)]
        [Display(Name = "Zip Code: ")]
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(20)]
        [Display(Name = "Telephone: ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Toll Free no is not valid.")]
        public string TollFree { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Fax no is not valid.")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "Payment Terms is required")]
        public int PaymentTerms { get; set; }
        public string TaxID { get; set; }
        public int LoadTypeID { get; set; }
        public string strLoadTypeID { get; set; }
        public string MCFFType { get; set; }
        public string MCFFNo { get; set; }
        public string URS { get; set; }
        public string DOT { get; set; }
        public string Notes { get; set; }
        public bool IsBlacklisted { get; set; }
        public bool IsCareerAsBlacklisted { get; set; }
        public bool Corporation { get; set; }
        [Required(ErrorMessage = "Factoring company is required")]

        public int FactoringCompany { get; set; }
        public string strFactoringCompany { get; set; }
        public string LiabilityCompany { get; set; }
        public string PolicyNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string strExpiryDate { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string LibCompanyTelephone { get; set; }

        public string LibCompanyTelephoneExt { get; set; }
        public string LibCompContact { get; set; }
        public decimal Liability { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value upto 2 decimal places")]

        public string strLiability { get; set; }
        public string LibCompNotes { get; set; }
        public bool IsAICompSameAsLiabInsurance { get; set; }
        public string AutoInsuranceCompany { get; set; }
        public string AICPolicyNo { get; set; }
        public DateTime AICExpiryDate { get; set; }
        public string strAICExpiryDate { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string AICTelephoneNo { get; set; }
        public string AICTelephoneExt { get; set; }
        public string AICContact { get; set; }
        public decimal AICLiability { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value upto 2 decimal places")]

        public string strAICLiability { get; set; }
        public string AICNotes { get; set; }
        public bool IsCCSameAsLiab { get; set; }
        public string CargoCompany { get; set; }
        public string CCPolicyNo { get; set; }
        public DateTime CCExpiryDate { get; set; }
        public string strCCExpiryDate { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string CCTelephoneNo { get; set; }
        public string CCtelephoneExt { get; set; }
        public string CCContact { get; set; }
        public decimal CCCargoIns { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value upto 2 decimal places")]

        public string strCCCargoIns { get; set; }
        public string CCNotes { get; set; }
        public string CCWSIB { get; set; }
        public string FMCSAInsuranceCompany { get; set; }
        public string FICPolicyNo { get; set; }
        public DateTime FICExpDate { get; set; }
        public string strFICExpDate { get; set; }
        public string FICType { get; set; }
        public decimal FICCoverage { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value upto 2 decimal places")]
        public string strFICCoverage { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string FICTelephone { get; set; }
        public int FICAMBestRating { get; set; }
        public string AccountingPrimaryName { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string AccountingPrimaryTelephone { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string AccountingPrimaryEmail { get; set; }
        public string AccountingSecondaryName { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string AccountingSecondaryTelephone { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string AccountingSecondaryEmail { get; set; }
        public long CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }
        public long LastModifyByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string strLastModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool StatusInd { get; set; }
        [Required(ErrorMessage = "Status is required")]

        public string strStatusInd { get; set; }
        public int ApprovalStatus { get; set; }
        public string strApprovalStatus { get; set; }
        public bool IsDeletedInd { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> LoadTypeList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> PaymentTermsList { get; set; }
        
        public List<SelectListItem> MCFFList { get; set; }
        public List<CareerEquipmentTypeModel> EquipmentList { get; set; }
        public List<SelectListItem> FactoringCompanyListing { get; set; }
        
        public string CareerNotes { get; set; }
        public int SizeOfFleet { get; set; }

    }


    public class CareerListModel
    {
        public long CareerID { get; set; }
        [Required(ErrorMessage = "Career Name is required")]
        [StringLength(100)]
        [Display(Name = "Career Name: ")]
        public string CareerName { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [Display(Name = "Address: ")]
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public long CountryID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(100)]
        [Display(Name = "Country: ")]
        public string strCountryID { get; set; }
        public string CountryName { get; set; }
        public long StateID { get; set; }
        [Required(ErrorMessage = "State is required")]
        [StringLength(100)]
        [Display(Name = "State: ")]
        public string strStateID { get; set; }
        public string StateName { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        [Display(Name = "City: ")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        [StringLength(10)]
        [Display(Name = "Zip Code: ")]
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(20)]
        [Display(Name = "Telephone: ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        
        public string MCFFNo { get; set; }
        public string LoadType { get; set; }
       
        public long CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }
        public long LastModifyByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string strLastModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool StatusInd { get; set; }
        [Required(ErrorMessage = "Status is required")]

        public string strStatusInd { get; set; }
        public int ApprovalStatus { get; set; }
        public string strApprovalStatus { get; set; }
        public bool IsDeletedInd { get; set; }
        public long TotalRecords { get; set; }


    }

    public class CareerEquipmentTypeModel
    {

        public Int64 CareerEquipmentTypeID { get; set; }
        public Int64 EquipmentTypeID { get; set; }
        public Int64 CareerID { get; set; }
        
        public string EquipmentTypeName { get; set; }
        public bool StatusInd { get; set; }
        public bool IsChecked { get; set; }
        public int Quantity { get; set; }

        public string strStatusInd { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool Isdeleted { get; set; }
        public List<SelectListItem> StatusList { get; set; }


    }
}