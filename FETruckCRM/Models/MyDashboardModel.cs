using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FETruckCRM.Models
{
    public class CarrierDashboardModel
    {

        public List<SelectListItem> CarrierList { get; set; }
        public List<SelectListItem> FilterType { get; set; }
        public List<SelectListItem> DateFilter { get; set; }

        public string strCarrierID { get; set; }
        public string strFiltertypeID { get; set; }
        public string strDateFilterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CarrierName { get; set; }
        public string TotalLoads { get; set; }
        public string GrossRevenue { get; set; }
        public string CarrierPay { get; set; }
        public string Miles { get; set; }
        public string RevenueMiles { get; set; }
        public string PayPerMiles { get; set; }
        public bool IsPolicyExpires { get; set; }
        public long LoggedUserID { get; set; }

        public long TotalRecords { get; set; }


    }

    public class CustomerDashboardModel
    {
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> FilterType { get; set; }
        public List<SelectListItem> DateFilter { get; set; }
        public string strCustomerID { get; set; }
        public string strFiltertypeID { get; set; }
        public string strDateFilterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerName { get; set; }
        public string TotalLoads { get; set; }
        public string GrossRevenue { get; set; }
        public string NetProfit { get; set; }
        public string OpenLoad { get; set; }
        public string DeliveredLoad { get; set; }
        public string CompletedLoad { get; set; }
        public long LoggedUserID { get; set; }

        public long TotalRecords { get; set; }

    }

    public class DispatcherDashboardModel
    {
        public List<SelectListItem> DispatcherList { get; set; }
        public List<SelectListItem> FilterType { get; set; }
        public List<SelectListItem> DateFilter { get; set; }
        public string strDispatcherID { get; set; }
        public string strFiltertypeID { get; set; }
        public string strDateFilterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DispatcherName { get; set; }
        public string TotalLoads { get; set; }
        public string GrossRevenue { get; set; }
        public string NetProfit { get; set; }
        public string OpenLoad { get; set; }
        public string DeliveredLoad { get; set; }
        public string CompletedLoad { get; set; }
        public long LoggedUserID { get; set; }
        public long TotalRecords { get; set; }

    }
    public class LoadReportModel
    {
        public List<SelectListItem> ManagerList { get; set; }
        public List<SelectListItem> TeamLeadsList { get; set; }
        public List<SelectListItem> SiteList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public string strManagerID { get; set; }
        public string strTeamLeadID { get; set; }
        public string strEmployeeTypeID { get; set; }
        public string strSiteID { get; set; }
       

    }

    public class SalesRepDashboardModel
    {
        public List<SelectListItem> SalesRepList { get; set; }
        public List<SelectListItem> FilterType { get; set; }
        public List<SelectListItem> DateFilter { get; set; }
        public string strSalesRepID { get; set; }
        public string strFiltertypeID { get; set; }
        public string strDateFilterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SaleRepName { get; set; }
        public string TotalLoads { get; set; }
        public string GrossRevenue { get; set; }
        public string NetProfit { get; set; }
        public string OpenLoad { get; set; }
        public string DeliveredLoad { get; set; }
        public string CompletedLoad { get; set; }
        public long LoggedUserID { get; set; }

        public long TotalRecords { get; set; }

    }


    public class LoadDashboardModel
    {
        public List<SelectListItem> LoadStatusList { get; set; }
        public List<SelectListItem> FilterType { get; set; }
        public List<SelectListItem> DateFilter { get; set; }
        public string strLoadStatusID { get; set; }
        public string strFiltertypeID { get; set; }
        public string strDateFilterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoadNo { get; set; }
        public string LoadStatus { get; set; }
        public string CarrierName { get; set; }
        public string DateAdded { get; set; }
        public string Dispatcher { get; set; }
        public string CustomerName { get; set; }
        public string ShipperName { get; set; }
        public string ShipperDate { get; set; }
        public string Location { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeDate { get; set; }
        public string ConsigneeLocation { get; set; }

        public int LoadId { get; set; }

        public long TotalRecords { get; set; }

    }
}