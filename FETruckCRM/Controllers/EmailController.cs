using System;
using System.Collections.Generic;
using System.Linq;
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

    public class EmailController : Controller
    {
        // GET: EquipmentType
        EmailService _service;
        // GET: Admin/EquipmentType
        #region Email
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new EmailService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.EmailModel> data = _service.getAllEmails(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddEmail(Int64? id = 0)
        {
            _service = new EmailService();

            EmailModel objModel = new EmailModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.EmailTypeList = EmailService.getAllEmailTypes();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Email";
            if (id > 0)
            {
                _service = new EmailService();

                objModel = _service.getEmailByEmailID(id);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.EmailTypeList = EmailService.getAllEmailTypes();
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Email";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddEmail(EmailModel Model)
        {
            _service = new EmailService();
            Model.StatusList = HtmlHelperExtension.GetStatusListItems();
            Model.EmailTypeList = EmailService.getAllEmailTypes();

            // List<SelectListItem> selectedItems = EquipmentTypeModel.FormList.Where(p =>   EquipmentTypeModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Submit = Model.EmailID > 0 ? "Update" : "Save";
            ViewBag.Title = (Model.EmailID > 0 ? "Edit" : "Add") + " Email";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_service.CheckEmailType(Model.EmailID.ToString(),Model.strEmailTypeID))
                    {

                        Model.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        var res = _service.RegisterEmail(Model);

                        if (res > 0)
                        {

                            TempData["Success"] = "Email " + (Model.EmailID > 0 ? "updated" : "saved") + " successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("strEmailTypeID", "Email with same email type already exist!");

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
            return View(Model);
        }

        [HttpPost]
        public ActionResult Delete(long EmailID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new EmailService();
                retval = _service.deleteEmail(EmailID);

                if (retval > 0)
                {
                    msg = "Email deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Email is in use cannot be deleted.";
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