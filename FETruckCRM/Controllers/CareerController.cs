using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FETruckCRM.Common;
using FETruckCRM.CustomFilter;
using FETruckCRM.Data;
using FETruckCRM.Models;

namespace FETruckCRM.Controllers
{
    [SessionExpire]
    [ExceptionHandler]

    public class CareerController : Controller
    {
        // GET: Career
        CareerService _service;
        // GET: Admin/Career
        #region Career
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new CareerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.CareerListModel> data = _service.getAllCareers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public string loaddataByPaging(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new CareerService();
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];


            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            // IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoads(loggedUserID);

            var sorCol = "CareerID";
            if (sortColumnIndex == 1)
            {
                sorCol = "CareerName";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "MCFFNo";
            }
            if (sortColumnIndex == 3)
            {
                sorCol = "LoadType";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "Address";
            }
            else if (sortColumnIndex == 5)
            {
                sorCol = "City";
            }
            //else if (sortColumnIndex == 5)
            //{
            //    sorCol = "StateName";
            //}
            else if (sortColumnIndex == 7)
            {
                sorCol = "ZipCode";
            }
            else if (sortColumnIndex == 8)
            {
                sorCol = "Telephone";
            }
            //else if (sortColumnIndex == 9)
            //{
            //    sorCol = "strStatusInd";
            //}
            //else if (sortColumnIndex == 10)
            //{
            //    sorCol = "strApprovalStatus";
            //}
            else if (sortColumnIndex == 11)
            {
                sorCol = "strCreatedDate";
            }
            //else if (sortColumnIndex == 12)
            //{
            //    sorCol = "AddedByUser";
            //}
            //else if (sortColumnIndex == 13)
            //{
            //    sorCol = "TeamLead";
            //}

            //else if (sortColumnIndex == 14)
            //{
            //    sorCol = "TeamManager";
            //}


            //IList<FETruckCRM.Models.UserDashboardModel> data = _Loadservice.getAllLoadsByPaging(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);

            IList<FETruckCRM.Models.CareerListModel> data = _service.getAllCareersByPaging(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
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
        public FileResult ExportToExcelCarrier()
        {
            _service = new CareerService();

            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = _service.getAllCareersExcel(loggedUserID);
            string attachment = "attachment; filename=CarrierReport.xls";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName.Replace("\t", ""));
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "Approval Date" || dt.Columns[i].ColumnName == "Date Added")
                    {
                        if (string.IsNullOrEmpty(dr[i].ToString()) == false)
                        {
                            Response.Write(tab + Convert.ToDateTime(dr[i].ToString().Replace("\t", "")).ToString("MM/dd/yyyy"));
                            tab = "\t";
                        }
                        else
                        {
                            Response.Write(tab + dr[i].ToString().Replace("\t", ""));
                            tab = "\t";
                        }
                    }
                    else
                    {
                        Response.Write(tab + dr[i].ToString().Replace("\t", ""));
                        tab = "\t";
                    }
                }
                Response.Write("\n");
            }
            Response.End();
            //Workbook book = new Workbook();
            //Worksheet sheet = book.Worksheets[0];
            //sheet.InsertDataTable(dt, true, 1, 1);
            //var filen = Server.MapPath("~/assets/Carrier/CarrierReport"+System.DateTime.Now.Ticks+".xls");
            //book.SaveToFile(filen, ExcelVersion.Version97to2003);
            ////System.Diagnostics.Process.Start(filen);
            return File(attachment, "application/vnd.ms-excel", "CarrierListing.xls");
        }

        [HttpGet]
        public ActionResult AddCareer(Int64? id = 0)
        {
            _service = new CareerService();

            CareerModel objModel = new CareerModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.CountryList = CareerService.getCountryList();
            objModel.StateList = CareerService.getStateList(0);
            objModel.EquipmentList = _service.getAllCareersEquipments(0);
            objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            objModel.LoadTypeList = _service.getAllLoadType();
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            objModel.strApprovalStatus = "1";
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Carrier";
            if (id > 0)
            {
                _service = new CareerService();

                objModel = _service.getCareerByCareerID(id);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.LoadTypeList = _service.getAllLoadType();
                objModel.CountryList = CareerService.getCountryList();
                objModel.StateList = CareerService.getStateList(objModel.CountryID);
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
                objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
                objModel.EquipmentList = _service.getAllCareersEquipments(id.Value);
                objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Carrier";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddCareer(CareerModel CareerModel)
        {
            _service = new CareerService();
            var msg = "";
            var IsSuccess = false;
            CareerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            CareerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            CareerModel.CountryList = CareerService.getCountryList();
            CareerModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            CareerModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            CareerModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            CareerModel.LoadTypeList = _service.getAllLoadType();


            //  CareerModel.EquipmentList = _service.getAllCareersEquipments(0);


            CareerModel.StateList = CareerService.getStateList((string.IsNullOrEmpty(CareerModel.strCountryID) ? (long)0 : Convert.ToInt64(CareerModel.strCountryID)));
            // List<SelectListItem> selectedItems = CareerModel.FormList.Where(p =>   CareerModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = (CareerModel.CareerID > 0 ? "Edit" : "Add") + " Carrier";
            ViewBag.Submit = CareerModel.CareerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.CheckMCFF(CareerModel.MCFFNo, CareerModel.CareerID))
                    {

                        CareerModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        CareerModel.LastModifyByID = Convert.ToInt64(Session["UserID"]);

                        var res = _service.RegisterCareer(CareerModel);

                        if (res > 0)
                        {
                            #region Send Email
                            try
                            {
                                UserService _userServioce = new UserService();
                                var objUser = _userServioce.getUserByUserID(Convert.ToInt64(Session["UserID"]));
                                EmailService emailService = new EmailService();
                                var emailmodel = emailService.getEmailByEmailTypeID(5);

                                var emailto = emailmodel.EmailAddress.Split(',');
                                foreach (var item in emailto)
                                {
                                    MailMessage message = new MailMessage();
                                    using (var smtp = new SmtpClient())
                                    {
                                        message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                                        message.To.Add(new MailAddress(item));
                                        message.Subject = emailmodel.Subject;
                                        message.IsBodyHtml = true; //to make message body as html  
                                        message.Body = emailmodel.Body.Replace("##CARRIERNAME##", CareerModel.CareerName).Replace("##MCFFNO##", CareerModel.MCFFNo).Replace("##USERNAME##", objUser.Alias);
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

                            TempData["Success"] = "Carrier " + (CareerModel.CareerID > 0 ? "updated" : "saved") + " successfully";
                            msg = "Carrier " + (CareerModel.CareerID > 0 ? "updated" : "saved") + " successfully";
                            IsSuccess = true;
                            return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                            //  return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                            msg = "something went wrong try later!";
                            IsSuccess = false;

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("MCFFNo", "M.C. #/F.F.# already exist!");
                        ViewBag.Error = "M.C. #/F.F.# already exist!";
                        msg = "M.C. #/F.F.# already exist!";
                        IsSuccess = false;

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    ViewBag.Error = "Data is not correct";
                    msg = "Data is not correct";
                    IsSuccess = false;
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return Json(new { data = CareerModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Delete(long CareerID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new CareerService();
                retval = _service.deleteCareer(CareerID, Convert.ToInt64(Session["UserID"]));

                if (retval > 0)
                {
                    msg = "Carrier deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Carrier is in use cannot be deleted.";
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

        #region CareerApprove
        public ActionResult ASIndex()
        {
            return View();
        }
        public ActionResult asloaddata()
        {
            _service = new CareerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.CareerModel> data = _service.getAllPendingCareers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ViewCareer(Int64? id = 0)
        {
            _service = new CareerService();

            CareerModel objModel = new CareerModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.CountryList = CareerService.getCountryList();
            objModel.StateList = CareerService.getStateList(0);
            objModel.EquipmentList = _service.getAllCareersEquipments(0);
            objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            objModel.LoadTypeList = _service.getAllLoadType();
            objModel.strApprovalStatus = "1";
            ViewBag.Submit = "Save";
            ViewBag.Title = "View Carrier";
            if (id > 0)
            {
                _service = new CareerService();

                objModel = _service.getCareerByCareerID(id);
                objModel.LoadTypeList = _service.getAllLoadType();
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.CountryList = CareerService.getCountryList();
                objModel.StateList = CareerService.getStateList(objModel.CountryID);
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
                objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
                objModel.EquipmentList = _service.getAllCareersEquipments(id.Value);
                objModel.FactoringCompanyListing = _service.getAllFactoringCompany();


                //ViewBag.Submit = "Update";
                //ViewBag.Title = "Edit Career";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult ViewCareer(CareerModel CareerModel)
        {
            _service = new CareerService();
            var msg = "";
            var IsSuccess = false;
            CareerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            CareerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            CareerModel.CountryList = CareerService.getCountryList();
            CareerModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            CareerModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            CareerModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            CareerModel.LoadTypeList = _service.getAllLoadType();
            //  CareerModel.EquipmentList = _service.getAllCareersEquipments(0);


            CareerModel.StateList = CareerService.getStateList((string.IsNullOrEmpty(CareerModel.strCountryID) ? (long)0 : Convert.ToInt64(CareerModel.strCountryID)));
            // List<SelectListItem> selectedItems = CareerModel.FormList.Where(p =>   CareerModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = "View Carrier";
            ViewBag.Submit = CareerModel.CareerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.CheckMCFF(CareerModel.MCFFNo, CareerModel.CareerID))
                    {

                        CareerModel.ApprovedBy = Convert.ToInt64(Session["UserID"]).ToString();
                        CareerModel.LastModifyByID = Convert.ToInt64(Session["UserID"]);

                        var res = _service.RegisterCareer(CareerModel);

                        if (res > 0)
                        {
                            #region Send Email


                            try
                            {
                                var objModel = _service.getCareerByCareerID(CareerModel.CareerID);

                                UserService _userServioce = new UserService();
                                var objUser = _userServioce.getUserByUserID(objModel.CreatedByID);
                                EmailService emailService = new EmailService();
                                var emailmodel = emailService.getEmailByEmailTypeID(2);
                                emailmodel.EmailAddress = emailmodel.EmailAddress + "," + objUser.EmailId;

                                var emailto = emailmodel.EmailAddress.Split(',');
                                foreach (var item in emailto)
                                {
                                    MailMessage message = new MailMessage();
                                    using (var smtp = new SmtpClient())
                                    {

                                        message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                                        message.To.Add(new MailAddress(item));
                                        message.Subject = emailmodel.Subject;
                                        message.IsBodyHtml = true; //to make message body as html  
                                        var action = CareerModel.strApprovalStatus == "2" ? "Approved" : "Not Approved";

                                        message.Body = emailmodel.Body.Replace("##CARRIERNAME##", CareerModel.CareerName).Replace("##APPROVALSTATUS##", action).Replace("##CREDITLIMIT##", "").Replace("##USERNAME##", objUser.Alias);


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
                            TempData["Success"] = "Carrier " + (CareerModel.CareerID > 0 ? "updated" : "saved") + " successfully";
                            msg = "Career " + (CareerModel.CareerID > 0 ? "updated" : "saved") + " successfully";
                            IsSuccess = true;
                            return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                            //  return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                            msg = "something went wrong try later!";
                            IsSuccess = false;

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("MCFFNo", "M.C. #/F.F.# already exist!");
                        ViewBag.Error = "M.C. #/F.F.# already exist!";
                        msg = "M.C. #/F.F.# already exist!";
                        IsSuccess = false;
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    ViewBag.Error = "Data is not correct";
                    msg = "Data is not correct";
                    IsSuccess = false;
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return Json(new { data = CareerModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        public ActionResult loadStates(string CountryID)
        {
            List<SelectListItem> retval;
            string msg = "";
            try
            {
                _service = new CareerService();
                retval = CareerService.getStateList(Convert.ToInt64(CountryID));


                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = null;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        #region Career Master Report
        public ActionResult CarrierMasaterReport()
        {
            return View();
        }
        //public ActionResult LoadCarrierMasterData()
        //{
        //    _service = new CareerService();
        //    var loggedUserID = Convert.ToInt64(Session["UserID"]);

        //    IList<FETruckCRM.Models.CareerModel> data = _service.getCarrierMasterReport(loggedUserID);
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

        public string LoadCarrierMasterData(string type, string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            sSearch = sSearch.ToLower();
            _service = new CareerService();

            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.CareerModel> data = _service.getCarrierMasterReport(loggedUserID);
            if (!string.IsNullOrEmpty(sSearch))
            {

                data = data.Where(x => x.CareerName.ToLower().Contains(sSearch) || x.Address.ToLower().Contains(sSearch) || x.City.ToLower().Contains(sSearch) || x.StateName.ToLower().Contains(sSearch) || x.Telephone.ToLower().Contains(sSearch))
                   .ToList();
            }


            int totalRecord = data.Count();
            var result = data.Skip(iDisplayStart).Take(iDisplayLength).ToList();


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

        public FileResult ExportToExcelCarrierMaster()
        {
            _service = new CareerService();

            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            DataTable dt = _service.getCarrierMasterReportdt(loggedUserID);
            //dt.Columns.Remove("CustomerID");
            //dt.Columns.Remove("CountryID");
            //dt.Columns.Remove("StateID");
            //dt.Columns.Remove("CreatedByID");
            //dt.Columns.Remove("LastModifiedByID");
            //dt.Columns.Remove("LastModifiedDate");
            //dt.Columns.Remove("CreatedDate");
            //dt.Columns.Remove("IsDeletedInd");
            //dt.Columns.Remove("IsOnHold");
            //dt.Columns.Remove("Status");
            //dt.Columns.Remove("ApprovalStatus");
            //dt.Columns.Remove("CustomerNo");
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

           

            return File(attachment, "application/vnd.ms-excel", "CarrierMasterReportListing'" + DateTime.UtcNow.Date.ToString() + "'.xls");
        }

        #endregion

        #region MC CHECK
        public ActionResult MCCheckIndex()
        {
            ViewBag.Title = "MC Check";
            ViewBag.ButtonTitle = "Add Mc Check";
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View();
        }
        [HttpPost]
        public string loadMCCheckdata(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new CareerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            var sorCol = "MCNumber";
            if (sortColumnIndex == 1)
            {
                sorCol = "MCNumber";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "CarrierName";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "CommodityValue";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "CommodityType";
            }
            else if (sortColumnIndex == 5)
            {
                sorCol = "EquipmentType";
            }
            else if (sortColumnIndex == 6)
            {
                sorCol = "CreatedDate";
            }
            else if (sortColumnIndex == 7)
            {
                sorCol = "ApprovalStatus";
            }
            IList<FETruckCRM.Models.MCCheckModel> data = _service.getAllMCCheck(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);

            // IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID, iDisplayStart, iDisplayLength, sSearch);
            //if (!string.IsNullOrEmpty(sSearch))
            //{

            //    data = data.Where(x => x.MCNumber.ToString().Contains(sSearch) || x.CarrierName.Contains(sSearch) || x.CommodityValue.Contains(sSearch) || x.CommodityType.Contains(sSearch) || x.City.Contains(sSearch) || x.StateName.Contains(sSearch) || x.Telephone.Contains(sSearch))
            //       .ToList();
            //}


            int totalRecord = data.Count();
            var result = data.Skip(iDisplayStart).Take(iDisplayLength).ToList();


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

        [HttpGet]
        public ActionResult AddMCCheck(Int64? MCCheckID = 0)
        {
            _service = new CareerService();
            MCCheckModel objModel = new MCCheckModel();
            objModel.strApprovalStatus = "1";
            objModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add MC Check";
            if (MCCheckID > 0)
            {
                _service = new CareerService();
                objModel = _service.MCCheckByMCCheckID(MCCheckID);
                objModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit MC Check";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddMCCheck(MCCheckModel mcCheckModel, HttpPostedFileBase[] files)
        {
            _service = new CareerService();
            var msg = "";
            var IsSuccess = false;
            mcCheckModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
            ViewBag.Title = (mcCheckModel.MCCheckID > 0 ? "Edit" : "Add") + " MC Check";
            ViewBag.Submit = mcCheckModel.MCCheckID > 0 ? "Update" : "Save";
            if (mcCheckModel.MCCheckDocsList == null)
            {
                mcCheckModel.MCCheckDocsList = new List<MCCheckDocModel>();
            }
            try
            {
                if (ModelState.IsValid)
                {

                    foreach (HttpPostedFileBase file in files)
                    {
                        //Checking file is available to save.
                        if (file != null)
                        {
                            MCCheckDocModel objDocModel = new MCCheckDocModel();

                            var InputFileName = Path.GetFileName(file.FileName);
                            FileInfo fi = new FileInfo(InputFileName);
                            var FilePath = "~/assets/MCChckDocs/" + fi.Name + DateTime.Now.Ticks.ToString() + fi.Extension;
                            // var ServerSavePath = Path.Combine(Server.MapPath("~/assets/MCChckDocs/") + InputFileName+DateTime.Now.Ticks.ToString());
                            var ServerSavePath = Path.Combine(Server.MapPath(FilePath));
                            objDocModel.MCCheckDocURL = FilePath;
                            objDocModel.MCCheckDocName = file.FileName;
                            objDocModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                            objDocModel.CreatedDate = DateTime.Now;
                            //Save file to server folder
                            file.SaveAs(ServerSavePath);

                            mcCheckModel.MCCheckDocsList.Add(objDocModel);
                            //assigning file uploaded status to ViewBag for showing message to user.
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }


                    }


                    //if (_service.CheckMCFF(CareerModel.MCFFNo, CareerModel.CareerID))
                    //{
                    mcCheckModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    mcCheckModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);
                    mcCheckModel.MCCheckID = mcCheckModel.MCCheckID ?? 0;
                    var res = _service.AddMCCheck(mcCheckModel);
                    if (res > 0)
                    {
                        #region Send Email
                        try
                        {
                            UserService _userServioce = new UserService();
                            var objUser = _userServioce.getUserByUserID(Convert.ToInt64(Session["UserID"]));
                            EmailService emailService = new EmailService();
                            var emailmodel = emailService.getEmailByEmailTypeID(5);
                            var emailto = emailmodel.EmailAddress.Split(',');
                            foreach (var item in emailto)
                            {
                                MailMessage message = new MailMessage();
                                using (var smtp = new SmtpClient())
                                {
                                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                                    message.To.Add(new MailAddress(item));
                                    message.Subject = emailmodel.Subject;
                                    message.IsBodyHtml = true; //to make message body as html  
                                    message.Body = emailmodel.Body.Replace("##CARRIERNAME##", mcCheckModel.CarrierName).Replace("##MCFFNO##", mcCheckModel.MCNumber.ToString()).Replace("##USERNAME##", objUser.Alias);
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
                        TempData["Success"] = "MC Check " + (mcCheckModel.MCCheckID > 0 ? "updated" : "saved") + " successfully";
                        msg = "MC Check " + (mcCheckModel.MCCheckID > 0 ? "updated" : "saved") + " successfully";
                        IsSuccess = true;
                        return RedirectToAction("MCCheckIndex");
                        // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        //  return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                        msg = "something went wrong try later!";
                        IsSuccess = false;
                    }
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("MCFFNo", "M.C. #/F.F.# already exist!");
                    //    ViewBag.Error = "M.C. #/F.F.# already exist!";
                    //    msg = "M.C. #/F.F.# already exist!";
                    //    IsSuccess = false;
                    //}
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    ViewBag.Error = "Data is not correct";
                    msg = "Data is not correct";
                    IsSuccess = false;
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return RedirectToAction("MCCheckIndex");
            //return Json(new { data = mcCheckModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MC Check Approval
        public ActionResult ViewMCCheckIndex()
        {
            ViewBag.Title = "View MC Check";
            ViewBag.ButtonTitle = "Add Mc Check";
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View();
        }
        [HttpPost]
        public string loadViewMCCheckdata(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new CareerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            var sorCol = "MCNumber";
            if (sortColumnIndex == 1)
            {
                sorCol = "MCNumber";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "CarrierName";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "CommodityValue";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "CommodityType";
            }
            else if (sortColumnIndex == 5)
            {
                sorCol = "EquipmentType";
            }
            else if (sortColumnIndex == 6)
            {
                sorCol = "CreatedDate";
            }
            else if (sortColumnIndex == 7)
            {
                sorCol = "ApprovalStatus";
            }
            IList<FETruckCRM.Models.MCCheckModel> data = _service.getAllMCCheck(loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);

            // IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID, iDisplayStart, iDisplayLength, sSearch);
            //if (!string.IsNullOrEmpty(sSearch))
            //{

            //    data = data.Where(x => x.MCNumber.ToString().Contains(sSearch) || x.CarrierName.Contains(sSearch) || x.CommodityValue.Contains(sSearch) || x.CommodityType.Contains(sSearch) || x.City.Contains(sSearch) || x.StateName.Contains(sSearch) || x.Telephone.Contains(sSearch))
            //       .ToList();
            //}


            int totalRecord = data.Count();
            var result = data.Skip(iDisplayStart).Take(iDisplayLength).ToList();


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

        [HttpGet]
        public ActionResult ViewMCCheck(Int64? MCCheckID = 0)
        {
            _service = new CareerService();
            MCCheckModel objModel = new MCCheckModel();
            objModel.strApprovalStatus = "1";
            objModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add MC Check";
            if (MCCheckID > 0)
            {
                _service = new CareerService();
                objModel = _service.MCCheckByMCCheckID(MCCheckID);
                objModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit MC Check";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult ViewMCCheck(MCCheckModel mcCheckModel, HttpPostedFileBase[] files)
        {
            _service = new CareerService();
            var msg = "";
            var IsSuccess = false;
            mcCheckModel.EquipmentTypeListing = _service.getAllEquipmentTypeList();
            ViewBag.Title = (mcCheckModel.MCCheckID > 0 ? "Edit" : "Add") + " MC Check";
            ViewBag.Submit = mcCheckModel.MCCheckID > 0 ? "Update" : "Save";
            if (mcCheckModel.MCCheckDocsList == null)
            {
                mcCheckModel.MCCheckDocsList = new List<MCCheckDocModel>();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (files != null)
                    {
                        foreach (HttpPostedFileBase file in files)
                        {
                            //Checking file is available to save.
                            if (file != null)
                            {
                                MCCheckDocModel objDocModel = new MCCheckDocModel();

                                var InputFileName = Path.GetFileName(file.FileName);
                                FileInfo fi = new FileInfo(InputFileName);
                                var FilePath = "~/assets/MCChckDocs/" + fi.Name + DateTime.Now.Ticks.ToString() + fi.Extension;
                                // var ServerSavePath = Path.Combine(Server.MapPath("~/assets/MCChckDocs/") + InputFileName+DateTime.Now.Ticks.ToString());
                                var ServerSavePath = Path.Combine(Server.MapPath(FilePath));
                                objDocModel.MCCheckDocURL = FilePath;
                                objDocModel.MCCheckDocName = file.FileName;
                                objDocModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                                objDocModel.CreatedDate = DateTime.Now;
                                //Save file to server folder
                                file.SaveAs(ServerSavePath);

                                mcCheckModel.MCCheckDocsList.Add(objDocModel);
                                //assigning file uploaded status to ViewBag for showing message to user.
                                ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                            }

                        }
                    }


                    //if (_service.CheckMCFF(CareerModel.MCFFNo, CareerModel.CareerID))
                    //{
                    mcCheckModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    mcCheckModel.ApprovedBy = Convert.ToInt64(Session["UserID"]).ToString();
                    mcCheckModel.ApprovalDate = DateTime.Now;
                    mcCheckModel.strApprovalStatus = mcCheckModel.ApprovalStatus.ToString();
                    mcCheckModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);
                    mcCheckModel.MCCheckID = mcCheckModel.MCCheckID ?? 0;
                    var res = _service.AddMCCheck(mcCheckModel);
                    if (res > 0)
                    {
                        #region Send Email
                        try
                        {
                            UserService _userServioce = new UserService();
                            var objUser = _userServioce.getUserByUserID(Convert.ToInt64(Session["UserID"]));
                            EmailService emailService = new EmailService();
                            var emailmodel = emailService.getEmailByEmailTypeID(6);
                            var emailto = emailmodel.EmailAddress.Split(',');
                            foreach (var item in emailto)
                            {
                                MailMessage message = new MailMessage();
                                using (var smtp = new SmtpClient())
                                {
                                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                                    message.To.Add(new MailAddress(item));
                                    message.Subject = emailmodel.Subject;
                                    message.IsBodyHtml = true; //to make message body as html  
                                    message.Body = emailmodel.Body.Replace("##CARRIERNAME##", mcCheckModel.CarrierName).Replace("##MCFFNO##", mcCheckModel.MCNumber.ToString()).Replace("##USERNAME##", objUser.Alias);
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
                        TempData["Success"] = "MC Check " + (mcCheckModel.ApprovalStatus == 2 ? "Approved" : "Not Approved") + " successfully";
                        msg = "MC Check " + (mcCheckModel.ApprovalStatus == 2 ? "Approved" : "Not Approved") + " successfully";
                        IsSuccess = true;
                        return RedirectToAction("ViewMCCheckIndex");
                        // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        //  return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                        msg = "something went wrong try later!";
                        IsSuccess = false;
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    ViewBag.Error = "Data is not correct";
                    msg = "Data is not correct";
                    IsSuccess = false;
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return RedirectToAction("ViewMCCheckIndex");
            //return Json(new { data = mcCheckModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}