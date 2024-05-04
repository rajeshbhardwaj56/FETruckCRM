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

    public class FactoringCompanyController : Controller
    {
        // GET: FactoringCompany
        FactoringCompanyService _service;
        // GET: Admin/FactoringCompany
        #region FactoringCompany
        public ActionResult Index(int? type)
        
        {
            ViewBag.Title = "Factoring Company";
            ViewBag.ButtonTitle="Add FactoringCompany";
            ViewBag.FactoringCompanyType = type;
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new FactoringCompanyService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.FactoringCompanyModel> data = _service.getAllFactoringCompanys(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddFactoringCompany( string type,Int64? id = 0)
        {
            _service = new FactoringCompanyService();

            FactoringCompanyModel objModel = new FactoringCompanyModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            objModel.CountryList = FactoringCompanyService.getCountryList();
            objModel.StateList = FactoringCompanyService.getStateList(0);
            objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();
            objModel.strStatusInd = "1";

            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Factoring Company";
            if (id > 0)
            {
                _service = new FactoringCompanyService();

                objModel = _service.getFactoringCompanyByFactoringCompanyID(id);  
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
                objModel.CountryList = FactoringCompanyService.getCountryList();
                objModel.StateList = FactoringCompanyService.getStateList(objModel.CountryID);
                objModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

                ViewBag.Submit = "Update";
                ViewBag.Title =  "Edit FactoringCompany" ;

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddFactoringCompany(FactoringCompanyModel FactoringCompanyModel)
        {
            _service = new FactoringCompanyService();
            FactoringCompanyModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            FactoringCompanyModel.CurrencyList = HtmlHelperExtension.GetCurrencyListItems();
            FactoringCompanyModel.CountryList = FactoringCompanyService.getCountryList();
            FactoringCompanyModel.PaymentTermsList = FactoringCompanyService.getPaymentTermsList();

            FactoringCompanyModel.StateList = FactoringCompanyService.getStateList((string.IsNullOrEmpty(FactoringCompanyModel.strCountryID)?(long)0: Convert.ToInt64(FactoringCompanyModel.strCountryID)));
            // List<SelectListItem> selectedItems = FactoringCompanyModel.FormList.Where(p =>   FactoringCompanyModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = (FactoringCompanyModel.FCID > 0 ? "Edit" : "Add") + " Factoring Company";
            ViewBag.Submit = FactoringCompanyModel.FCID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_service.CheckFactoringCompanyName(FactoringCompanyModel.FactoringCompanyName, FactoringCompanyModel.FCID))
                    //{
                       
                        FactoringCompanyModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        var res = _service.RegisterFactoringCompany(FactoringCompanyModel);

                        if (res > 0)
                        {

                            TempData["Success"] = "Factoring Company " + (FactoringCompanyModel.FCID > 0 ? "updated" : "saved") + " successfully";
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
                    //    ModelState.AddModelError("FactoringCompanyname", "FactoringCompany with same name already exist!");

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
            return View(FactoringCompanyModel);
        }

        [HttpPost]
        public ActionResult Delete(long FCID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new FactoringCompanyService();
                retval = _service.deleteFactoringCompany(FCID);

                if (retval > 0)
                {
                    msg = "FactoringCompany deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "FactoringCompany is in use cannot be deleted.";
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

        [HttpPost]
        public ActionResult loadStates(string CountryID)
        {
            List<SelectListItem> retval ;
            string msg = "";
            try
            {
                _service = new FactoringCompanyService();
                retval = FactoringCompanyService.getStateList(Convert.ToInt64(CountryID));

                
                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = null;
            }
            return Json(new { data = retval, msg = msg }, JsonRequestBehavior.AllowGet);
        }
        
    }
}