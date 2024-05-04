using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class PreferencesModel
    {
        public long PreferenceID { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public string PrimaryContactName { get; set; }
        public string Telephone { get; set; }
        public string TelePhoneExt { get; set; }
        public string TollFree { get; set; }
        public string Fax { get; set; }
        public string FEINumber { get; set; }
        public string Currency { get; set; }
        public string DateFormat { get; set; }
        public string CalenderFormat { get; set; }
        public string MileageSystem { get; set; }
        public string AvoidTollRoadsMiles { get; set; }
        public string OpenBorderMiles { get; set; }
        public string CompanyLogo { get; set; }
        public string Address { get; set; }
        public string CountryID { get; set; }
        public string StateID { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string InvoiceSequencing { get; set; }
        public string NextLoadNumber { get; set; }
        public string NextInvoiceNumber { get; set; }
        public string NextQuoteNumber { get; set; }
        public string DispatcherTitle { get; set; }
        public string IsUsecompanyEmailOnReport { get; set; }
        public string CompanyEmail { get; set; }
        public string ShowPickupandDeliveryInfo { get; set; }
        public string ShowDeliveryPOonInvoice { get; set; }
        public string PrefferedEnvelopsize { get; set; }
        public string ShowITSDispatchonPrintOut { get; set; }
        public string SuggestStartingLocationOfTruck { get; set; }
        public string TruckStop { get; set; }
        public string Unavailabletrucks { get; set; }
        public string WorkOrder { get; set; }
        public string Show13MonthVs12MonthData { get; set; }
        public string SearchByShipDate { get; set; }
        public string HighlightRowsBasedonLoadStatus { get; set; }
        public string TimeZone { get; set; }
        public string PaginationRow { get; set; }
        public string AccountingManager { get; set; }
        public string LiveTypeSearchBy { get; set; }
        public string LiveTypeSearchRow { get; set; }
        public string EmailFooter { get; set; }
        [Required(ErrorMessage ="Standard Invoice Notes are Required.")]
        public string StandardInvoiceNotes { get; set; }
        [Required(ErrorMessage = "Standard Load Sheet Notes are Required.")]

        public string StandardLoadSheetNotes { get; set; }
        [Required(ErrorMessage = "Standard Customer Sheet Notes are Required.")]

        public string StandardCustomerSheetNotes { get; set; }
        [Required(ErrorMessage = "Standard Quote Notes are Required.")]

        public string StandardQuoteNotes { get; set; }
        [Required(ErrorMessage = "Standard BOL Notes are Required.")]

        public string StandardBOLNotes { get; set; }
    }



}