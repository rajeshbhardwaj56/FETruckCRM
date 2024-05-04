using FETruckCRM.Data;
using FETruckCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using FETruckCRM.Common;
using FETruckCRM.CustomFilter;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Formatting = Newtonsoft.Json.Formatting;

namespace FETruckCRM.Controllers
{
    [SessionExpire]
    [ExceptionHandler]

    public class DashboardController : Controller
    {
        LoadService _service;
        // GET: Admin/Home
        public ActionResult Index()
        {
            LoadModel model = new LoadModel();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            model.RecoveredLoadList = LoadService.getRecoveredLoadList(loggedUserID);
            model.strLoadID = "";
            ViewBag.Rolename = us.RoleName;
            return View(model);
        }

        public ActionResult loaddata()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.UserDashboardModel> data = _service.getAllLoads(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getloaddata_test()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.UserDashboardModel> data = _service.getAllLoads(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindDropDowns()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var dt = LoadService.BindDropDowns(loggedUserID);
            //
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Json(json);
        }


        [HttpPost]
        public ActionResult CheckStatus(long LoadID, int LoadstatusID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();
                retval = _service.CheckStatus(LoadID, LoadstatusID);

                if (retval > 0)
                {
                    msg = "Load Status updated successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Are you sure you want to change status?";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ChangeStatus(long LoadID, int LoadstatusID, Int64 LoggedinUserId = 0)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();

                if (Convert.ToInt32(Session["RoleID"].ToString()) > 2 && (LoadstatusID == 9 || LoadstatusID == 10) )
                {
                    retval = -11;
                    
                }
                else
                {
                    retval = _service.ChangeStatus(LoadID, LoadstatusID, LoggedinUserId);
                }
                if (retval > 0)
                {
                    msg = "Load Status updated successfully.";

                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Cannot be updated.";
                }
                else if (retval == -11)
                {
                    msg = "You do not have permission to change this status.";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeShipperInvoiceSentStatus(long LoadID, bool IsShipperInvoiceSent,string ShipperInvoiceSentDate)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();
                retval = _service.ChangeShipperInvoiceSentStatus(LoadID, IsShipperInvoiceSent, ShipperInvoiceSentDate);

                if (retval > 0)
                {
                    msg = "Shipper Invoice Sent Status updated successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Cannot be updated.";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePaymentRecdStatus(long LoadID, bool IsCheckedPaymentRecd,string ShipperPaymentReceivedDate)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();
                retval = _service.ChangeShipperPaymentRecdStatus(LoadID, IsCheckedPaymentRecd, ShipperPaymentReceivedDate);

                if (retval > 0)
                {
                    msg = "Shipper Payment Received Status updated successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Cannot be updated.";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeCarrierInvoiceRecdStatus(long LoadID, bool IsCheckedPaymentRecd,string CarrierInvoiceReceivedDate)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();
                retval = _service.ChangeCarrierInvoiceRecdStatus(LoadID, IsCheckedPaymentRecd, CarrierInvoiceReceivedDate);

                if (retval > 0)
                {
                    msg = "Carrier Invoice Received Status updated successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Cannot be updated.";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IsCarrierPaymentMade(long LoadID, bool IsCheckedPaymentRecd,string CarrierInvoiceReceivedDate)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new LoadService();
                retval = _service.IsCarrierPaymentMade(LoadID, IsCheckedPaymentRecd, CarrierInvoiceReceivedDate);

                if (retval > 0)
                {
                    msg = "Carrier Payment Made Status updated successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Load Status already exists! Cannot be updated.";
                }
                else
                {
                    msg = "Some error occurred. Please try again!";
                }
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = -1;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }


        #region My  Dashboard
        public ActionResult MyDashboard()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            CarrierDashboardModel objmodel = new CarrierDashboardModel();
            objmodel.CarrierList = LoadService.getCarrierList();
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strCarrierID = "";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }
        
        [HttpPost]
        public JsonResult loadMyDashboarddata( string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var data = LoadService.getDashboardReport(loggedUserID,  FilterTypeID, FromDate, ToDate);
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            return Json(json);
        }
       
        #endregion


        #region My Carrier Dashboard
        public ActionResult Carrier()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            CarrierDashboardModel objmodel = new CarrierDashboardModel();
            objmodel.CarrierList = LoadService.getCarrierList();
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strCarrierID = "0";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }
        //[HttpPost]
        //public ActionResult loadCarrierdata( string CarrierID, string FilterTypeID, string FromDate, string ToDate)
        //{
        //    _service = new LoadService();
        //    var loggedUserID = Convert.ToInt64(Session["UserID"]);

        //    IList<FETruckCRM.Models.CarrierDashboardModel> data = _service.getAllLoadsByCarriers(loggedUserID,  CarrierID,  FilterTypeID,  FromDate,  ToDate);
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public string loadCarrierdata(string CarrierID, string FilterTypeID, string FromDate, string ToDate, string sEcho, int iDisplayStart=0, int iDisplayLength=0, string sSearch="")
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            string sorCol = "CareerName";

            if (sortColumnIndex == 1)
            {
                sorCol = "Loads";
            }
            else if (sortColumnIndex == 2)
            {
                sorCol = "GrossRevenue";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "CarrierPay";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "Miles";
            }

            var data = _service.getAllLoadsByCarriersPaging(loggedUserID,CarrierID,FilterTypeID,FromDate,ToDate, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
          
            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            var result = data;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public ActionResult ExportToExcelCarrier(string CarrierID, string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "CareerName";
           
            var dataList = _service.getAllLoadsByCarriersPaging(loggedUserID, CarrierID, FilterTypeID, FromDate, ToDate, 0, int.MaxValue, "", sorCol, "desc");
            int index = 1;
            var objlist = from data in dataList
                          select new
                          {
                              SrNo = index++,
                              CarrierName = data.CarrierName,
                              TotalLoads = data.TotalLoads,
                              GrossRevenue = data.GrossRevenue,
                              CarrierPay = data.CarrierPay,
                              Miles = data.Miles,
                              RevenueMiles = data.RevenueMiles,
                              PayPerMiles = data.PayPerMiles,
                              IsPolicyExpires = Convert.ToString(data.IsPolicyExpires)
                          };

            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = objlist.ToList();
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion

        #region My Customer Dashboard
        public ActionResult Customer()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            CustomerDashboardModel objmodel = new CustomerDashboardModel();
            objmodel.CustomerList = LoadService.getCustomerList(loggedUserID);
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strCustomerID = "0";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }

        //public ActionResult loadCustomerdata(string CustomerID, string FilterTypeID, string FromDate, string ToDate)
        //{
        //    _service = new LoadService();
        //    var loggedUserID = Convert.ToInt64(Session["UserID"]);

        //    IList<FETruckCRM.Models.CustomerDashboardModel> data = _service.getAllLoadsByCustomer(loggedUserID, CustomerID, FilterTypeID, FromDate, ToDate);
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public string loadCustomerdata(string CustomerID, string FilterTypeID, string FromDate, string ToDate, string sEcho, int iDisplayStart = 0, int iDisplayLength = 0, string sSearch = "")
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            string sorCol = "CustomerName";

            if (sortColumnIndex == 1)
            {
                sorCol = "Loads";
            }
            else if (sortColumnIndex == 2)
            {
                sorCol = "GrossRevenue";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "NetProfit";
            }
           

            IList<FETruckCRM.Models.CustomerDashboardModel> data = _service.getAllLoadsByCustomerPaging(loggedUserID, CustomerID, FilterTypeID, FromDate, ToDate, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            var result = data;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public ActionResult ExportToExcelCustomer(string CustomerID, string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "CustomerName";

            var dataList = _service.getAllLoadsByCustomerPaging(loggedUserID, CustomerID, FilterTypeID, FromDate, ToDate, 0, int.MaxValue, "", sorCol, "desc");
            int index = 1;
            var objlist = from data in dataList
                          select new
                          {
                              SrNo = index++,
                              CustomerName = data.CustomerName,
                              TotalLoads = data.TotalLoads,
                              GrossRevenue = data.GrossRevenue,
                              NetProfit = data.NetProfit,
                              OpenLoad = data.OpenLoad,
                              DeliveredLoad = data.DeliveredLoad,
                              CompletedLoad = data.CompletedLoad,
                          };

            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = objlist.ToList();
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion

        #region My Dispatcher Dashboard
        public ActionResult Dispatcher()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            DispatcherDashboardModel objmodel = new DispatcherDashboardModel();
            objmodel.DispatcherList = LoadService.getDispatcherList(loggedUserID);
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strDispatcherID = "0";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }
        //[HttpPost]
        //public ActionResult loadDispatcherdata(string DispatcherID, string FilterTypeID, string FromDate, string ToDate)
        //{
        //    _service = new LoadService();
        //    var loggedUserID = Convert.ToInt64(Session["UserID"]);
        //    IList<FETruckCRM.Models.DispatcherDashboardModel> data = _service.getAllLoadsByDispatcher(loggedUserID, DispatcherID, FilterTypeID, FromDate, ToDate);
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public string loadDispatcherdata(string DispatcherID, string FilterTypeID, string FromDate, string ToDate, string sEcho, int iDisplayStart = 0, int iDisplayLength = 0, string sSearch = "")
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            string sorCol = "DispatcherName";

            if (sortColumnIndex == 1)
            {
                sorCol = "Loads";
            }
            else if (sortColumnIndex == 2)
            {
                sorCol = "GrossRevenue";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "NetProfit";
            }

            IList<FETruckCRM.Models.DispatcherDashboardModel> data = _service.getAllLoadsByDispatcherPaging(loggedUserID, DispatcherID, FilterTypeID, FromDate, ToDate, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            var result = data;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public ActionResult ExportToExcelDispatcher(string DispatcherID, string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "DispatcherName";

            var dataList = _service.getAllLoadsByDispatcherPaging(loggedUserID, DispatcherID, FilterTypeID, FromDate, ToDate, 0, int.MaxValue, "", sorCol, "desc");
            int index = 1;
            var objlist = from data in dataList
                          select new
                          {
                              SrNo = index++,
                              DispatcherName = data.DispatcherName,
                              TotalLoads = data.TotalLoads,
                              GrossRevenue = data.GrossRevenue,
                              NetProfit = data.NetProfit,
                              OpenLoad = data.OpenLoad,
                              DeliveredLoad = data.DeliveredLoad,
                              CompletedLoad = data.CompletedLoad,
                          };

            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = objlist.ToList();
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion

        #region My Sales Rep Dashboard
        public ActionResult SalesRep()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            SalesRepDashboardModel objmodel = new SalesRepDashboardModel();
            objmodel.SalesRepList = LoadService.getSalesRepList(loggedUserID);
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strSalesRepID = "0";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }
        

        [HttpPost]
        public string loadSalesRepdata(string SalesRepID, string FilterTypeID, string FromDate, string ToDate, string sEcho, int iDisplayStart = 0, int iDisplayLength = 0, string sSearch = "")
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            string sorCol = "DispatcherName";

            if (sortColumnIndex == 1)
            {
                sorCol = "Loads";
            }
            else if (sortColumnIndex == 2)
            {
                sorCol = "GrossRevenue";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "NetProfit";
            }
            IList<FETruckCRM.Models.SalesRepDashboardModel> data = _service.getAllLoadsBySaleRepPaging(loggedUserID, SalesRepID, FilterTypeID, FromDate, ToDate,iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            var result = data;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public ActionResult ExportToExcelSalesRep(string SalesRepID, string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "SaleRepName";

            var dataList = _service.getAllLoadsBySaleRepPaging(loggedUserID, SalesRepID, FilterTypeID, FromDate, ToDate, 0, int.MaxValue, "", sorCol, "desc");
            int index = 1;
            var objlist = from data in dataList
            select new
            {
                              SrNo = index++,
                              SalesReportName = data.SaleRepName,
                              TotalLoads = data.TotalLoads,
                              GrossRevenue = data.GrossRevenue,
                              NetProfit = data.NetProfit,
                              OpenLoad = data.OpenLoad,
                              DeliveredLoad = data.DeliveredLoad,
                              CompletedLoad = data.CompletedLoad,
                          };

            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = objlist.ToList();
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion

        #region My Load Status Dashboard
        public ActionResult LoadStatus()
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            LoadDashboardModel objmodel = new LoadDashboardModel();
            objmodel.LoadStatusList = LoadService.getLoadStatusList(loggedUserID);
            objmodel.FilterType = HtmlHelperExtension.GetFilterType();
            objmodel.DateFilter = HtmlHelperExtension.GetDateFilter();
            objmodel.strLoadStatusID = "0";
            objmodel.strFiltertypeID = "1";
            objmodel.strDateFilterID = "8";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            objmodel.FromDate = startDate.ToString("MM/dd/yyyy");
            objmodel.ToDate = DateTime.Today.ToString("MM/dd/yyyy");
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View(objmodel);
        }
       
        [HttpPost]
        public string loadStatusdata(string LoadStatusID, string FilterTypeID, string FromDate, string ToDate, string sEcho, int iDisplayStart = 0, int iDisplayLength = 0, string sSearch = "")
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            string sorCol = "DispatcherName";

            if (sortColumnIndex == 1)
            {
                sorCol = "Loads";
            }
            else if (sortColumnIndex == 2)
            {
                sorCol = "GrossRevenue";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "NetProfit";
            }
            IList<FETruckCRM.Models.LoadDashboardModel> data = _service.getAllLoadsByLoadStatusPaging(loggedUserID, LoadStatusID, FilterTypeID, FromDate, ToDate, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            var result = data;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": ");
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        public ActionResult ExportToExcelloadStatus(string LoadStatusID, string FilterTypeID, string FromDate, string ToDate)
        {
            _service = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "CarrierName";

            var dataList = _service.getAllLoadsByLoadStatusPaging(loggedUserID, LoadStatusID, FilterTypeID, FromDate, ToDate, 0, int.MaxValue, "", sorCol, "desc");
            int index = 1;
            var objlist = from data in dataList
                          select new
                          {
                              SrNo=index++,
                              LoadNo = data.LoadNo,
                              Status = data.LoadStatus,
                              Carrier = data.CarrierName,
                              CreatedDate = data.DateAdded,
                              Dispatcher = data.Dispatcher,
                              Customer = data.CustomerName,
                              Shipper = data.ShipperName,
                              ShipDate = data.ShipperDate,
                              Location = data.Location,
                              Consignee = data.ConsigneeName,
                              DeliveryDate = data.ConsigneeDate,
                              ConsigneeLocation = data.ConsigneeLocation,
                          };

            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = objlist.ToList();
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion
    }
}
