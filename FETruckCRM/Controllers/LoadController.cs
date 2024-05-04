using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FETruckCRM.Common;
using FETruckCRM.Data;
using FETruckCRM.Models;
using Spire.Pdf;
using Spire.Xls;
using Spire.Pdf.HtmlConverter;
using System.Data;
using System.Web.UI;
using System.Threading;
using System.Net;
using Spire.Pdf.Graphics;
using System.Drawing;
using Spire.Pdf.AutomaticFields;
using FETruckCRM.CustomFilter;
using Spire.Pdf.Exporting.XPS.Schema;
using Spire.Pdf.Tables;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Pdf.HtmlConverter.Qt;
using Spire.Pdf.Grid;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Net.Mail;
using System.Configuration;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using iText.StyledXmlParser;
using iTextSharp.text;
using System.Web.UI.WebControls.WebParts;
using Spire.Xls;
using Worksheet = Spire.Xls.Worksheet;

namespace FETruckCRM.Controllers
{

    [SessionExpire]
    [ExceptionHandler]
    public class LoadController : Controller
    {
        AdditionalNotesService _service;
        LoadFilesService _LoadFilesservice;
        // GET: Load

        // GET: Admin/Load
        string _loadID;

        #region Load
        public JsonResult loaddata()
        {
            LoadService _Loadservice = new LoadService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoads(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadListing()
        {
            return View();
        }
        [HttpPost]
       public string getloaddata_test(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            LoadService _Loadservice = new LoadService();
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];


            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            // IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoads(loggedUserID);

            var sorCol = "LoadID";
            if (sortColumnIndex == 1)
            {
                sorCol = "LoadNo";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "InvoiceNo"; 
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "WO";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "CareerName";
            }
            else if (sortColumnIndex == 5)
            {
                sorCol = "strShipperDate";
            }
            else if (sortColumnIndex == 6)
            {
                sorCol = "CreatedDate";
            }
            else if (sortColumnIndex == 7)
            {
                sorCol = "strDeliveredDate";
            }
            else if (sortColumnIndex == 8)
            {
                sorCol = "CustomerName";
            }
            else if (sortColumnIndex == 9)
            {
                sorCol = "Location";
            }
            else if (sortColumnIndex == 10)
            {
                sorCol = "ConsigneeLocation";
            }
            else if (sortColumnIndex == 11)
            {
                sorCol = "strRate";
            }
            else if (sortColumnIndex == 12)
            {
                sorCol = "strCareerFee";
            }

            else if (sortColumnIndex == 13)
            {
                sorCol = "Status";
            }


            IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoadsByPaging(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);


            // IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID, iDisplayStart, iDisplayLength, sSearch);
            //if (!string.IsNullOrEmpty(sSearch))
            //{

            //    data = data.Where(x => x.CustomerName.Contains(sSearch) || x.LoadNo.Contains(sSearch) || x.InvoiceNo.Contains(sSearch) || x.WO.Contains(sSearch) || x.CareerName.Contains(sSearch) || x.Location.Contains(sSearch) || x.ConsigneeLocation.Contains(sSearch))
            //       .ToList();
            //}


            long totalRecord = data.Count>0? data.FirstOrDefault().TotalRecords:0;
            //var result = data.Skip(iDisplayStart).Take(iDisplayLength).ToList();
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
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingLoads()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View();
        }

        public string getPendingloaddata(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            LoadService _Loadservice = new LoadService();
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];


            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            // IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoads(loggedUserID);

            var sorCol = "LoadID";
            if (sortColumnIndex == 1)
            {
                sorCol = "LoadNo";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "InvoiceNo";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "WO";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "CareerName";
            }
            else if (sortColumnIndex == 5)
            {
                sorCol = "strShipperDate";
            }
            else if (sortColumnIndex == 6)
            {
                sorCol = "CreatedDate";
            }
            else if (sortColumnIndex == 7)
            {
                sorCol = "strDeliveredDate";
            }
            else if (sortColumnIndex == 8)
            {
                sorCol = "CustomerName";
            }
            else if (sortColumnIndex == 9)
            {
                sorCol = "Location";
            }
            else if (sortColumnIndex == 10)
            {
                sorCol = "ConsigneeLocation";
            }
            else if (sortColumnIndex == 11)
            {
                sorCol = "strRate";
            }
            else if (sortColumnIndex == 12)
            {
                sorCol = "strCareerFee";
            }

            else if (sortColumnIndex == 13)
            {
                sorCol = "Status";
            }


            IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllPendingLoadsByPaging(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);


            // IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID, iDisplayStart, iDisplayLength, sSearch);
            //if (!string.IsNullOrEmpty(sSearch))
            //{

            //    data = data.Where(x => x.CustomerName.Contains(sSearch) || x.LoadNo.Contains(sSearch) || x.InvoiceNo.Contains(sSearch) || x.WO.Contains(sSearch) || x.CareerName.Contains(sSearch) || x.Location.Contains(sSearch) || x.ConsigneeLocation.Contains(sSearch))
            //       .ToList();
            //}


            long totalRecord = data.Count > 0 ? data.FirstOrDefault().TotalRecords : 0;
            //var result = data.Skip(iDisplayStart).Take(iDisplayLength).ToList();
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
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public FileResult ExportToExcelLoad()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = LoadService.getloaddataForExcel(Convert.ToString(loggedUserID));

            string attachment = "attachment; filename=rawreport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "rawreport.xls");
        }

        public ActionResult Index(string loadid, string LoadType,string CloneId,string isrecoveredload="")
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            ViewBag.Load = Convert.ToString(loadid);
            ViewBag.LoadType = Convert.ToString(LoadType);
            ViewBag.isrecoveredload = string.IsNullOrEmpty(isrecoveredload)?0:1;

            if (!string.IsNullOrEmpty(CloneId))
            {
                ViewBag.IsClone = 1;
                ViewBag.CloneId = CloneId;
            }
            else
                ViewBag.IsClone = 0;
            return View();
        }


        public ActionResult PendingLoadIndex(string loadid, string LoadType)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            ViewBag.Load = Convert.ToString(loadid);
            ViewBag.LoadType = Convert.ToString(LoadType);
            return View();
        }


        #region Raw Report
        public FileResult ManagerReports()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var dt = LoadService.getManagerloaddataForExcel(Convert.ToString(loggedUserID));
            string attachment = "attachment; filename=rawreport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\t", ""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "rawreport.xls");
        }

        public FileResult RawReport()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = LoadService.getloaddataForExcel(Convert.ToString(loggedUserID));

            string attachment = "attachment; filename=rawreport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\t", ""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "rawreport.xls");
        }

       // [DeleteFileAttribute]
        public FileResult LoadRawReport(string MID, string TID, string EID, string SID)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = LoadService.getloaddataForExcelWithFilters(Convert.ToString(loggedUserID), MID, TID, EID, SID);
            string attachment = "attachment; filename=rawreport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
        

            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\t", ""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            
            //Spire.Xls.Workbook book = new Spire.Xls.Workbook();
            //Worksheet sheet = book.Worksheets[0];
            //sheet.InsertDataTable(dt, true, 1, 1);
            //var filen = Server.MapPath("~/assets/RawReport/rawreport"+DateTime.Now.Ticks.ToString()+".xls");
            //book.SaveToFile(filen, ExcelVersion.Version97to2003);
            // System.Diagnostics.Process.Start(filen);
            return File(attachment, "application/vnd.ms-excel", "rawreport.xls");
           

            //return File(System.IO.File.OpenRead(filen), "application/vnd.ms-excel", "rawreport.xls");
        }
        public ActionResult ViewRawReport()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            RawReportModel objmodel = new RawReportModel();
            objmodel.strManagerID = "";
            objmodel.strTeamLeadID = "";
            objmodel.strEmployeeTypeID = "";
            objmodel.strSiteID = "";
            objmodel.ManagerList = LoadService.getManagerList(loggedUserID);
            objmodel.TeamLeadList = LoadService.getTeamLeadList(0);
            objmodel.EmployeeTypeList = LoadService.getEmployeetypeList();
            objmodel.SiteList = LoadService.getSiteList();
            return View(objmodel);
        
        }
        #endregion


        [HttpPost]

        public JsonResult loadTeamLeadsdata(string ManagerID)
        {

           List<SelectListItem> list = LoadService.getTeamLeadList(Convert.ToInt64(ManagerID));
            return Json(list);

        
        }

        [HttpPost]
        public JsonResult AutoCompleteCustomer(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = LoadService.AutoCompleteCustomer(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteShipper(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = LoadService.AutoCompleteShipper(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteConsignee(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = LoadService.AutoCompleteConsignee(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteCarrier(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = LoadService.AutoCompleteCarrier(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteMC(string prefix)
        {
           var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = LoadService.AutoCompleteMC(loggedUserID, prefix);
            return Json(entities);
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
        public JsonResult saveload(LoadModeldata load, List<LoadShipperModeldata> shippers, List<LoadShipperModeldata> consignee, List<LoadOthercharges> othercharges, List<LoadCarrierOthercharges> Carrierothercharges)
        {
            var msg = "";
            var IsSuccess = false;
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            try
            {
                if (Convert.ToInt64(load.LoadID) == 0 && LoadService.CheckCreditLimit(Convert.ToInt64(load.billto), Convert.ToDecimal(load.txtRate)) <= 0)
                {

                    ModelState.AddModelError("", "Customer Credit Limit exceeded");
                    ViewBag.Error = "Customer Credit Limit exceeded";
                    msg = "Customer Credit Limit exceeded";
                    IsSuccess = false;
                }
                else
                {

                    var res = LoadService.SaveLoad(load, shippers, consignee, loggedUserID, othercharges, Carrierothercharges);

                    if (res > 0)
                    {

                        if (load.IsLoadRecovered == 1)
                        {
                            LoadService.UpdateLoadCreationDate(Convert.ToInt64(load.LoadID));
                        }
                        if (load.LoadType == "1" && Convert.ToInt64(load.LoadID) == 0)
                        {
                            #region Send Email
                            try
                            {
                                UserService _userServioce = new UserService();
                                var objUser = _userServioce.getUserByUserID(Convert.ToInt64(Session["UserID"]));
                                EmailService emailService = new EmailService();
                                var emailmodel = emailService.getEmailByEmailTypeID(3);

                                var emailto = emailmodel.EmailAddress.Split(',');
                                foreach (var item in emailto)
                                {
                                    MailMessage message = new MailMessage();
                                    using (var smtp = new SmtpClient())
                                    {
                                        message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                                        message.To.Add(new MailAddress(item));
                                        message.Subject = emailmodel.Subject;
                                        message.IsBodyHtml = true; //to make message body as html  CREDITLIMIT
                                        message.Body = emailmodel.Body.Replace("##SHIPPERNAME##", load.CustomerName).Replace("##SHIPPERRATE##", Convert.ToDecimal(load.txtRateNw).ToString("C")).Replace("##USERNAME##", objUser.Alias)
                                             .Replace("##CARRIERNAME##", load.CarrierName).Replace("##CARRIERRATE##", Convert.ToDecimal(load.FinalCarrierFee).ToString("C"));
                                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                                        smtp.Host = ConfigurationManager.AppSettings["host"]; //for gmail host  
                                        smtp.UseDefaultCredentials = Convert.ToInt32(ConfigurationManager.AppSettings["defaultcredential"]) == 1 ? true : false;
                                        smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
                                        smtp.EnableSsl = Convert.ToInt32(ConfigurationManager.AppSettings["enablessl"]) == 1 ? true : false;
                                        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);

                                    }
                                }




                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            #endregion
                        }
                        TempData["Success"] = "Load " + (Convert.ToInt64(load.LoadID) > 0 ? "updated" : "saved") + " successfully";
                        msg = "Load " + ((Convert.ToInt64(load.LoadID) > 0 ? "updated" : "saved") + " successfully");
                        IsSuccess = true;
                        _LoadFilesservice = new LoadFilesService();
                        IList<FETruckCRM.Models.LoadFilesModel> LFiles = _LoadFilesservice.getAllLoadfiles(Convert.ToInt64(res));
                        return Json(new { data = res, msg = msg, IsSuccess = IsSuccess,Count= LFiles.Count() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                        msg = "something went wrong try later!";
                        IsSuccess = false;

                    }
                }
               
               }
            catch (Exception ex)
            {

                msg = "something went wrong try later!";
                throw;

            }
            return Json(new { data = load, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getload(string loadid)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var dt = LoadService.getload(loadid);
            //
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Json(json);
        }

        public ActionResult LoadReport()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            LoadReportModel objmodel = new LoadReportModel();
            objmodel.ManagerList = LoadService.getSalesRepList(loggedUserID);
            objmodel.TeamLeadsList = HtmlHelperExtension.GetFilterType();
            objmodel.EmployeeTypeList = HtmlHelperExtension.GetDateFilter();
            objmodel.SiteList = HtmlHelperExtension.GetDateFilter();
            objmodel.strManagerID = "0";
            objmodel.strTeamLeadID = "0";
            objmodel.strSiteID = "0";
            objmodel.strEmployeeTypeID = "0";
           
            //var us = service.getUserByUserID(loggedUserID);
            //ViewBag.Rolename = us.RoleName;
            return View();
        }
      

        public FileResult AccReceReport()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = LoadService.getAccRecedataForExcel(Convert.ToString(loggedUserID));

            string attachment = "attachment; filename=AccRecereport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\t",""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "accountrecieablereport.xls");
        }

        public FileResult AccPayReport()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = LoadService.getAccPayedataForExcel(Convert.ToString(loggedUserID));

            string attachment = "attachment; filename=AccPayreport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\t",""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "accountrecieablereport.xls");
        }
      
        [STAThread]
        public FileResult Export(string loadid)
        {
            string fpath = Server.MapPath("~/rateconfirmation.html");
            this._loadID = loadid;
            Thread thread = new Thread(new ThreadStart(getpdf));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            string path = Server.MapPath("~/rateconfirmation.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "rateconfirmation.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        [STAThread]
        public FileResult ExportNew(string loadid)
        {
            string fpath = Server.MapPath("~/rateconfirmation.html");
            this._loadID = loadid;
            Thread thread = new Thread(new ThreadStart(getpdfNew1));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            string path = Server.MapPath("~/rateconfirmation.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "rateconfirmation.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public void getpdf()
        {
            string loadid = _loadID;
            var dt = LoadService.getloaddataForPDF(loadid);
            var Loadno = "0";
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                Loadno = Convert.ToString(dt.Tables[0].Rows[0]["Loadno"]);
            }
            string str = gethtmlstring(dt);
            PdfDocument doc = new PdfDocument();
            PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
            htmlLayoutFormat.IsWaiting = true;
            htmlLayoutFormat.Layout = Spire.Pdf.Graphics.PdfLayoutType.Paginate;
            htmlLayoutFormat.FitToPage = Clip.Both;
            PdfPageSettings setting = new PdfPageSettings();
            setting.Size = PdfPageSize.A4;
            setting.Margins = new PdfMargins(20, 80, 20, 20);
            doc.PageSettings.Size = PdfPageSize.A4;
            string imgurl = Server.MapPath("~/assets/images/eterLogonew.png");
            doc.PageSettings.Margins = new PdfMargins(75);
            doc.LoadFromHTML(str, true, setting, htmlLayoutFormat);
            string result = Server.MapPath("~/rateconfirmation.pdf"); //"HtmlToPdf.pdf";
            doc.SaveToFile(result);
            doc.Close();
        }

       
        public void getpdfNew1()
        {
            string loadid = _loadID;
            var dt = LoadService.getloaddataForPDF(loadid);
            var Loadno = "0";
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                Loadno = Convert.ToString(dt.Tables[0].Rows[0]["Loadno"]);
            }

            var shipdate = "";
            if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
            {
                shipdate = Convert.ToString(dt.Tables[1].Rows[0]["ShipperDate"]);
            }
            string str = gethtmlstring(dt);
            PdfDocument doc = new PdfDocument();
            PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
            htmlLayoutFormat.IsWaiting = true;
            htmlLayoutFormat.Layout = Spire.Pdf.Graphics.PdfLayoutType.Paginate;
            PdfPageSettings setting = new PdfPageSettings();
            setting.Size = PdfPageSize.A4;
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            doc.PageSettings.Size = PdfPageSize.A4;
            string imgurl = Server.MapPath("~/assets/images/eterLogonew.png");
            doc.PageSettings.Margins = new PdfMargins(0);
            PdfMargins margins = new PdfMargins(75f, 75f, 75f, 75f);
            doc.Template.Top = CreateHeaderTemplateNew(doc, margins, imgurl);
            doc.PageSettings.Margins = new PdfMargins(0);
            margins = new PdfMargins(75f, 75f, 75f, 75f);
            doc.Template.Bottom = CreateFooterTemplate(doc, margins);
            doc.PageSettings.Margins = new PdfMargins(0);
            margins = new PdfMargins(75f, 75f, 75f, 75f);
            //doc.LoadFromHTML(str, true, setting, htmlLayoutFormat);
            PdfSection sec = doc.Sections.Add();
            PdfPageBase page = sec.Pages.Add();
            float y = 10;
            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Rate Confirmation", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString("Country List", format1).Height;
            y = y + 5;

           


            PdfGrid grid = new PdfGrid();
            grid.Columns.Add(2);
           
            float width = page.Canvas.ClientSize.Width;

            grid.Style.CellPadding = new PdfPaddings(0, 0, 0, 0);
            grid.AllowCrossPages = true;

            PdfGridRow pdfGridRow = grid.Rows.Add();
            pdfGridRow.Height = 130;

            grid.Columns[0].Width = width * 0.30f;
            grid.Columns[1].Width = width * 0.70f;


            pdfGridRow.Cells[0].Value = "Fleet Experts \n 308 W 10th st \n Dear Park, NY USA 11729 \n Phone: 929-429-7237 \n Fax: 551-400-0786 ";
            pdfGridRow.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            pdfGridRow.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            pdfGridRow.Cells[0].Style.StringFormat.Alignment = PdfTextAlignment.Left;
            pdfGridRow.Cells[0].Style.Font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 12f, FontStyle.Bold), true);
            pdfGridRow.Cells[1].Value = "Description of Goods";
            PdfGrid embedgrid = new PdfGrid();
            embedgrid.Style.CellPadding = new PdfPaddings(2, 2, 0, 0);

            embedgrid.Columns.Add(4);
            embedgrid.Columns[0].Width = width * 0.125f;
            embedgrid.Columns[1].Width = width * 0.125f;
            embedgrid.Columns[2].Width = width * 0.125f;
            embedgrid.Columns[3].Width = width * 0.125f;
            PdfGridRow pdfembedGridRow = embedgrid.Rows.Add();
            pdfembedGridRow.Height = 25;

            pdfembedGridRow.Cells[0].Value = "Dispatcher";
            pdfembedGridRow.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["Alias"]);
            pdfembedGridRow.Cells[2].Value = "Load#";
            pdfembedGridRow.Cells[3].Value = Convert.ToString(dt.Tables[0].Rows[0]["LoadNo"]);
            pdfembedGridRow = embedgrid.Rows.Add();

            PdfGridRow pdfembedGridRow1 = embedgrid.Rows.Add();
            pdfembedGridRow1.Height = 25;
            pdfembedGridRow1.Cells[0].Value = "Phone #";
            pdfembedGridRow1.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["telephone"]);
            pdfembedGridRow1.Cells[2].Value = "Ship Date";
            pdfembedGridRow1.Cells[3].Value = shipdate;
            pdfembedGridRow1 = embedgrid.Rows.Add();

            PdfGridRow pdfembedGridRow2 = embedgrid.Rows.Add();
            pdfembedGridRow2.Height = 25;
            pdfembedGridRow2.Cells[0].Value = "Fax #";
            pdfembedGridRow2.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["Fax"]);
            pdfembedGridRow2.Cells[2].Value = "Today's Date";
            pdfembedGridRow2.Cells[3].Value = Convert.ToString(dt.Tables[0].Rows[0]["createdDate"]);
            pdfembedGridRow2 = embedgrid.Rows.Add();

            PdfGridRow pdfembedGridRow3 = embedgrid.Rows.Add();
            pdfembedGridRow3.Height = 25;
            pdfembedGridRow3.Cells[0].Value = "Email";
            pdfembedGridRow3.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["Email"]);
            pdfembedGridRow3.Cells[1].ColumnSpan = 3;
            pdfembedGridRow3 = embedgrid.Rows.Add();

            PdfGridRow pdfembedGridRow4 = embedgrid.Rows.Add();
            pdfembedGridRow4.Height = 25;
            pdfembedGridRow4.Cells[0].Value = "WO";
            pdfembedGridRow4.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["WO"]);
            pdfembedGridRow4.Cells[1].ColumnSpan = 3;
            pdfembedGridRow4 = embedgrid.Rows.Add();

            pdfGridRow.Cells[1].Value = embedgrid;
            foreach (PdfGridRow row in embedgrid.Rows)
            {
                foreach (PdfGridCell cell in row.Cells)
                {
                    cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                }
            }

            PdfGridRow pdfGridRowblank = grid.Rows.Add();
            grid.Columns[0].Width = width * 0.50f;
            grid.Columns[1].Width = width * 0.50f;
            pdfGridRowblank.Cells[0].ColumnSpan = 2;
            pdfGridRowblank.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            pdfGridRowblank.Cells[0].Value = " ";
            pdfGridRowblank.Height = 25;
            pdfGridRowblank = grid.Rows.Add();


            PdfGridRow pdfGridRowCarrier = grid.Rows.Add();
            grid.Columns[0].Width = width * 0.50f;
            grid.Columns[1].Width = width * 0.50f;
            pdfGridRowCarrier.Cells[0].ColumnSpan = 2;
            pdfGridRowCarrier.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);


            PdfGrid embedgrid2 = new PdfGrid();
            embedgrid2.Style.CellPadding = new PdfPaddings(2, 2, 0, 0);
            embedgrid2.Columns.Add(5);
            embedgrid2.Columns[0].Width = width * 0.20f;
            embedgrid2.Columns[1].Width = width * 0.20f;
            embedgrid2.Columns[2].Width = width * 0.20f;
            embedgrid2.Columns[3].Width = width * 0.20f;
            embedgrid2.Columns[4].Width = width * 0.20f;

            PdfGridRow pdfembedGridRowcarrier = embedgrid2.Rows.Add();
            pdfembedGridRowcarrier.Height = 25;
            pdfembedGridRowcarrier.Cells[0].Value = "Carrier";
            pdfembedGridRowcarrier.Cells[1].Value = "Phone #";
            pdfembedGridRowcarrier.Cells[2].Value = "Fax#";
            pdfembedGridRowcarrier.Cells[3].Value = "Equipment";
            pdfembedGridRowcarrier.Cells[4].Value = "Agreed Amount";
            pdfembedGridRowcarrier = embedgrid2.Rows.Add();

            PdfGridRow pdfembedGridRowcarrier1 = embedgrid2.Rows.Add();
            //  pdfembedGridRowcarrier1.Height = 25;
            pdfembedGridRowcarrier1.Cells[0].Value = Convert.ToString(dt.Tables[0].Rows[0]["CareerName"]);
            pdfembedGridRowcarrier1.Cells[1].Value = Convert.ToString(dt.Tables[0].Rows[0]["CareerTelephone"]);
            pdfembedGridRowcarrier1.Cells[2].Value = Convert.ToString(dt.Tables[0].Rows[0]["CareerFax"]);
            pdfembedGridRowcarrier1.Cells[3].Value = Convert.ToString(dt.Tables[0].Rows[0]["EquipmentTypeName"]);
            pdfembedGridRowcarrier1.Cells[4].Value = Convert.ToDecimal(dt.Tables[0].Rows[0]["FinalCarrierFee"]).ToString("C");
            pdfembedGridRowcarrier1 = embedgrid2.Rows.Add();

            pdfGridRowCarrier.Cells[0].Value = embedgrid2;

            PdfGridRow pdfGridRowblank1 = grid.Rows.Add();
            grid.Columns[0].Width = width * 0.50f;
            grid.Columns[1].Width = width * 0.50f;
            pdfGridRowblank1.Cells[0].ColumnSpan = 2;
            pdfGridRowblank1.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            pdfGridRowblank1.Cells[0].Value = " ";
            pdfGridRowblank1.Height = 25;
            pdfGridRowblank1 = grid.Rows.Add();


            var strshipper = "";
            if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dt.Tables[1].Rows)
                {

                    PdfGridRow pdfGridRowShipper = grid.Rows.Add();
                grid.Columns[0].Width = width * 0.40f;
                grid.Columns[1].Width = width * 0.60f;
                pdfGridRowShipper.Cells[0].Style.Borders.Right = new PdfPen(PdfBrushes.Transparent);
                pdfGridRowShipper.Cells[0].Value = "Shipper "+i+":"+ Convert.ToString(dr["ShipperName"]);
                PdfGrid embedgrid3 = new PdfGrid();
                embedgrid3.Style.CellPadding = new PdfPaddings(2, 2, 0, 0);
                embedgrid3.Columns.Add(4);
                embedgrid3.Columns[0].Width = width * 0.125f;
                embedgrid3.Columns[1].Width = width * 0.125f;
                embedgrid3.Columns[2].Width = width * 0.125f;
                embedgrid3.Columns[3].Width = width * 0.125f;

                PdfGridRow pdfembedGridRowShipper = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper.Height = 25;
                pdfembedGridRowShipper.Cells[0].Value = "Date";
                pdfembedGridRowShipper.Cells[1].Value = Convert.ToString(dr["ShipperDate"]);
                pdfembedGridRowShipper.Cells[2].Value = "Purchase Order#";
                pdfembedGridRowShipper.Cells[3].Value = Convert.ToString(dr["PONumber"]);
                pdfembedGridRowShipper.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper = embedgrid3.Rows.Add();

                PdfGridRow pdfembedGridRowShipper1 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper1.Height = 25;
                pdfembedGridRowShipper1.Cells[0].Value = "Time";
                pdfembedGridRowShipper1.Cells[1].Value = Convert.ToString(dr["ShipperTime"]);
                pdfembedGridRowShipper1.Cells[2].Value = "Major Instructions";
                pdfembedGridRowShipper1.Cells[3].Value = Convert.ToString(dr["MajorInspectionDirections"]);

                pdfembedGridRowShipper1.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1 = embedgrid3.Rows.Add();


                PdfGridRow pdfembedGridRowShipper2 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper2.Height = 25;
                pdfembedGridRowShipper2.Cells[0].Value = "Quantity";
                pdfembedGridRowShipper2.Cells[1].Value = Convert.ToString(dr["Quantity"]);
                pdfembedGridRowShipper2.Cells[2].Value = "Appointment";
                pdfembedGridRowShipper2.Cells[3].Value = Convert.ToString(dr["Appointments"]);
                pdfembedGridRowShipper2.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2 = embedgrid3.Rows.Add();

                PdfGridRow pdfembedGridRowShipper3 = embedgrid3.Rows.Add();
                //  pdfembedGridRowShipper3.Height = 25;
                pdfembedGridRowShipper3.Cells[0].Value = "Weight";
                pdfembedGridRowShipper3.Cells[1].Value = Convert.ToString(dr["Weight"]);
                pdfembedGridRowShipper3.Cells[2].Value = "Description";
                pdfembedGridRowShipper3.Cells[3].Value = Convert.ToString(dr["Description"]);
                pdfembedGridRowShipper3.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3 = embedgrid3.Rows.Add();


                PdfGridRow pdfembedGridRowShipper4 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper4.Height = 25;
                pdfembedGridRowShipper4.Cells[0].Value = "Notes";
                pdfembedGridRowShipper4.Cells[1].Value = Convert.ToString(dr["ShipperNotes"]);
                pdfembedGridRowShipper4.Cells[1].ColumnSpan = 3;
                pdfembedGridRowShipper4.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper4.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper4 = embedgrid3.Rows.Add();


                pdfGridRowShipper.Cells[1].Value = embedgrid3;
                    i++;
                }
            }


            var strsconsignee = "";
            if (dt.Tables.Count > 1 && dt.Tables[2].Rows.Count > 0)
            {
                int j = 1;
                foreach (DataRow dr in dt.Tables[2].Rows)
                {

                    PdfGridRow pdfGridRowShipper = grid.Rows.Add();
                grid.Columns[0].Width = width * 0.40f;
                grid.Columns[1].Width = width * 0.60f;
                pdfGridRowShipper.Cells[0].Style.Borders.Right = new PdfPen(PdfBrushes.Transparent);
                pdfGridRowShipper.Cells[0].Value = "Consignee " + j + ": \n "+Convert.ToString(dr["ShipperName"])+"  \n "+ Convert.ToString(dr["ConsigneeLocation"]);
                PdfGrid embedgrid3 = new PdfGrid();
                embedgrid3.Style.CellPadding = new PdfPaddings(2, 2, 0, 0);
                embedgrid3.Columns.Add(4);
                embedgrid3.Columns[0].Width = width * 0.125f;
                embedgrid3.Columns[1].Width = width * 0.125f;
                embedgrid3.Columns[2].Width = width * 0.125f;
                embedgrid3.Columns[3].Width = width * 0.125f;

                PdfGridRow pdfembedGridRowShipper = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper.Height = 25;
                pdfembedGridRowShipper.Cells[0].Value = "Date";
                pdfembedGridRowShipper.Cells[1].Value = Convert.ToString(dr["ConsigneeDate"]);
                pdfembedGridRowShipper.Cells[2].Value = "Purchase Order#";
                pdfembedGridRowShipper.Cells[3].Value = Convert.ToString(dr["PONumber"]);
                pdfembedGridRowShipper.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper = embedgrid3.Rows.Add();

                PdfGridRow pdfembedGridRowShipper1 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper1.Height = 25;
                pdfembedGridRowShipper1.Cells[0].Value = "Time";
                pdfembedGridRowShipper1.Cells[1].Value = Convert.ToString(dr["ConsigneeTime"]);
                pdfembedGridRowShipper1.Cells[2].Value = "Major Instructions";
                pdfembedGridRowShipper1.Cells[3].Value = Convert.ToString(dr["MajorInspectionDirections"]);

                pdfembedGridRowShipper1.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper1 = embedgrid3.Rows.Add();


                PdfGridRow pdfembedGridRowShipper2 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper2.Height = 25;
                pdfembedGridRowShipper2.Cells[0].Value = "Quantity";
                pdfembedGridRowShipper2.Cells[1].Value = Convert.ToString(dr["Quantity"]);
                pdfembedGridRowShipper2.Cells[2].Value = "Appointment";
                pdfembedGridRowShipper2.Cells[3].Value = Convert.ToString(dr["Appointments"]);
                pdfembedGridRowShipper2.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper2 = embedgrid3.Rows.Add();

                PdfGridRow pdfembedGridRowShipper3 = embedgrid3.Rows.Add();
                //  pdfembedGridRowShipper3.Height = 25;
                pdfembedGridRowShipper3.Cells[0].Value = "Weight";
                pdfembedGridRowShipper3.Cells[1].Value = Convert.ToString(dr["Weight"]);
                pdfembedGridRowShipper3.Cells[2].Value = "Description";
                pdfembedGridRowShipper3.Cells[3].Value = Convert.ToString(dr["Description"]);
                pdfembedGridRowShipper3.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3.Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper3 = embedgrid3.Rows.Add();


                PdfGridRow pdfembedGridRowShipper4 = embedgrid3.Rows.Add();
                // pdfembedGridRowShipper4.Height = 25;
                pdfembedGridRowShipper4.Cells[0].Value = "Notes";
                pdfembedGridRowShipper4.Cells[1].Value = Convert.ToString(dr["DeliveryNotes"]);
                pdfembedGridRowShipper4.Cells[1].ColumnSpan = 3;
                pdfembedGridRowShipper4.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper4.Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                pdfembedGridRowShipper4 = embedgrid3.Rows.Add();


                pdfGridRowShipper.Cells[1].Value = embedgrid3;
                    j++;
                }
            }


            foreach (PdfGridRow row in grid.Rows)
            {
                foreach (PdfGridCell cell in row.Cells)
                {
                    cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                }
            }

            var loadnotes = "";
            if (dt.Tables.Count > 3 && dt.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Tables[3].Rows)
                {

                                                                                                                                                                       

                    PdfGridRow pdfGridStandardNotes = grid.Rows.Add();
                    grid.Columns[0].Width = width * 0.50f;
                    grid.Columns[1].Width = width * 0.50f;
                    pdfGridStandardNotes.Cells[0].ColumnSpan = 2;
                    pdfGridStandardNotes.Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
                    pdfGridStandardNotes.Cells[0].Value = " ";
                    pdfGridStandardNotes.Height = 25;
                    pdfGridStandardNotes.Cells[0].Value =   Convert.ToString(dr["StandardLoadSheetNotes"]);
                    pdfGridStandardNotes = grid.Rows.Add();
                }
            }
            PdfLayoutResult result = grid.Draw(page, new PointF(0, 30));
            string result1 = Server.MapPath("~/rateconfirmation.pdf"); //"HtmlToPdf.pdf";
            //Save pdf file        
            doc.SaveToFile(result1);
            doc.Close();
        }

        public ActionResult ratecon(string loadid)
        {
            RateConModel objModel = new RateConModel();
            objModel = LoadService.getRateConloaddataForPDF(loadid);
            return View(objModel);
        }

        public ActionResult shipperratecon(string loadid)
        {
            RateConModel objModel = new RateConModel();
            objModel = LoadService.getShipperRateConloaddataForPDF(loadid);
            return View(objModel);
        }
        public ActionResult loadInv(string loadid)
        {
            RateConModel objModel = new RateConModel();
            objModel = LoadService.getloaddataForInvoicePDFNew(loadid);
            return View(objModel);
        }

        [STAThread]
        public FileResult InvoiceExport(string loadid)
        {
            string fpath = Server.MapPath("~/rateconfirmation.html");
            this._loadID = loadid;
            Thread thread = new Thread(new ThreadStart(getInvoicepdf));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            string path = Server.MapPath("~/invoice.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "invoice.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public void getInvoicepdf()
        {
            var dt = LoadService.getloaddataForInvoicePDF(this._loadID);
            var Loadno = "0";
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                Loadno = Convert.ToString(dt.Tables[0].Rows[0]["Loadno"]);
            }
            string str = getInvoicehtmlstring(dt);
            PdfDocument doc = new PdfDocument();
            PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
            htmlLayoutFormat.IsWaiting = false;

            doc.PageSettings.Margins = new PdfMargins(0);
            PdfPageSettings setting = new PdfPageSettings();
            setting.Size = PdfPageSize.A4;
            setting.Margins = new PdfMargins(20, 60, 20, 60);
            doc.LoadFromHTML(str, true, setting, htmlLayoutFormat);
            SizeF pageSize = doc.Pages[0].Size;
            float x = 90;
            float y = pageSize.Height - 120;
            for (int i = 0; i < doc.Pages.Count; i++)
            {
                //draw line at bottom
                PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.9f);
                doc.Pages[i].Canvas.DrawLine(pen, x, y, pageSize.Width - x, y);

                //draw text at bottom
                PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Calibri", 14f));
                PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
                String footerText = "Fleet Experts.\n";
                doc.Pages[i].Canvas.DrawString(footerText, font, PdfBrushes.RoyalBlue, x, y + 2, format);

                //draw page number and page count at bottom
                PdfPageNumberField number = new PdfPageNumberField();
                PdfPageCountField count = new PdfPageCountField();
                PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.RoyalBlue, "Page {0} of {1}", number, count);
                compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);
                SizeF size = font.MeasureString(compositeField.Text);
                compositeField.Bounds = new RectangleF(pageSize.Width - x, y + 2, size.Width, size.Height);
                compositeField.Draw(doc.Pages[i].Canvas);
            }
            string result = Server.MapPath("~/invoice.pdf"); //"HtmlToPdf.pdf";
            //Save pdf file        
            doc.SaveToFile(result);
            doc.Close();

        }
        static PdfPageTemplateElement CreateFooterTemplate(PdfDocument doc, PdfMargins margins)
        {
            //get page size
            SizeF pageSize = PdfPageSize.A4;

            //create a PdfPageTemplateElement object which works as footer space
            PdfPageTemplateElement footerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Bottom);
            footerSpace.Foreground = false;

            //declare two float variables
            float x = margins.Left;
            float y = 0;

            //draw line in footer space
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 1);
            footerSpace.Graphics.DrawLine(pen, x, y, pageSize.Width - x, y);

            //draw text in footer space
            y = y + 10;
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Impact", 10f), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String footerText = "Fleet Experts\n";
            footerSpace.Graphics.DrawString(footerText, font, PdfBrushes.Gray, x, y, format);

            //draw dynamic field in footer space
            PdfPageNumberField number = new PdfPageNumberField();
            PdfPageCountField count = new PdfPageCountField();
            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Gray, "Page {0} of {1}", number, count);
            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);
            SizeF size = font.MeasureString(compositeField.Text);
            footerSpace.Graphics.SetTransparency(0.5f);

            compositeField.Bounds = new RectangleF(pageSize.Width - x, y + 5, size.Width, size.Height);
            compositeField.Draw(footerSpace.Graphics, pageSize.Width - margins.Right, y);

            //return footerSpace
            return footerSpace;
        }
        static PdfPageTemplateElement CreateHeaderTemplate(PdfDocument doc, PdfMargins margins, string path)
        {
            //get page size
            SizeF pageSize = doc.PageSettings.Size;

            //create a PdfPageTemplateElement object as header space
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Top);
            headerSpace.Foreground = true;

            //declare two float variables
            float x = margins.Left;
            float y = 0;
            y = y - 100;
            //draw image in header space 
            // string imgurl =HttpContext.Request. Server.MapPath("~/assets/images/eterLogo.png");
            //PdfImage headerImage = PdfImage.FromFile(path);
            //float width = 100;// headerImage.Width / 3;
            //float height = 100;// headerImage.Height / 3;
            //headerSpace.Graphics.DrawImage(headerImage, x, margins.Top - height - 2, width, height);

            //draw line in header space
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 1);
            headerSpace.Graphics.DrawLine(pen, x, y + margins.Top - 2, pageSize.Width - x, y + margins.Top - 2);

            //draw text in header space
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Impact", 25f, FontStyle.Bold));
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String headerText = "Fleet Experts";
            SizeF size = font.MeasureString(headerText, format);
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Gray, pageSize.Width - x - size.Width - 2, margins.Top - (size.Height + 5), format);

            //return headerSpace
            return headerSpace;
        }

        static PdfPageTemplateElement CreateHeaderTemplateNew(PdfDocument doc, PdfMargins margins, string path)
        {
            //get page size
            SizeF pageSize = doc.PageSettings.Size;

            //create a PdfPageTemplateElement object as header space
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Top);
            headerSpace.Foreground = false;

            //declare two float variables
            float x = margins.Left;
            float y = 0;

            //draw image in header space 
            PdfImage headerImage = PdfImage.FromFile(path);
            float width = headerImage.Width / 3;
            float height = headerImage.Height / 3;
            headerSpace.Graphics.DrawImage(headerImage, x, margins.Top - height - 2, width, height);

            //draw line in header space
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 1);
            headerSpace.Graphics.DrawLine(pen, x, y + margins.Top - 2, pageSize.Width - x, y + margins.Top - 2);

            //draw text in header space
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Impact", 25f, FontStyle.Bold));
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String headerText = "\nFleet Experts";
            SizeF size = font.MeasureString(headerText, format);
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Gray, pageSize.Width - x - size.Width - 2, margins.Top - (size.Height + 5), format);



            
           // PdfLayoutResult result = grid.Draw(headerSpace, new PointF(0, 30));
            //return headerSpace
            return headerSpace;
        }

        static void AddHeader(PdfDocument doc, PdfMargins margin, string path)
        {
            //Get the size of first page
            SizeF pageSize = doc.Pages[0].Size;

            //Create a PdfPageTemplateElement object that will be
            //used as header space
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margin.Top);
            headerSpace.Foreground = true;
            doc.Template.Top = headerSpace;

            //Draw text at the top right of header space
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 9f, FontStyle.Bold | FontStyle.Italic), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            String headerText = "\nFleet Experts";
            float x = pageSize.Width;
            float y = 0;
            format.MeasureTrailingSpaces = true;

            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Black, x, y + 5, format);

            //Draw image at the top left of header space
            PdfImage headerImage = PdfImage.FromFile(path);

            float width = headerImage.Width / 2;
            float height = headerImage.Height / 2;
            headerSpace.Graphics.DrawImage(headerImage, 0, 0, width, height);
        }

        static void AddFooter(PdfDocument doc, PdfMargins margin, string path)
        {
            //Get the size of first page
            SizeF pageSize = doc.Pages[0].Size;

            //Create a PdfPageTemplateElement object that will be
            //used as footer space
            PdfPageTemplateElement footerSpace = new PdfPageTemplateElement(pageSize.Width, margin.Bottom);
            footerSpace.Foreground = true;
            doc.Template.Bottom = footerSpace;

            //Draw text at the center of footer space
            PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 9f, FontStyle.Bold), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center);
            // String headerText = "Copyright © 2017 xxx. All Rights Reserved.";
            float x = pageSize.Width / 2;
            float y = 15f;
            // footerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Black, x, y, format);

            //Create page number automatical field
            PdfPageNumberField number = new PdfPageNumberField();
            //Create page count automatical field
            PdfPageCountField count = new PdfPageCountField();
            //Add the fields in composite field
            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Black, "Page {0} of {1}", number, count);
            //Align string of "Page {0} of {1}" to center 
            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Bottom);
            compositeField.Bounds = footerSpace.Bounds;
            //Draw composite field on footer
            compositeField.Draw(footerSpace.Graphics);
        }


        public string gethtmlstring(DataSet dt)
        {

            string str = "";
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                var shipdate = "";
                if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
                {
                    shipdate = Convert.ToString(dt.Tables[1].Rows[0]["ShipperDate"]);
                }



                str += @"<html><body><div style='width:100%;font-family:arial;font-size:12px;margin:30px auto;'>" +
                 "<table style='width:100%;border-collapse: collapse;'> " +
                 "     <tr>                                                   " +
                 "         <td style='text-align:center;font-weight:bold;font-size:16px;'>   " +
                 "             RATE & LOAD CONFIRMATION                        " +
                 "         </td>                                               " +
                 "     </tr>                                                   " +
                 "     <tr>                                                    " +
                 "         <td>                                                " +
                 "             <table style='width:100%;'>                     " +
                 "                 <tr>                                        " +
                 "                     <td>                                    " +
                 "                         <table>                             " +
                 "                             <tr><td style='font-weight:bold;' rowspan='6' valign='top'>                     " +
                 "                             Fleet Experts   <br />            " +
                 "                             308 W 10th St            <br />        " +
                 "                             Deer Park, NY, USA 11729    <br />     " +
                 "                             Phone: 929-429-7237    <br />         " +
                 "                             Fax: 551-400-0786</td></tr>                " +
                 "                         </table>                                                                         " +
                 "                     </td>                                                                                " +
                 "                     <td>                                                                                 " +
                 "                         <table style='border: 1px #000 solid;width:100%;font-size:12px;'>                                          " +
                 "                             <tr><td style='font-weight:bold;'>Dispatcher</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["Alias"]) + "</td><td style='font-weight:bold;'>Load #</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["LoadNo"]) + "</td></tr>                  " +
                 "                             <tr><td style='font-weight:bold;'>Phone #</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["Telephone"]) + "</td><td style='font-weight:bold;'>Ship Date</td><td>" + shipdate + "</td></tr>  " +
                 "                             <tr><td style='font-weight:bold;'>Fax:</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["Fax"]) + "</td><td style='font-weight:bold;'>Booking Date</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["CreatedDate"]) + "</td></tr>  " +
                 "                             <tr><td style='font-weight:bold;'>Email:</td><td colspan='3'>" + Convert.ToString(dt.Tables[0].Rows[0]["Email"]) + "</td></tr>                                               " +
                 "                             <tr><td style='font-weight:bold;'>W/O:</td><td colspan='3'>" + Convert.ToString(dt.Tables[0].Rows[0]["WO"]) + "</td></tr>                                                                " +
                 "                         </table>                                                                                                                                   " +
                 "                     </td>                                                                                                                                          " +
                 "                 </tr>                                                                                                                                              " +
                 "             </table>                                                                                                                                               " +
                 "         </td>                                                                                                                                                      " +
                 "     </tr>                                                                                                                                                          " +
                 "     <tr>                                                                                                                                                           " +
                 "         <td>                                                                                                                                                       " +
                 "             <table style='border: 1px #000 solid;width:100%;font-size:12px;border-collapse: collapse;'>                                                                                                                                                " +
                 "                 <tr>                                                                                                                                           " +
                 "                 <td style='font-weight:bold;border: 1px #000 solid;'>Carrier</td>                                                                                                                                    " +
                 "                 <td style='font-weight:bold;border: 1px #000 solid;'>Phone #</td>                                                                                                                                   " +
                 "                 <td style='font-weight:bold;border: 1px #000 solid;'>Fax #</td>                                                                                                                                     " +
                 "                 <td style='font-weight:bold;border: 1px #000 solid;'>Equipment</td>                                                                                                                                 " +
                 "                 <td style='font-weight:bold;border: 1px #000 solid;'>Agreed Amount</td>                                                                                                                             " +
                 //  "                 <td style='font-weight:bold;border: 1px #000 solid;'>Load Status</td>                                                                                                                               " +
                 "                </tr>                                                                                                                                         " +
                 "                 <tr>                                                                                                                                               " +
                 "                     <td style='border: 1px #000 solid;'>  " + Convert.ToString(dt.Tables[0].Rows[0]["CareerName"]) + "                                                                                                                                         " +
                 "                     </td>                                                                                                                                          " +
                 "                     <td style='border: 1px #000 solid;'>" + Convert.ToString(dt.Tables[0].Rows[0]["CareerTelephone"]) + "</td>                                                                                                                          " +
                 "                     <td style='border: 1px #000 solid;'>" + Convert.ToString(dt.Tables[0].Rows[0]["CareerFax"]) + "</td>                                                                                                                          " +
                 "                     <td style='border: 1px #000 solid;'>  " + Convert.ToString(dt.Tables[0].Rows[0]["EquipmentTypeName"]) + "                                                                                                                                         " +
                 "                     </td>                                                                                                                                          " +
                 "                     <td style='border: 1px #000 solid;'> " + Convert.ToDecimal(dt.Tables[0].Rows[0]["FinalCarrierFee"]).ToString("C") + " </td>                                                                                                                           " +
                 //  "                     <td style='border: 1px #000 solid;'>" + Convert.ToString(dt.Tables[0].Rows[0]["LoadStatus"]) + " </td>                                                                                                                                  " +
                 "                 </tr>                                                                                                                                              " +
                 "             </table>                                                                                                                                               " +
                 "         </td>                                                                                                                                                      " +
                 "     </tr>                                                                                                                                                          " +
                 "     <tr>                                                                                                                                                           " +
                 "         <td>                                                                                                                                                       " +
                 "             <table style='border: 1px #000 solid;width:100%;font-size:12px;border-collapse: collapse;'>                                                                                                                                                ";
                var strshipper = "";
                if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow dr in dt.Tables[1].Rows)
                    {


                        strshipper += "                 <tr style='border:1px #000 solid;'>                                                                                                                                               " +
                          "                     <td style='border:1px #000 solid;width:50%;vertical-align:top;'>                                                                                                                                           " +
                          "                         <b>Shipper" + i.ToString() + ": " + Convert.ToString(dr["ShipperName"]) + "</b><br />                                                                                                                       " +
                          "                         " + Convert.ToString(dr["Location"]) + "<br />                                                                                                                    " +
                          "                     </td>                                                                                                                                          " +
                          "                     <td style='border:1px #000 solid;width:50%;'>                                                                                                                                           " +
                          "                         <table style='width:100%;font-size:12px;border-collapse: collapse;'>                                                                                                                                    " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Date</td>                                                                                            " +
                          "                                 <td>" + Convert.ToString(dr["ShipperDate"]) + "</td>                                                                                                               " +
                          "                                 <td style='font-weight:bold;'>Purchase Order#</td>                                                                               " +
                          "                                 <td>" + Convert.ToString(dr["PONumber"]) + "</td>                                                                                                                     " +
                          "                             </tr>                                                                                                                                  " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Type</td>                                                                                            " +
                          "                                 <td>" + Convert.ToString(dr["Type"]) + " </td>                                                                                                                " +
                          "                                 <td style='font-weight:bold;'>Major Intersection</td>                                                                              " +
                          "                                 <td>" + Convert.ToString(dr["MajorInspectionDirections"]) + "</td>                                                                                              " +
                          "                             </tr>                                                                                                                                  " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Quantity</td>                                                                                        " +
                          "                                 <td>" + Convert.ToString(dr["Quantity"]) + " </td>                                                                                                                      " +
                          "                                 <td style='font-weight:bold;'>Shipping Hours:</td>                                                                                 " +
                          "                                 <td>" + Convert.ToString(dr["ShippingHours"]) + "</td>                                                                                                                        " +
                          "                             </tr>                                                                                                                                  " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Weight</td>                                                                                          " +
                          "                                 <td>" + Convert.ToString(dr["Weight"]) + "</td>                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Appointment</td>                                                                                     " +
                          "                                 <td>" + Convert.ToString(dr["Appointments"]) + "</td>                                                                                                                      " +
                          "                             </tr>             " +
                          "" +
                          "                                                                                                                     " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Notes</td>                                                                                           " +
                          "                                 <td colspan='3'>" + Convert.ToString(dr["ShipperNotes"]) + " </td>                                                                                                            " +
                          "                             <tr>                                                                                                                                   " +
                          "                                 <td style='font-weight:bold;'>Description</td>                                                                                     " +
                          "                                 <td colspan='3'>" + Convert.ToString(dr["Description"]) + "</td>                                                                                                       " +
                          "                             </tr>                                                                                                                                  " +
               "                             </tr>                                                                                                                                  " +

                          "                         </table>                                                                                                                                   " +
                          "                     </td>                                                                                                                                          " +
                          "                 </tr>                                                                                                                                              ";
                        i++;
                    }
                }
                str += strshipper;
                str += "             </table>                                                                                                                                               " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          " +
                       "     <tr>                                                                                                                                                           " +
                       "         <td>                                                                                                                                                       " +
                       "             <table style='border: 1px #000 solid;width:100%;font-size:12px;border-collapse: collapse;'>                                                                                                                                                ";
                var strsconsignee = "";
                if (dt.Tables.Count > 1 && dt.Tables[2].Rows.Count > 0)
                {
                    int j = 1;
                    foreach (DataRow dr in dt.Tables[2].Rows)
                    {

                        strsconsignee += "                 <tr style='border:1px #000 solid;font-size:12px;'>                                                                                                                                               " +
                       "                     <td style='border:1px #000 solid;width:50%;vertical-align:top;'>                                                                                                                                           " +
                       "                         <b>Consignee" + j.ToString() + ": " + Convert.ToString(dr["ShipperName"]) + "</b><br />                                                                                                                     " +
                       "                         " + Convert.ToString(dr["ConsigneeLocation"]) + "<br />                                                                                                                    " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='border:1px #000 solid;width:50%;'>                                                                                                                                           " +
                       "                         <table style='font-size:12px;width:100%;border-collapse: collapse;'>                                                                                                                                    " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Date</td>                                                                                            " +
                       "                                 <td>" + Convert.ToString(dr["ConsigneeDate"]) + " </td>                                                                                                               " +
                       "                                 <td style='font-weight:bold;'>Purchase Order#</td>                                                                               " +
                       "                                 <td>" + Convert.ToString(dr["PONumber"]) + "</td>                                                                                                                     " +
                       "                             </tr>                                                                                                                                  " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Type</td>                                                                                            " +
                       "                                 <td>" + Convert.ToString(dr["Type"]) + " </td>                                                                                                                " +
                       "                                 <td style='font-weight:bold;'>Major Intersection</td>                                                                              " +
                       "                                 <td>" + Convert.ToString(dr["MajorInspectionDirections"]) + "</td>                                                                                              " +
                       "                             </tr>                                                                                                                                  " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Quantity</td>                                                                                        " +
                       "                                 <td>" + Convert.ToString(dr["Quantity"]) + " </td>                                                                                                                      " +
                       "                                 <td style='font-weight:bold;'>Receiving Hours:</td>                                                                                " +
                       "                                 <td>" + Convert.ToString(dr["ShippingHours"]) + "</td>                                                                                                                        " +
                       "                             </tr>                                                                                                                                  " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Weight</td>                                                                                          " +
                       "                                 <td>" + Convert.ToString(dr["Weight"]) + " </td>                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Appointment</td>                                                                                     " +
                       "                                 <td>" + Convert.ToString(dr["Appointments"]) + "</td>                                                                                                                      " +
                       "                             </tr>                                                                                                                                  " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Notes</td>                                                                                           " +
                       "                                 <td colspan='3'>" + Convert.ToString(dr["DeliveryNotes"]) + "</td>                                                                                                            " +
                       "                             </tr>                                                                                                                                  " +
                       "                             <tr>                                                                                                                                   " +
                       "                                 <td style='font-weight:bold;'>Description</td>                                                                                     " +
                       "                                 <td colspan='3'>" + Convert.ToString(dr["Description"]) + "</td>                                                                                                       " +
                       "                             </tr>                                                                                                                                  " +

                       "                         </table>                                                                                                                                   " +
                       "                     </td>                                                                                                                                          " +
                       "                 </tr>                                                                                                                                              ";
                        j++;
                    }
                }
                str += strsconsignee;
                str += "             </table>                                                                                                                                               " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          " +
                       "     <tr>                                                                                                                                                           " +
                       "         <td style='text-align:left; font-weight:bold;'>                                                                                                            " +
                       "             Dispatch Notes:                                                                                                                                        " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          ";

                var loadnotes = "";
                if (dt.Tables.Count > 3 && dt.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Tables[3].Rows)
                    {

                        loadnotes +=
                       "     <tr>                                                                                                                                                           " +
                       "         <td style='text-align:left; font-weight:bold;font-size:12px;margin-bottom:40px;'>                                                                                                            " +
                       "            " + Convert.ToString(dr["StandardLoadSheetNotes"]) + "                                                                                                                                        " +
                       "             **Signee certifies that this contract can be honored without exceeding driver's hour of service limitations. **                                        " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          ";

                    }
                }
                str += loadnotes;
                str +=
                        "     <tr>                                                                                                                                                           " +
                       "         <td style='text-align:left; font-weight:bold;font-size:12px;'>                                                                                                            " +
                       "             Carrier Pay: Carrier Fee:" + Convert.ToDecimal(dt.Tables[0].Rows[0]["CareerFee"]).ToString("C") + ", FSC Rate:" + Convert.ToDecimal(dt.Tables[0].Rows[0]["CarrierFSC"]).ToString("C") + ", " + Convert.ToString(dt.Tables[0].Rows[0]["OtherCharges"]) + " , TOTAL: " + Convert.ToDecimal(dt.Tables[0].Rows[0]["FinalCarrierFee"]).ToString("C") + "  USD                                                                                                                 " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          " +
                       "     <tr>                                                                                                                                                           " +
                       "         <td>                                                                                                                                                       " +
                       "             <table style='width:100%;font-size:12px;'>                                                                                                                                                " +
                       "                 <tr>                                                                                                                                               " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Accepted By: _______________                                                                                                               " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Date: _______________                                                                                                                      " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='text-align:left; font-weight:bold;' colspan='2'>                                                                                    " +
                       "                         Signature: _______________                                                                                                                 " +
                       "                     </td>                                                                                                                                          " +
                       "                 </tr>                                                                                                                                              " +
                       "                 <tr>                                                                                                                                               " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Driver Name: _______________                                                                                                               " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Cell #: _______________                                                                                                                    " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Truck #: _______________                                                                                                                   " +
                       "                     </td>                                                                                                                                          " +
                       "                     <td style='text-align:left; font-weight:bold;'>                                                                                                " +
                       "                         Trailer #: _______________                                                                                                                 " +
                       "                     </td>                                                                                                                                          " +
                       "                 </tr>                                                                                                                                              " +
                       "             </table>                                                                                                                                               " +
                       "         </td>                                                                                                                                                      " +
                       "     </tr>                                                                                                                                                          " +
                       " </table>                                                                                                                                                           " +
                    "</div></body></html>";
            }
            return str;

        }


        public string getInvoicehtmlstring(DataSet dt)
        {

            string str = "";
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                var shipdate = "";
                if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
                {
                    shipdate = Convert.ToString(dt.Tables[1].Rows[0]["ShipperDate"]);
                }

                str = @"<div style='width:100%;margin:0 auto;font-family:Arial;font-size:12px;'>" +
                "<table style='width:100%;' cellpadding='10' cellspacing='5'>                                                                                          " +
                "    <tr>                                                                                                                                              " +
                "        <td style='text-align: center; font-weight: bold;font-size:16px;'>Invoice</td>                                                               " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td>                                                                                                                                          " +
                "            <table style='width:100%;'>                                                                                                               " +
                "                <tr>                                                                                                                                  " +
                "                    <td>                                                                                                                              " +
                "                        <table cellpadding='0' cellspacing='0' style='width:100%;font-size:12px;'>                                                                                                                       " +
                "                            <tr><td rowspan='6' style='font-weight:bold;'>PAYABLE TO:<br />                                                                   " +
                "                            <b>Fleet Experts</b><br />                                                           " +
                "                            308 W 10th St<br />                                                                                          " +
                "                            Deer Park, NY, USA 11729<br />                                                                                " +
                "                            Phone: 929-429-7237<br />                                                                                   " +
                "                            Fax: 551-400-0786</td></tr>                                                                                       " +
                "                        </table>                                                                                                                      " +
                "                    </td>                                                                                                                             " +
                "                                                                                                                                                      " +
                "                    <td>                                                                                                                              " +
                "                        <table style='border: 1px #000 solid;' cellpadding='0' cellspacing='0' style='width:100%;font-size:12px;'>                                                                                       " +
                "                            <tr><td style='font-weight:bold;'>INVOICE #</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["InvoiceNo"]) + "</td></tr>                                                         " +
                "                            <tr><td>Invoice Date:</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["InvoiceDate"]) + "</td></tr>                                                                        " +
                "                            <tr><td>Terms:</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["PaymentTerm"]) + "</td></tr>                                                                                   " +
                "                            <tr><td>W/O (Ref):</td><td>" + Convert.ToString(dt.Tables[0].Rows[0]["WO"]) + "</td></tr>                                                                            " +
                "                        </table>                                                                                                                      " +
                "                                                                                                                                                      " +
                "                    </td>                                                                                                                             " +
                "                </tr>                                                                                                                                 " +
                "            </table>                                                                                                                                  " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td style='text-align: left; font-weight: bold;'>Bill To</td>                                                                                 " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td style='text-align: left;'>                                                                                                                " +
                "           " + Convert.ToString(dt.Tables[0].Rows[0]["CustomerName"]) + "<br />                                                                                                           " +
                "           " + Convert.ToString(dt.Tables[0].Rows[0]["BillAddress"]) + "                                                                                                           " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td style='text-align: left;font-weight:bold;'>                                                                                               " +
                "            LOAD DETAILS:                                                                                                                             " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td style='text-align: left;font-weight:bold;'>                                                                                               " +
                "            LOAD#:" + Convert.ToString(dt.Tables[0].Rows[0]["LoadNo"]) + "                                                                                                                                    " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             ";
                var strshipper = "";
                if (dt.Tables.Count > 1 && dt.Tables[1].Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow dr in dt.Tables[1].Rows)
                    {

                        strshipper += "    <tr>                                                                                                                                              " +
                "        <td>                                                                                                                                          " +
                "            <table style='width:100%;font-size:12px;' cellpadding='0' cellspacing='0'>                                                                                                                " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Shipper " + i + ":</td>                                                                                    " +
                "                    <td colspan='3'>" + Convert.ToString(dr["ShipperName"]) + "," + Convert.ToString(dr["Location"]) + "</td>                                                    " +
                "                    <td style='font-weight:bold;'>Date</td>                                                                                           " +
                "                    <td>" + Convert.ToString(dr["ShipperDate"]) + "</td>                                                                                                               " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Type:</td>                                                                                         " +
                "                    <td>" + Convert.ToString(dr["Type"]) + "</td>                                                                                                                       " +
                "                    <td style='font-weight: bold;'>Quantity:</td>                                                                                     " +
                "                    <td>" + Convert.ToString(dr["Quantity"]) + "</td>                                                                                                                      " +
                "                    <td style='font-weight: bold;'>Weight:</td>                                                                                       " +
                "                    <td>" + Convert.ToString(dr["Weight"]) + "</td>                                                                                                                    " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Description:</td>                                                                                  " +
                "                    <td colspan='5'>" + Convert.ToString(dr["Description"]) + "</td>                                                                                                           " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Purchase Order #:</td>                                                                             " +
                "                    <td colspan='5'>" + Convert.ToString(dr["PONumber"]) + "</td>                                                                                                           " +
                "                </tr>                                                                                                                                 " +
                "            </table>                                                                                                                                  " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             ";
                        i++;
                    }
                }
                str += strshipper;
                var strsconsignee = "";
                if (dt.Tables.Count > 1 && dt.Tables[2].Rows.Count > 0)
                {
                    int j = 1;
                    foreach (DataRow dr in dt.Tables[2].Rows)
                    {

                        strsconsignee += " <tr>                                                                                                                                              " +
                "        <td>                                                                                                                                          " +
                "            <table style='width:100%;font-size:12px;' cellpadding='0' cellspacing='0'>                                                                                                                " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Consignee 1:</td>                                                                                  " +
                "                    <td colspan='3'>                                                                                                                  " +
                "                        " + Convert.ToString(dr["ConsigneeName"]) + ", " + Convert.ToString(dr["ConsigneeLocation"]) + "                                                         " +
                "                    </td>                                                                                                                             " +
                "                    <td style='font-weight:bold;'>Date</td>                                                                                           " +
                "                    <td>" + Convert.ToString(dr["ConsigneeDate"]) + "</td>                                                                                                               " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Type:</td>                                                                                         " +
                "                    <td>" + Convert.ToString(dr["Type"]) + "</td>                                                                                                                       " +
                "                    <td style='font-weight: bold;'>Quantity:</td>                                                                                     " +
                "                    <td>" + Convert.ToString(dr["Quantity"]) + "</td>                                                                                                                      " +
                "                    <td style='font-weight: bold;'>Weight:</td>                                                                                       " +
                "                    <td>" + Convert.ToString(dr["Weight"]) + "</td>                                                                                                                    " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Description:</td>                                                                                  " +
                "                    <td colspan='5'>" + Convert.ToString(dr["Description"]) + "</td>                                                                                                           " +
                "                </tr>                                                                                                                                 " +
                "                <tr>                                                                                                                                  " +
                "                    <td style='font-weight: bold;'>Purchase Order #:</td>                                                                             " +
                "                    <td colspan='5'>" + Convert.ToString(dr["PONumber"]) + "</td>                                                                                                           " +
                "                </tr>                                                                                                                                 " +
                "            </table>                                                                                                                                  " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             ";

                        j++;
                    }
                }
                str += strsconsignee;
                str += "  <tr>                                                                                                                                              " +
                "        <td style='text-align: left;font-weight:bold;'>                                                                                               " +
                "            RATES AND CHARGES                                                                                                                         " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td>                                                                                                                                          " +
                "            <table>                                                                                                                                   " +
                "                <tr>                                                                                                                                  " +
                "                    <td> " + Convert.ToString(dt.Tables[0].Rows[0]["Type"]) + " </td>                                                                                                               " +
                "                    <td> " + Convert.ToDecimal(dt.Tables[0].Rows[0]["RatePercent"]).ToString("C") + "</td>                                                                                                                " +
                "                </tr>                                                                                                                                 " +
                "            </table>                                                                                                                                  " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "                                                                                                                                                      " +
                "    <tr>                                                                                                                                              " +
                "        <td>                                                                                                                                          " +
                "            Notes: <br/>    " + Convert.ToString(dt.Tables[3].Rows[0]["StandardInvoiceNotes"]) + "                                                                                                                               " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "    <tr>                                                                                                                                              " +
                "        <td style='font-size:12px;'>                                                                                                                                          " +
                "            Direct Deposit Info:                                                                                                                      " +
                "            Account Name: Fleet Experts LLC                                                                                                      " +
                "            Bank Name: TD Bank                                                                                                                        " +
                "            Account Type: Checking                                                                                                                    " +
                "            Account Number: 4360587972                                                                                                                " +
                "            Routing Number: 026013673                                                                                                                 " +
                "            Bank Address: 196-41 Northern blvd, Flushing, NY 11358                                                                                    " +
                "        </td>                                                                                                                                         " +
                "    </tr>                                                                                                                                             " +
                "</table>                                                                                                                                              " +
            "</div>";




            }

            return str;

        }
        #endregion

        #region Additional Notes And Files
        public ActionResult AdditionalNotes(long LoadID)
        {
            _service = new AdditionalNotesService();
            AdditionalNotesModel objModel = new AdditionalNotesModel();
            objModel = _service.getAdditionalNotesByLoadID(LoadID);
            objModel.LoadNo = "EL1";
            return View(objModel);
        }


        [HttpPost]
        public ActionResult AdditionalNotes(AdditionalNotesModel objModel)
        {
            _service = new AdditionalNotesService();
            ViewBag.Title = "Additional Notes";
            ViewBag.Submit = "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    objModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    var res = _service.RegisterAdditionalNotes(objModel);
                    if (res > 0)
                    {
                        TempData["Success"] = "Additional Notes saved successfully.";
                        return View(objModel);
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    ViewBag.Error = "Data is not correct";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
            }
            RouteValueDictionary rvd = new RouteValueDictionary();
            rvd.Add("LoadID", 1);
            return View(objModel);
        }

        public ActionResult LoadFiles(long LoadID)
        {
            LoadFilesModel objModel = new LoadFilesModel();
            objModel.LoadID = LoadID;
            ViewBag.Title = "Load Files";
            ViewBag.ButtonTitle = "Add File";
            ViewBag.LoadID = LoadID;
            return View(objModel);
        }

        public ActionResult AddLoadFiles(long LoadID, int PendingLoad)
        {
            LoadFilesModel objModel = new LoadFilesModel();
            objModel.LoadID = LoadID;
            ViewBag.Title = "Upload Load Files";
            ViewBag.ButtonTitle = "Add File";
            ViewBag.LoadID = LoadID;
            ViewBag.IspendingLoad = PendingLoad;
            return View(objModel);
        }
        public ActionResult loadfilesdata(string LoadID)
        {
            _LoadFilesservice = new LoadFilesService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.LoadFilesModel> data = _LoadFilesservice.getAllLoadfiles(Convert.ToInt64(LoadID));
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadFiles(LoadFilesModel model, HttpPostedFileBase imgUpd)
        {
            _LoadFilesservice = new LoadFilesService();

            var file = Request.Files[0];
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {

                    // extract only the filename
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    model.FileName = fileName;
                    fileName = DateTime.Now.Ticks.ToString() + fileName;
                    // store the file inside ~/App_Data/uploads folder
                    var path = System.IO.Path.Combine(Server.MapPath("~/assets/LoadFiles"), fileName);
                    try
                    {
                        file.SaveAs(path);

                        if (System.IO.File.Exists((Server.MapPath(model.FileURL))))
                        {
                            System.IO.File.Delete((Server.MapPath(model.FileURL)));
                        }
                        model.FileURL = "~/assets/LoadFiles/" + fileName;
                        long fileSize = file.ContentLength;
                        model.FileSize = fileSize.ToString() + " KB";
                        file.InputStream.Flush();
                    }
                    catch (Exception ce)
                    {

                    }

                }
                model.CreatedByID = Convert.ToInt64(Session["UserID"]);
                var res = _LoadFilesservice.RegisterLoadFiles(model);
                if (res > 0)
                {
                    TempData["Success"] = "Load File saved successfully.";
                    RouteValueDictionary rvd = new RouteValueDictionary();
                    rvd.Add("LoadID", model.LoadID);
                    return RedirectToAction("LoadFiles", rvd);
                }
                else
                {
                    ModelState.AddModelError("", "something went wrong try later!");
                    ViewBag.Error = "something went wrong try later!";
                }
            }
            else
            {
                ModelState.AddModelError("", "Data is not correct");
                ViewBag.Error = "Data is not correct";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddLoadFiles(LoadFilesModel model, HttpPostedFileBase imgUpd, int PendingLoad )
        {
            _LoadFilesservice = new LoadFilesService();
            ViewBag.IspendingLoad = PendingLoad;
            var file = Request.Files[0];
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {

                    // extract only the filename
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    model.FileName = fileName;
                    fileName = DateTime.Now.Ticks.ToString() + fileName;
                    // store the file inside ~/App_Data/uploads folder
                    var path = System.IO.Path.Combine(Server.MapPath("~/assets/LoadFiles"), fileName);
                    try
                    {
                        file.SaveAs(path);

                        if (System.IO.File.Exists((Server.MapPath(model.FileURL))))
                        {
                            System.IO.File.Delete((Server.MapPath(model.FileURL)));
                        }
                        model.FileURL = "~/assets/LoadFiles/" + fileName;
                        long fileSize = file.ContentLength;
                        model.FileSize = fileSize.ToString() + " KB";
                        file.InputStream.Flush();
                    }
                    catch (Exception ce)
                    {

                    }

                }
                model.CreatedByID = Convert.ToInt64(Session["UserID"]);
                var res = _LoadFilesservice.RegisterLoadFiles(model);
                if (res > 0)
                {
                    TempData["aSuccess"] = "Load File saved successfully.";
                    RouteValueDictionary rvd = new RouteValueDictionary();
                    rvd.Add("LoadID", model.LoadID);
                    rvd.Add("PendingLoad",PendingLoad);
                    return RedirectToAction("AddLoadFiles", rvd);
                }
                else
                {
                    ModelState.AddModelError("", "something went wrong try later!");
                    ViewBag.Error = "something went wrong try later!";
                }
            }
            else
            {
                ModelState.AddModelError("", "Data is not correct");
                ViewBag.Error = "Data is not correct";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteLoadFiles(long LoadFilesID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _LoadFilesservice = new LoadFilesService();
                retval = _LoadFilesservice.deleteLoadFiles(LoadFilesID);

                if (retval > 0)
                {
                    msg = "File deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "File is in use cannot be deleted.";
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

        #endregion
    }
}
