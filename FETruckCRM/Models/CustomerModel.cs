using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class CustomerModel
    {

        public Int64 CustomerID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string CustomerName { get; set; }
        public string StateName { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Isdeleted { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> BillingStateList { get; set; }
        public List<SelectListItem> PaymentTermsList { get; set; }


        public List<SelectListItem> AppointmentsList { get; set; }
        public List<SelectListItem> MCFFList { get; set; }
        public List<SelectListItem> FSCTypeList { get; set; }
        public List<SelectListItem> CurrencyList { get; set; }
        public List<SelectListItem> SaleRepList { get; set; }

        public List<CareerEquipmentTypeModel> EquipmentList { get; set; }
        public List<SelectListItem> FactoringCompanyListing { get; set; }
        public string CustomerNo { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public long CountryID { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string strCountryID { get; set; }
        public long StateID { get; set; }
        [Required(ErrorMessage = "State is required")]

        public string strStateID { get; set; }
        [Required(ErrorMessage = "City is required")]

        public string City { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; }
        public bool ISBillingAddSameAsMailing { get; set; }
        [Required(ErrorMessage = "Billing Address is required")]
        public string BillAddress { get; set; }
        public string Billingaddress2 { get; set; }
        public string BillingAddress3 { get; set; }
        public long BillingCountryID { get; set; }
        [Required(ErrorMessage = "Billing Country is required")]

        public string strBillingCountryID { get; set; }
        public long BillingStateID { get; set; }
        [Required(ErrorMessage = "Billing State is required")]

        public string strBillingStateID { get; set; }
        [Required(ErrorMessage = "Billing City is required")]

        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Billing Zip is required")]

        public string BillingZip { get; set; }
        public string PrimaryContact { get; set; }
        [Required(ErrorMessage = "Telephone is required")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telephone number is not valid.")]
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        [Required(ErrorMessage = "Email is required")]

        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Toll free number is not valid.")]
        public string TollFree { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Fax number is not valid.")]
        public string Fax { get; set; }
        public string SecondaryContact { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string SecondaryEmail { get; set; }
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string BillingEmail { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Billing Telephone number is not valid.")]
        public string BillingTelephone { get; set; }
        public string BillingTelephoneExt { get; set; }
        public string MCFF { get; set; }
        public string MCFFType { get; set; }
        public string URS { get; set; }
        public bool ISBlackListed { get; set; }
        public bool ISBroker { get; set; }
        public int CurrencySettingID { get; set; }
        public string strCurrencySettingID { get; set; }
        [Required(ErrorMessage = "Payment Terms is required")]
        public int PaymentTerms { get; set; }
        [Required(ErrorMessage = "Credit Limit is required")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string strCreditLimit { get; set; }
        public decimal CreditLimit { get; set; }
        public string SalesRep { get; set; }
        public long FactoringCompany { get; set; }
        public string strFactoringCompany { get; set; }
        public string FederalID { get; set; }
        public string WorkersComp { get; set; }
        public string WebsiteURL { get; set; }
        public bool NumberonInvoice { get; set; }
        public bool CustomerRate { get; set; }
        public bool duplicate { get; set; }
        public bool AddAsShipper { get; set; }
        public bool AddAsConsignee { get; set; }
        public string InternalNotes { get; set; }
        public bool ShowMilesOnQuote { get; set; }
        public string RateType { get; set; }
        public int FSCType { get; set; }
        public string strFSCType { get; set; }
        public decimal FSCRate { get; set; }
        public string strFSCRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public long LastModifiedByID { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string strStatus { get; set; }
        public string strApprovalStatus { get; set; }
        public int ApprovalStatus { get; set; }
        public bool IsDeletedInd { get; set; }
        public bool IsOnHold { get; set; }

        public decimal TotalCreditLimitUsed { get; set; }

        public decimal RemainingCreditLimit { get; set; }

        public int AssignTo { get; set; }
    }

    public class CustomerNotificationsModel
    {

        public long CustomerNotificationID { get; set; }
        public long CustomerID { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string Email { get; set; }
        public bool SendCopyToInitiatingUser { get; set; }
        public bool ENDispatched { get; set; }
        public bool ENLoading { get; set; }
        public bool ENOnRoute { get; set; }
        public bool ENUnloading { get; set; }
        public bool ENInYard { get; set; }
        public bool ENDelivered { get; set; }
        public bool ENCompleted { get; set; }
        public bool IsDeletedInd { get; set; }

        public string strSendCopyToInitiatingUser { get; set; }
        public string strENDispatched { get; set; }
        public string strENLoading { get; set; }
        public string strENOnRoute { get; set; }
        public string strENUnloading { get; set; }
        public string strENInYard { get; set; }
        public string strENDelivered { get; set; }
        public string strENCompleted { get; set; }
    }



}