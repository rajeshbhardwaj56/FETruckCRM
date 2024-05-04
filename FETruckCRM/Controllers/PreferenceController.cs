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

    public class PreferenceController : Controller
    {
        // GET: CustomBroker
        PreferencesService _service;
        // GET: Admin/CustomBroker
        #region Preference
        [HttpGet]
        public ActionResult AddPreference(string type, Int64? id = 0)
        {
            _service = new PreferencesService();
            PreferencesModel objModel = new PreferencesModel();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add T & C Notes";
            objModel = _service.getAllPreferencess();
            ViewBag.Submit = "Update";
            ViewBag.Title = "Edit T & C Notes";
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPreference(PreferencesModel PreferenceModel)
        {
            _service = new PreferencesService();
            ViewBag.Title = (PreferenceModel.PreferenceID > 0 ? "Edit " : "Add ") + "T & C Notes";
            ViewBag.Submit = PreferenceModel.PreferenceID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _service.RegisterPreferences(PreferenceModel);
                    if (res > 0)
                    {
                        TempData["Success"] = "T & C Notes " + (PreferenceModel.PreferenceID > 0 ? "updated" : "saved") + " successfully";
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
            return View(PreferenceModel);
        }

        //[HttpPost]
        //public ActionResult Delete(long CustomBrokerID)
        //{
        //    long retval = -1;
        //    string msg = "";
        //    try
        //    {
        //        _service = new CustomBrokerService();
        //        retval = _service.deleteCustomBroker(CustomBrokerID);

        //        if (retval > 0)
        //        {
        //            msg = "Custom Broker deleted successfully.";
        //        }
        //        else if (retval == -2)
        //        {
        //            msg = "CustomBroker is in use cannot be deleted.";
        //        }
        //        else
        //        {
        //            msg = "Some error occurred. Please try again!";
        //        }
        //        // TODO: Add delete logic here
        //    }
        //    catch (Exception ce)
        //    {
        //        msg = ce.Message; ;
        //        retval = -1;
        //    }
        //    return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

    }
}