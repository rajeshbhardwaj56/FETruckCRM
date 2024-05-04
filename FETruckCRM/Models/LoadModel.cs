using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FETruckCRM.Models
{
    public class LoadModel
    {
        public Int64 LoadID { get; set; }
        public Int32 LoadType { get; set; }
        public int BillingTypeID { get; set; }
        public string BillingTypeName { get; set; }
        public string CustomerName { get; set; }
        public Int64 LoadNo { get; set; }
        public Int64 BillTo { get; set; }
        public string Dispatcher { get; set; }
        public Int64 SaleRep1 { get; set; }
        public Int64 SaleRep2 { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string WO { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public string strRate { get; set; }
        public decimal PDs { get; set; }
        public decimal FSC { get; set; }
        public bool IsRatePercentage { get; set; }
        public string OtherCharges { get; set; }
        public decimal RatePercent { get; set; }
        public Int64 CareerID { get; set; }
        public Int64 DriverID { get; set; }
        public Int64 EquipmenttypeID { get; set; }
        public decimal CareerFee { get; set; }
        public string strCareerFee { get; set; }
        public decimal Margin { get; set; }
        public string strMargin { get; set; }
        public decimal MarginPercent { get; set; }
        public string strMarginPercent { get; set; }
        public string LoadTypeName { get; set; }

        public string Currency { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeletedInd { get; set; }

        public string Alias { get; set; }
        public string CareerName { get; set; }
        public string CareerTelephone { get; set; }
        public string CareerFax { get; set; }
        public string EquipmentTypeName { get; set; }
        public string FinalCarrierFee { get; set; }
        public decimal CarrierFSC { get; set; }

        public DateTime ShipperDate { get; set; }
        public string strShipperDate { get; set; }
        public DateTime LoadDate { get; set; }
        public string strLoadDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string strDeliveredDate { get; set; }
        public string Location { get; set; }
        public string ConsigneeLocation { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }

        public string CustomerID { get; set; }
        public string BillingEmail { get; set; }
        public string BillingTelephone { get; set; }
        public string PrimaryContact { get; set; }
        public string BillingFax { get; set; }
        
        public string Dispatcher1 { get; set; }
        public string LoadStatus { get; set; }

        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string CurrencyID { get; set; }
        public string PaymentTerm { get; set; }
        public string BillAddress { get; set; }
        public int LType { get; set; }

        public decimal AdvancePayment { get; set; }
        public List<SelectListItem> RecoveredLoadList { get; set; }
        public string strLoadID { get; set; }


    }

    public class LoadShipperModel
    {

        public Int64 LoadShipperID { get; set; }
        public Int64 LoadID { get; set; }
        public Int64 ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public DateTime ShipperDate { get; set; }
        public string strShipperDate { get; set; }
        public bool IsShowShipperTime { get; set; }
        public string ShipperTime { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }
        public string ShipperValues { get; set; }
        public string ShipperNotes { get; set; }
        public string ShippingHours { get; set; }
        public string MajorInspectionDirection { get; set; }
        public string Appointments { get; set; }
        public string PONumber { get; set; }
        public Int64 CustomBrokerID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool IsDeletedInd { get; set; }

    }

    public class LoadConsigneeModel
    {

        public Int64 LoadConsigneeID { get; set; }
        public Int64 LoadID { get; set; }
        public Int64 ConsigneeID { get; set; }

        public string ShippingHours { get; set; }
        public string MajorInspectionDirection { get; set; }
        public string Appointments { get; set; }

        public string ConsigneeName { get; set; }
        public string ConsigneeLocation { get; set; }
        public string Contact { get; set; }

        public DateTime ConsigneeDate { get; set; }
        public string strConsigneeDate { get; set; }
        public bool IsShowTime { get; set; }
        public string ConsigneeTime { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Weight { get; set; }
        public string Value { get; set; }
        public string Quantity { get; set; }
        public string DeliveryNotes { get; set; }
        public string PONumber { get; set; }
        public string ProMiles { get; set; }
        public string ProMilesEmpty { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool IsDeletedInd { get; set; }
    }



    public class Autocomplete
    {
        public string label { get; set; }
        public string val { get; set; }
        public string Location { get; set; }
        public bool IsOnHold { get; set; }
        public string CarrierName { get; set; }

        public bool IsCarrierBlackListed { get; set; }

    }

    public class LoadModeldata
    {

        public string LoadID { get; set; }
        public string CustomerName { get; set; }
        public string CarrierName { get; set; }
        public string MCNO { get; set; }
        public string LoadNo { get; set; }
        public string LoadType { get; set; }
        public string drpBillingType { get; set; }
        public string drpBillingTypeName { get; set; }
        public string CustomerID { get; set; }
        public string billto { get; set; }
        public string drpDispatcher { get; set; }
        public string drpSalesResp { get; set; }
        public string drpStatus { get; set; }
        public string txtWo { get; set; }
        public string txtdrpType { get; set; }
        public string txtRate { get; set; }
        public string txtPDS { get; set; }
        public string txtfscrate { get; set; }
        public string chkrate { get; set; }
        public string txtothercharges { get; set; }
        public string txtRateNw { get; set; }
        public string drpLoadTypeNew { get; set; }
        
        public string hdntxtCarrier { get; set; }
        public string drpEquipmentType { get; set; }
        public string txtCarrierFee { get; set; }
        public string drpCurrency { get; set; }
        public string CareerFee { get; set; }
        public string CurrencyID { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public bool IsDeletedInd { get; set; }


        public string txtInvoiceNo { get; set; }
        public string CerrierPDs { get; set; }
        public string CarrierFSC { get; set; }
        public string IsCarrierRatePercentage { get; set; }
        public string CarrierOthercharges { get; set; }
        public string FinalCarrierFee { get; set; }

        public string paymentType { get; set; }

       public decimal AdvancePayment { get; set; }
       public int IsLoadRecovered { get; set; }
        

    }

    public class LoadShipperModeldata
    {

        public string LoadShipperID { get; set; }
        public string LoadID { get; set; }
        public string ShipperID { get; set; }
        public string shippername { get; set; }
        public string location { get; set; }
        public string date { get; set; }
        public string isshowtime { get; set; }
        public string time { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string qty { get; set; }
        public string weight { get; set; }
        public string value { get; set; }
        public string notes { get; set; }
        public string ponumbers { get; set; }
        public string custombrokers { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeID { get; set; }
        public string LoadConsigneeID { get; set; }

        public string promiles { get; set; }
        public string empty { get; set; }
        public string LastModifiedByID { get; set; }
        public string IsDeletedInd { get; set; }
    }


    public class LoadOthercharges
    {

        public string LoadOtherChargesID { get; set; }
        public string LoadID { get; set; }
        public string OtherchargesDescription { get; set; }
        public string Amount { get; set; }
        public string OtherChargesDate { get; set; }
        public string OtherChargesType { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedByID { get; set; }
    }

    public class LoadCarrierOthercharges
    {

        public string LoadCarrierOtherChargesID { get; set; }
        public string LoadID { get; set; }
        public string Type { get; set; }
        public string OtherchargesDescription { get; set; }
        public string Amount { get; set; }
        public string OtherChargesDate { get; set; }
        public string OtherChargesType { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedByID { get; set; }
    }
    public class UserDashboardModel
    {

        public string LoadID { get; set; }
        public string LoadType { get; set; }
        public string Role { get; set; }
        public string LoadNo { get; set; }
        public string InvoiceNo { get; set; }
        
        public string BillTo { get; set; }
        public string WO { get; set; }
        public string CareerName { get; set; }
        public string CreatedDate { get; set; }
        public string strShipperDate { get; set; }
        public string strLoadDate { get; set; }
        public string strDeliveredDate { get; set; }
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string ConsigneeLocation { get; set; }
        public string Status { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string strRate { get; set; }
        public string strCareerFee { get; set; }
        public string strMargin { get; set; }
        public string strMarginPercent { get; set; }
        public string LoadTypeName { get; set; }
        public string InvoiceDate { get; set; }
        public bool IsShipperPaymentReceived { get; set; }
        public bool IsCarrierInvoiceReceived { get; set; }
        public bool IsCarrierPaymentMade { get; set; }
        public bool IsShipperInvoiceSent { get; set; }
        public string ShipperInvoiceSentDate { get; set; }
        public string ShipperPaymentReceivedDate { get; set; }
        public string CarrierInvoiceReceivedDate { get; set; }
        public string CarrierPaymentMadeDate { get; set; }
        public long TotalRecords { get; set; }

        public string PaymentType { get; set; }


    }


    public class RateConModel
    {

        public LoadModel Loaddata { get; set; }
        public List<LoadShipperModel> LoadShipperdata { get; set; }
        public List<LoadConsigneeModel> LoadConsigneedata { get; set; }
        public PreferencesModel LoadPreferencedata { get; set; }
        public LoadAdditionalNotes LoadAddiNotes { get; set; }
        public List<LoadOthercharges> LoadOthCharges { get; set; }
    }


    public class LoadAdditionalNotes
    {

        public string LoadAdditionalNotesID { get; set; }
        public string LoadID { get; set; }
        public string LoadNo { get; set; }
        public string DriverPayNotes { get; set; }
        public string DPNAppearOnReport { get; set; }
        public string InvoiceNotes { get; set; }
        public string INAppearOnInvoice { get; set; }
        public string InvoiceDescription { get; set; }
        public string DeletedRefusalNotes { get; set; }
        public string RecInvoiceNo { get; set; }
        public string RecInvoiceDate { get; set; }
        public string RecAmount { get; set; }
        public string RecCustomer { get; set; }
        public string RecContact { get; set; }
        public string RecTelephone { get; set; }
        public string RecTelephoneExt { get; set; }
        public string RecTollFree { get; set; }
        public string RecFax { get; set; }
        public string RecNotes { get; set; }
        public string InternalNotes { get; set; }
        public string ConDispatcherName { get; set; }
        public string ConDispatcherPhone { get; set; }
        public string conDispatcherEmail { get; set; }
        public string conDriverName { get; set; }
        public string ConDriverPhone { get; set; }
        public string conDriverEmail { get; set; }
        public string ConTruck { get; set; }
        public string ConTrailer { get; set; }
        public string ConNotes { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string IsDeleted { get; set; }
    }

    public class RawReportModel
    {

        public List<SelectListItem> ManagerList { get; set; }
        public List<SelectListItem> TeamLeadList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> SiteList { get; set; }

        public string strManagerID { get; set; }
        public string strTeamLeadID { get; set; }
        public string strEmployeeTypeID { get; set; }
        public string strSiteID { get; set; }
    }
}