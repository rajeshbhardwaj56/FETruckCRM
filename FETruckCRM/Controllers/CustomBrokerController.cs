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

    public class CustomBrokerController : Controller
    {
        // GET: CustomBroker
        CustomBrokerService _service;
        // GET: Admin/CustomBroker
        #region CustomBroker
        public ActionResult Index(int? type)
        {
            ViewBag.Title = "Custom Broker";
            ViewBag.ButtonTitle="Add Custom Broker";
            ViewBag.CustomBrokerType = type;
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new CustomBrokerService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.CustomBrokerModel> data = _service.getAllCustomBrokers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddCustomBroker( string type,Int64? id = 0)
        {
            _service = new CustomBrokerService();
            CustomBrokerModel objModel = new CustomBrokerModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.strStatusInd = "1";
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Custom Broker";
            if (id > 0)
            {
                _service = new CustomBrokerService();
                objModel = _service.getCustomBrokerByCustomBrokerID(id);  
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                ViewBag.Submit = "Update";
                ViewBag.Title =  "Edit Custom Broker" ;

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddCustomBroker(CustomBrokerModel CustomBrokerModel)
        {
            _service = new CustomBrokerService();
            CustomBrokerModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            // List<SelectListItem> selectedItems = CustomBrokerModel.FormList.Where(p =>   CustomBrokerModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = (CustomBrokerModel.CustomBrokerID > 0 ? "Edit" : "Add") + " Custom Broker";
            ViewBag.Submit = CustomBrokerModel.CustomBrokerID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_service.CheckCustomBrokerName(CustomBrokerModel.CustomBrokerName, CustomBrokerModel.FCID))
                    //{
                       
                        CustomBrokerModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        var res = _service.RegisterCustomBroker(CustomBrokerModel);

                        if (res > 0)
                        {

                            TempData["Success"] = "Custom Broker " + (CustomBrokerModel.CustomBrokerID > 0 ? "updated" : "saved") + " successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                        }

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("CustomBrokername", "CustomBroker with same name already exist!");

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
            }
            return View(CustomBrokerModel);
        }

        [HttpPost]
        public ActionResult Delete(long CustomBrokerID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new CustomBrokerService();
                retval = _service.deleteCustomBroker(CustomBrokerID);

                if (retval > 0)
                {
                    msg = "Custom Broker deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "CustomBroker is in use cannot be deleted.";
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