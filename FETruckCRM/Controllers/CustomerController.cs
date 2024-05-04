using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

    public class CustomerController : Controller
    {
        // GET: Customer
        CustomerService _service;
        // GET: Admin/Customer
        #region Customer
        public ActionResult Index()
        {
            ViewBag.Title = "Customer";
            ViewBag.ButtonTitle = "Add Customer";
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            return View();
        }

        public ActionResult CustomerIndex()
        {

            return View();
        }
        public ActionResult loaddata(string type)
        {
            _service = new CustomerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public string loadCustomerdata(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new CustomerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
           IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID);

            // IList<FETruckCRM.Models.CustomerModel> data = _service.getAllCustomers(loggedUserID, iDisplayStart, iDisplayLength, sSearch);
            if (!string.IsNullOrEmpty(sSearch))
            {

                data = data.Where(x => x.CustomerName.Contains(sSearch) || x.Address.Contains(sSearch) || x.Address2.Contains(sSearch) || x.Address3.Contains(sSearch) || x.City.Contains(sSearch) || x.StateName.Contains(sSearch) || x.Telephone.Contains(sSearch))
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




        public FileResult ExportToExcelCustomer()
        {
            _service = new CustomerService();

            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = _service.getAllCustomersExcel(loggedUserID);
            dt.Columns.Remove("CustomerID");
            dt.Columns.Remove("CountryID");
            dt.Columns.Remove("StateID");
            dt.Columns.Remove("CreatedByID");
            dt.Columns.Remove("LastModifiedByID");
            dt.Columns.Remove("LastModifiedDate");
            dt.Columns.Remove("CreatedDate");
            dt.Columns.Remove("IsDeletedInd");
            dt.Columns.Remove("IsOnHold");
            dt.Columns.Remove("Status");
            dt.Columns.Remove("ApprovalStatus");
            dt.Columns.Remove("CustomerNo");
            string attachment = "attachment; filename=CustomerList"+DateTime.Now.Ticks.ToString()+".xls";
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

                    Response.Write(tab + dr[i].ToString().Replace("\t",""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            return File(attachment, "application/vnd.ms-excel", "CustomerListing'"+DateTime.UtcNow.Date.ToString()+"'.xls");
        }





        [HttpGet]
        public ActionResult AddCustomer(Int64? id = 0)
        {
            _service = new CustomerService();

            CustomerModel objModel = new CustomerModel();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            UserService service = new UserService();
            var us = service.getUserByUserID(loggedUserID);
            ViewBag.Rolename = us.RoleName;
            // var role=UserService.getUsersList
            objModel.CountryList = CustomerService.getCountryList();
            objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
            objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.strApprovalStatus = "1";

            objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            objModel.StateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strCountryID) ? (long)0 : Convert.ToInt64(objModel.strCountryID)));
            objModel.BillingStateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(objModel.strBillingCountryID)));
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Customer";
            if (id > 0)
            {
                _service = new CustomerService();

                objModel = _service.getCustomerByCustomerID(id);
                objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();

                objModel.CountryList = CustomerService.getCountryList();
                objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
                objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
                objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
                objModel.StateList = CustomerService.getStateList(objModel.CountryID);
                objModel.BillingStateList = CustomerService.getStateList(objModel.BillingCountryID);
                objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Customer";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddCustomer(CustomerModel CustomerModel)
        {
            _service = new CustomerService();

            var msg = "";
            var IsSuccess = false;
            CustomerModel.CountryList = CustomerService.getCountryList();
            CustomerModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            CustomerModel.SaleRepList = CustomerService.getSaleRepList(Convert.ToInt64(Session["UserID"]));
            UserService service = new UserService();
            var us = service.getUserByUserID(Convert.ToInt64(Session["UserID"]));
            ViewBag.Rolename = us.RoleName;
            CustomerModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            CustomerModel.StatusList = HtmlHelperExtension.GetStatusListItems();

            CustomerModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            CustomerModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            CustomerModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            CustomerModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            CustomerModel.StateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strCountryID)));
            CustomerModel.BillingStateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strBillingCountryID)));
            ViewBag.Title = (CustomerModel.CustomerID > 0 ? "Edit" : "Add") + " Customer";
            ViewBag.Submit = CustomerModel.CustomerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.CheckCustomerName(CustomerModel))
                    {
                        CustomerModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        CustomerModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);

                        var res = _service.RegisterCustomer(CustomerModel);

                        if (res > 0)
                        {
                            #region Send Email
                            try
                            {
                                UserService _userServioce = new UserService();
                                var objUser = _userServioce.getUserByUserID(Convert.ToInt64(Session["UserID"]));
                                EmailService emailService = new EmailService();
                                var emailmodel = emailService.getEmailByEmailTypeID(4);

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
                                        message.Body = emailmodel.Body.Replace("##CUSTOMERNAME##", CustomerModel.CustomerName).Replace("##CREDITLIMIT##", Convert.ToDecimal(CustomerModel.strCreditLimit).ToString("C")).Replace("##USERNAME##", objUser.Alias);
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
                            TempData["Success"] = " Customer " + (CustomerModel.CustomerID > 0 ? "updated" : "saved") + " successfully";
                            IsSuccess = true;
                            return RedirectToAction("Index");
                            // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                            // msg = "Data is not correct";
                            IsSuccess = false;
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Customername", "Customer already exist!");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    //  ViewBag.Error = "Data is not correct";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return View(CustomerModel);
            //return Json(new { data = CustomerModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddLoadCustomer(Int64? id = 0)
        {
            _service = new CustomerService();

            CustomerModel objModel = new CustomerModel();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            objModel.CountryList = CustomerService.getCountryList();
            objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
            objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.strApprovalStatus = "1";

            objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            objModel.StateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strCountryID) ? (long)0 : Convert.ToInt64(objModel.strCountryID)));
            objModel.BillingStateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(objModel.strBillingCountryID)));
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Customer";
            if (id > 0)
            {
                _service = new CustomerService();

                objModel = _service.getCustomerByCustomerID(id);
                objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();

                objModel.CountryList = CustomerService.getCountryList();
                objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
                objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
                objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
                objModel.StateList = CustomerService.getStateList(objModel.CountryID);
                objModel.BillingStateList = CustomerService.getStateList(objModel.BillingCountryID);
                objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Customer";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddLoadCustomer(CustomerModel CustomerModel)
        {
            _service = new CustomerService();

            var msg = "";
            var IsSuccess = false;
            CustomerModel.CountryList = CustomerService.getCountryList();
            CustomerModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            CustomerModel.SaleRepList = CustomerService.getSaleRepList(Convert.ToInt64(Session["UserID"]));
            CustomerModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            CustomerModel.StatusList = HtmlHelperExtension.GetStatusListItems();

            CustomerModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            CustomerModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            CustomerModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            CustomerModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            CustomerModel.StateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strCountryID)));
            CustomerModel.BillingStateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strBillingCountryID)));
            ViewBag.Title = (CustomerModel.CustomerID > 0 ? "Edit" : "Add") + " Customer";
            ViewBag.Submit = CustomerModel.CustomerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.CheckCustomerName(CustomerModel))
                    {
                        CustomerModel.CreatedByID = Convert.ToInt64(Session["UserID"]);

                        var res = _service.RegisterCustomer(CustomerModel);

                        if (res > 0)
                        {

                            TempData["Success"] = " Customer " + (CustomerModel.CustomerID > 0 ? "updated" : "saved") + " successfully";
                            IsSuccess = true;
                            return RedirectToAction("CustomerIndex");
                            // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                            // msg = "Data is not correct";
                            IsSuccess = false;
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Customername", "Customer already exist!");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    //  ViewBag.Error = "Data is not correct";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return View(CustomerModel);
            //return Json(new { data = CustomerModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long CustomerID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new CustomerService();
                retval = _service.deleteCustomer(CustomerID, Convert.ToInt64(Session["UserID"]));

                if (retval > 0)
                {
                    msg = "Customer deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Customer is in use cannot be deleted.";
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
        public ActionResult OnHold(long CustomerID,bool IsOnHold)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new CustomerService();
                retval = _service.OnHoldCustomer(CustomerID, IsOnHold, Convert.ToInt64(Session["UserID"]));

                if (retval > 0)
                {
                    msg = "On Hold status updated successfully.";
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




        #region ApproveCustomer
        public ActionResult ASIndex()

        {
            ViewBag.Title = "Pending Approval Customer";
            return View();
        }
        public ActionResult asloaddata(string type)
        {
            _service = new CustomerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.CustomerModel> data = _service.getAllPendingCustomers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult ViewCustomer(Int64? id = 0)
        {
            _service = new CustomerService();

            CustomerModel objModel = new CustomerModel();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            objModel.CountryList = CustomerService.getCountryList();
            objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
            objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.strApprovalStatus = "1";

            objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            objModel.StateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strCountryID) ? (long)0 : Convert.ToInt64(objModel.strCountryID)));
            objModel.BillingStateList = CareerService.getStateList((string.IsNullOrEmpty(objModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(objModel.strBillingCountryID)));
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Customer";
            if (id > 0)
            {
                _service = new CustomerService();

                objModel = _service.getCustomerByCustomerID(id);
                objModel.SaleRepList = CustomerService.getSaleRepList(loggedUserID);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();

                objModel.CountryList = CustomerService.getCountryList();
                objModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
                objModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
                objModel.FactoringCompanyListing = _service.getAllFactoringCompany();
                objModel.StateList = CustomerService.getStateList(objModel.CountryID);
                objModel.BillingStateList = CustomerService.getStateList(objModel.BillingCountryID);
                objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Customer";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult ViewCustomer(CustomerModel CustomerModel, FormCollection fc)
        {
            _service = new CustomerService();
            var action = fc["action"];
            if (action == "Approve")
            {
                CustomerModel.strApprovalStatus = "2";
            }
            else if (action == "Not Approve")
            {
                CustomerModel.strApprovalStatus = "3";
            }
            else if (action == "Approve For Prepaid Only")
            {
                CustomerModel.strApprovalStatus = "4";
            }
            var msg = "";
            var IsSuccess = false;
            CustomerModel.CountryList = CustomerService.getCountryList();
            CustomerModel.MCFFList = HtmlHelperExtension.GetMCFFListItems();
            CustomerModel.SaleRepList = CustomerService.getSaleRepList(Convert.ToInt64(Session["UserID"]));
            CustomerModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            CustomerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            CustomerModel.FSCTypeList = HtmlHelperExtension.GetFSCTypeListItems();
            CustomerModel.FactoringCompanyListing = _service.getAllFactoringCompany();
            CustomerModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            CustomerModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            CustomerModel.StateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strCountryID)));
            CustomerModel.BillingStateList = CustomerService.getStateList((string.IsNullOrEmpty(CustomerModel.strBillingCountryID) ? (long)0 : Convert.ToInt64(CustomerModel.strBillingCountryID)));
            CustomerModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);
            CustomerModel.ApprovedBy = Convert.ToInt64(Session["UserID"]).ToString();
            ViewBag.Title = "View Customer";
            // ViewBag.Submit = CustomerModel.CustomerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {

                    if (_service.CheckCustomerName(CustomerModel))
                    {
                        CustomerModel.ApprovedBy = Convert.ToInt64(Session["UserID"]).ToString();

                        var res = _service.RegisterCustomer(CustomerModel);

                        if (res > 0)
                        {
                            #region Send Email


                            try
                            {
                                var objModel = _service.getCustomerByCustomerID(CustomerModel.CustomerID);
                                UserService _userService = new UserService();
                                var objUser = _userService.getUserByUserID(objModel.CreatedByID);
                                EmailService emailService = new EmailService();
                                var emailmodel = emailService.getEmailByEmailTypeID(1);
                                emailmodel.EmailAddress=emailmodel.EmailAddress + "," + objUser.EmailId;
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
                                        var status = action == "Not Approve" ? "Not Approved" : action == "Approve" ? "Approved" : "Approved For Prepaid Only";
                                        if (action == "Not Approve")
                                        {
                                            message.Body = emailmodel.Body.Replace("##CUSTOMERNAME##", CustomerModel.CustomerName).Replace("##APPROVALSTATUS##", status).Replace("##CREDITLIMIT##", "").Replace("(Amount)","").Replace("for", "").Replace("##USERNAME##", objUser.Alias);

                                        }
                                        else
                                        {
                                            message.Body = emailmodel.Body.Replace("##CUSTOMERNAME##", CustomerModel.CustomerName).Replace("##APPROVALSTATUS##", status).Replace("##CREDITLIMIT##", Convert.ToDecimal(CustomerModel.strCreditLimit).ToString("C")).Replace("##USERNAME##", objUser.Alias);
                                        }
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

                            TempData["Success"] = " Customer " + (CustomerModel.CustomerID > 0 ? "updated" : "saved") + " successfully";
                            IsSuccess = true;
                            return RedirectToAction("asIndex");
                            // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                            // msg = "Data is not correct";
                            IsSuccess = false;
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("MCFF", "MC/FF No already exists!");
                        //  ViewBag.Error = "Data is not correct";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    //  ViewBag.Error = "Data is not correct";
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = e.Message;
                msg = e.Message;
                IsSuccess = false;
            }
            return View(CustomerModel);
            //return Json(new { data = CustomerModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCustomerNotifications(Int64 CustomerID)

        {
            ViewBag.Title = "View Customer Notifications";
            ViewBag.ButtonTitle = "View Customer";
            ViewBag.CustomerID = CustomerID;
            return View();
        }
        public ActionResult ViewloadCustomerNotificationdata(string CustomerID)
        {
            _service = new CustomerService();
            IList<CustomerNotificationsModel> data = _service.getAllCustomerNotifications(Convert.ToInt64(CustomerID));
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        

        [HttpPost]
        public ActionResult CheckDuplicate(string CustomerID,string CustomerName)
        {
            bool retval;
            string msg = "";
            try
            {
                _service = new CustomerService();
                CustomerModel model = new CustomerModel();
                model.CustomerID = Convert.ToInt64(CustomerID);
                model.CustomerName = CustomerName;
                retval = _service.CheckCustomerName(model);

                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = false;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loadStates(string CountryID)
        {
            List<SelectListItem> retval;
            string msg = "";
            try
            {
                _service = new CustomerService();
                retval = CustomerService.getStateList(Convert.ToInt64(CountryID));


                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = null;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CustomerNotifications(Int64 CustomerID)

        {
            ViewBag.Title = "Customer";
            ViewBag.ButtonTitle = "Add Customer";
            ViewBag.CustomerID = CustomerID;
            return View();
        }
        public ActionResult loadCustomerNotificationdata(string CustomerID)
        {
            _service = new CustomerService();
            IList<CustomerNotificationsModel> data = _service.getAllCustomerNotifications(Convert.ToInt64(CustomerID));
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddCustomerNotifications(Int64 customerID, Int64? id = 0)
        {
            _service = new CustomerService();

            CustomerNotificationsModel objModel = new CustomerNotificationsModel();
            objModel.CustomerID = customerID;


            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Customer Notification";
            if (id > 0)
            {
                _service = new CustomerService();

                objModel = _service.getCustomerNotificationByID(id);
                objModel.CustomerID = customerID;
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Customer Notification";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddCustomerNotifications(CustomerNotificationsModel CustomerNotificationModel)
        {
            _service = new CustomerService();

            var msg = "";
            var IsSuccess = false;
            ViewBag.Title = (CustomerNotificationModel.CustomerNotificationID > 0 ? "Edit" : "Add") + " Customer notification";
            ViewBag.Submit = CustomerNotificationModel.CustomerNotificationID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_service.CheckCustomerName(CustomerModel.CustomerName, CustomerModel.CustomerID))
                    //{

                    var res = _service.RegisterCustomerNotifications(CustomerNotificationModel);

                    if (res > 0)
                    {

                        TempData["Success"] = " Customer Notification " + (CustomerNotificationModel.CustomerNotificationID > 0 ? "updated" : "saved") + " successfully";
                        IsSuccess = true;
                        // return Json(new { data = res, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                        return RedirectToAction("Customernotifications", new { customerID = CustomerNotificationModel.CustomerID });
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                        msg = "Data is not correct";
                        IsSuccess = false;
                    }

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("Customername", "Customer with same name already exist!");

                    //}

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
                msg = e.Message;
                IsSuccess = false;
            }
            return View(CustomerNotificationModel); //return Json(new { data = CustomerNotificationModel, msg = msg, IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCustomerNotifications(long CustomerNotificationID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new CustomerService();
                retval = _service.deleteCustomerNotificationsByID(CustomerNotificationID);
                if (retval > 0)
                {
                    msg = "Customer Notifications deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Customer notification is in use cannot be deleted.";
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

        #region ApproveCustomer
        public ActionResult CustomerMasterReport()

        {
            ViewBag.Title = "Customer Master Report";
            return View();
        }
        public string CustomerMasterReportData(string type, string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            sSearch = sSearch.ToLower();
            _service = new CustomerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.CustomerModel> data = _service.getCustomerMasterReport(loggedUserID);
            if (!string.IsNullOrEmpty(sSearch))
            {

                data = data.Where(x => x.CustomerName.ToLower().Contains(sSearch) || x.Address.ToLower().Contains(sSearch) || x.Address2.ToLower().Contains(sSearch) || x.Address3.ToLower().Contains(sSearch) || x.City.ToLower().Contains(sSearch) || x.StateName.ToLower().Contains(sSearch) || x.Telephone.ToLower().Contains(sSearch))
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

        public FileResult ExportToExcelCustomerMaster()
        {
            _service = new CustomerService();

            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            var dt = _service.getCustomerMasterReportdt(loggedUserID);
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
            string attachment = "attachment; filename=CustomerList"+DateTime.Now.Ticks.ToString()+".xls";
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
            return File(attachment, "application/vnd.ms-excel", "CustomerMasterReportListing'" + DateTime.UtcNow.Date.ToString() + "'.xls");
        }

        #endregion




        
    }
}