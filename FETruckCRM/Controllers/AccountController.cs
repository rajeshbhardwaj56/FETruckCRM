 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using FETruckCRM.CustomFilter;
using FETruckCRM.Data;
using FETruckCRM.Models;

namespace FETruckCRM.Controllers
{
    [ExceptionHandler]

    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login()
                                                                      {
            LoginModel objModel = new LoginModel();
            objModel.OTP = string.Empty;
            return View("Login", objModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel LoginModel)
        {
            int result = 0;
            LoginService objservice = new LoginService();
            if (objservice.IsValidUser(LoginModel.Email, LoginModel.Password))
            {
                UserModel objModel = new UserModel();
                objModel = objservice.getUserDetailsByUsername(LoginModel.Email);
                if (objModel.RoleID == 1 || objModel.UserID == 330 || objModel.UserID == 2)
                {
                    objModel = objservice.getUserDetailsByUsername(LoginModel.Email);
                    Session["UserName"] = objModel.FirstName + " " + objModel.LastName;
                    Session["Email"] = objModel.EmailId;
                    Session["UserID"] = objModel.UserID;
                    Session["RoleID"] = objModel.RoleID;
                    FormsAuthentication.SetAuthCookie(LoginModel.Email, false);
                    result = 99;
                }
                else
                {
                    UserService _services = new UserService();
                    var objSettings = _services.getSettingsID(1);
                    if (objSettings.IsLoginWithOTP == true)
                    {
                        objModel.OTP = GenerateRandomOtp();
                        bool isValid = objservice.SendUserOTP(objModel);
                        if (isValid)
                            result = 1;
                        else
                            result = -1;
                    }
                    else
                    {
                        objModel = objservice.getUserDetailsByUsername(LoginModel.Email);
                        Session["UserName"] = objModel.FirstName + " " + objModel.LastName;
                        Session["Email"] = objModel.EmailId;
                        Session["UserID"] = objModel.UserID;
                        Session["RoleID"] = objModel.RoleID;
                        FormsAuthentication.SetAuthCookie(LoginModel.Email, false);
                        result = 99;
                    }
                }
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoginWithOtp(LoginModel LoginModel)
        {
          
            LoginService objservice = new LoginService();
            int result = objservice.CheckUserOtp(LoginModel.Email, LoginModel.OTP);
            if (result>0)
            {
                UserModel objModel = new UserModel();
                objModel = objservice.getUserDetailsByUsername(LoginModel.Email);
                Session["UserName"] = objModel.FirstName + " " + objModel.LastName;
                Session["Email"] = objModel.EmailId;
                Session["UserID"] = objModel.UserID;
                Session["RoleID"] = objModel.RoleID;
                FormsAuthentication.SetAuthCookie(LoginModel.Email, false);
               
            }
            else if(result==-1)
            {
                ModelState.AddModelError("", "OTP is expired!");
                ViewBag.Error = "OTP is expired!";
            }
            else
            {
                ModelState.AddModelError("", "Wrong OTP!");
                ViewBag.Error = "Wrong OTP!";
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult SendOtp(string emailId)
        {
            LoginService objservice = new LoginService();
            int result = 0;
           var model = objservice.getUserDetailsByUsername(emailId);
            if(model!=null)
            {
                model.OTP = GenerateRandomOtp();
                bool isValid=objservice.SendUserOTP(model);
                if (isValid)
                    result = 1;
                else
                    result=-1;
            }

            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public JsonResult Timeout()
        {
            // For you to display an error message when the login page is loaded, in case you want it
            TempData["hasError"] = true;
            TempData["errorMessage"] = "Your session expired, please log-in again.";

            return Json(new
            {
                @timeout = true,
                url = Url.Content("~/AccountController/Login")
            }, JsonRequestBehavior.AllowGet);
            // return RedirectToAction("Login", "Account");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ChangePasswordModel model)
        {
          var _LoginService = new LoginService();
            try
            {
                if (_LoginService.CheckAdminUsername(model.EmailId))
                {
                    var cus = _LoginService.ChangePassword(model.EmailId, model.Password);

                    ViewBag.Success = "Password changed successfully.";
                }
                else
                {
                    ViewBag.Error = "Some error occured. Please try again!";
                }
            }
            catch (Exception ce)
            {
                ViewBag.Error = "Some error occured. Please try again!";
            }
            return View(model);
        }


        public ActionResult CheckEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckEmail(CheckEmailModel model)
        {
            var _LoginService = new LoginService();
            try
            {
                if (_LoginService.CheckAdminUsername(model.EmailId))
                {
                    var cus = _LoginService.getUserDetailsByUsername(model.EmailId);
                    if (_LoginService.SendEmail(cus))
                    {
                        ViewBag.Success = "Change Password request has been sent on your email successfully.";
                    }
                    else { 
                    
                ViewBag.Error = "Some error occured. Please try again!";

                    }
                }
                else
                {
                    ViewBag.Error = "Email address does not exists in the system!";
                }
            }
            catch (Exception ce)
            {
                ViewBag.Error = "Some error occured. Please try again!";
            }
            return View(model);
        }


        public ActionResult ResetPassword(string un)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.UserID = un;
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var _LoginService = new LoginService();
            try
            {
                if (_LoginService.CheckUserByUserID(model.UserID))
                {
                    var cus = _LoginService.ChangeUserPassword(model.UserID, model.Password);

                    ViewBag.Success = "Password changed successfully.";
                }
                else
                {
                    ViewBag.Error = "Some error occured. Please try again!";
                }
            }
            catch (Exception ce)
            {
                ViewBag.Error = "Some error occured. Please try again!";
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            if (Session["Email"] != null)
            {
                ChangePasswordModel model = new ChangePasswordModel();
                LoginService objservice = new LoginService();

                UserModel objModel = new UserModel();
                model.UserID = (long)Session["UserID"];
                model.EmailId = Session["Email"].ToString();
            return View(model);
            }
            else {

                return RedirectToAction("Logout");
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var _LoginService = new LoginService();
            try
            {
                if (_LoginService.IsValidUser(model.EmailId, model.OldPassword))
                {
                    if (_LoginService.CheckAdminUsername(model.EmailId.ToString()))
                    {
                        var cus = _LoginService.ChangeUserPasswordByEmail(model.EmailId.ToString(), model.Password);

                        TempData["Success"] =  "Password changed successfully.";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Some error occured. Please try again!";
                    }
                }
                {
                    ViewBag.Error = "Old Password is incorrect. Please try again!";
                }
            }
            catch (Exception ce)
            {
                ViewBag.Error = "Some error occured. Please try again!";
            }
            return View(model);
        }

        public bool SendEmail(UserModel model)
        {
            bool res = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Dear " + model.FirstName + " " + model.LastName + ",<br/> Click on below given link to Reset Your Password<br/>");
                // sb.Append("<a href=http://localhost:57355/Account/reset?un=" + model.UserID);
                sb.Append("<a href=" + Convert.ToString(ConfigurationManager.AppSettings["ResetPasswordURL"]) + model.UserID);
                sb.Append(">Click here to change your password</a><br/>");
                sb.Append("<b>Thanks</b>,<br> Fleet Experts <br/>");
                MailMessage message = new MailMessage();
                using (var smtp = new SmtpClient())
                {

                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                    message.To.Add(new MailAddress(model.EmailId));
                    message.Subject = "Fleet Experts - Reset Password";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = sb.ToString();
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    smtp.Host = ConfigurationManager.AppSettings["host"]; //for gmail host  
                    smtp.UseDefaultCredentials = Convert.ToInt32(ConfigurationManager.AppSettings["defaultcredential"]) == 1 ? true : false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
                    smtp.EnableSsl = Convert.ToInt32(ConfigurationManager.AppSettings["enablessl"]) == 1 ? true : false;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    res = true;
                }



            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        private string GenerateRandomOtp()
        {
            Random generator = new Random();
            long r = generator.Next(100000,999999);
            string pwd = r.ToString().PadLeft(6, '0');
            return pwd;
        }
    }
}
