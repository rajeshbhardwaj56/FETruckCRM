using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

    public class ShipperController : Controller
    {
        // GET: Shipper
        ShipperService _service;
        // GET: Admin/Shipper
        #region Shipper
        public ActionResult Index(int? type)
        
        {
            ViewBag.Title = type == 1 ? "Shipper" : "Consignee";
            ViewBag.ButtonTitle= type == 1 ? "Add Shipper" : "Add Consignee";
            ViewBag.ShipperType = type;
            return View();
        }
        public ActionResult ShipperIndex()
        {
            
            return View();
        }
        public ActionResult loaddata(string type)
        {
            _service = new ShipperService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.ShipperModel> data = _service.getAllShippers(type, loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public string loaddataByPaging(string type,string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new ShipperService();
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "ShipperName";
            if (sortColumnIndex == 1)
            {
                sorCol = "Address";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "Telephone";
            }
            else if (sortColumnIndex == 3)
            {
                sorCol = "strStatusInd";
            }
            else if (sortColumnIndex == 4)
            {
                sorCol = "CreatedDate";
            }
           
            else if (sortColumnIndex == 5)
            {
                sorCol = "AddedByUser";
            }
            else if (sortColumnIndex == 6)
            {
                sorCol = "TeamLead";
            }
            else if (sortColumnIndex == 7)
            {
                sorCol = "TeamManager";
            }
            IList<FETruckCRM.Models.ShipperModel> data = _service.getAllShippersByPaging(type, loggedUserID, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
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

        [HttpGet]
        public ActionResult AddShipper( string type,Int64? id = 0)
        {
            _service = new ShipperService();

            ShipperModel objModel = new ShipperModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            //objModel.strStatusInd = "1";
            objModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            objModel.CountryList = ShipperService.getCountryList();
            objModel.StateList = CareerService.getStateList(0);

            objModel.ShipperType = Convert.ToInt32(type);
            ViewBag.Submit = "Save";
            ViewBag.Title = type=="1"?"Add Shipper":"Add Consignee";
            if (id > 0)
            {
                _service = new ShipperService();

                objModel = _service.getShipperByShipperID(id);  
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
                objModel.CountryList = ShipperService.getCountryList();
                objModel.StateList = ShipperService.getStateList(objModel.CountryID);
                ViewBag.Submit = "Update";
                ViewBag.Title = type == "1" ? "Edit Shipper" : "Edit Consignee";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddShipper(ShipperModel ShipperModel)
        {
            _service = new ShipperService();
            ShipperModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            ShipperModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            ShipperModel.CountryList = ShipperService.getCountryList();
            ShipperModel.StateList = ShipperService.getStateList((string.IsNullOrEmpty(ShipperModel.strCountryID)?(long)0: Convert.ToInt64(ShipperModel.strCountryID)));
            // List<SelectListItem> selectedItems = ShipperModel.FormList.Where(p =>   ShipperModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = (ShipperModel.ShipperID > 0 ? "Edit" : "Add") + (ShipperModel.ShipperType==1?  " Shipper": " Consignee");
            ViewBag.Submit = ShipperModel.ShipperID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_service.CheckShipperName(ShipperModel.ShipperName, ShipperModel.ShipperID))
                    //{
                       
                        ShipperModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    ShipperModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);
                        var res = _service.RegisterShipper(ShipperModel);

                        if (res > 0)
                        {

                            TempData["Success"] = (ShipperModel.ShipperType == 1 ? " Shipper "  : " Consignee ") + (ShipperModel.ShipperID > 0 ? "updated" : "saved") + " successfully";
                            return RedirectToAction("Index",new { type= ShipperModel.ShipperType });
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong try later!");
                            ViewBag.Error = "something went wrong try later!";
                        }

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("Shippername", "Shipper with same name already exist!");

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
            return View(ShipperModel);
        }



        [HttpGet]
        public ActionResult AddLoadShipper(string type, Int64? id = 0)
        {
            _service = new ShipperService();

            ShipperModel objModel = new ShipperModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            //objModel.strStatusInd = "1";
            objModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            objModel.CountryList = ShipperService.getCountryList();
            objModel.StateList = CareerService.getStateList(0);

            objModel.ShipperType = Convert.ToInt32(type);
            ViewBag.Submit = "Save";
            ViewBag.Title = type == "1" ? "Add Shipper" : "Add Consignee";
            if (id > 0)
            {
                _service = new ShipperService();

                objModel = _service.getShipperByShipperID(id);
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
                objModel.CountryList = ShipperService.getCountryList();
                objModel.StateList = ShipperService.getStateList(objModel.CountryID);
                ViewBag.Submit = "Update";
                ViewBag.Title = type == "1" ? "Edit Shipper" : "Edit Consignee";

            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddLoadShipper(ShipperModel ShipperModel)
        {
            _service = new ShipperService();
            ShipperModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            ShipperModel.AppointmentsList = HtmlHelperExtension.GetAppointmentListItems();
            ShipperModel.CountryList = ShipperService.getCountryList();
            ShipperModel.StateList = ShipperService.getStateList((string.IsNullOrEmpty(ShipperModel.strCountryID) ? (long)0 : Convert.ToInt64(ShipperModel.strCountryID)));
            // List<SelectListItem> selectedItems = ShipperModel.FormList.Where(p =>   ShipperModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Title = (ShipperModel.ShipperID > 0 ? "Edit" : "Add") + (ShipperModel.ShipperType == 1 ? " Shipper" : " Consignee");
            ViewBag.Submit = ShipperModel.ShipperID > 0 ? "Update" : "Save";
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_service.CheckShipperName(ShipperModel.ShipperName, ShipperModel.ShipperID))
                    //{

                    ShipperModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    ShipperModel.LastModifiedByID = Convert.ToInt64(Session["UserID"]);
                    var res = _service.RegisterShipper(ShipperModel);

                    if (res > 0)
                    {

                        TempData["Success"] = (ShipperModel.ShipperType == 1 ? " Shipper " : " Consignee ") + (ShipperModel.ShipperID > 0 ? "updated" : "saved") + " successfully";
                        return RedirectToAction("ShipperIndex", new { type = ShipperModel.ShipperType });
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        ViewBag.Error = "something went wrong try later!";
                    }

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("Shippername", "Shipper with same name already exist!");

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
            return View(ShipperModel);
        }


        [HttpPost]
        public ActionResult Delete(long ShipperID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new ShipperService();
                retval = _service.deleteShipper(ShipperID,Convert.ToInt64(Session["UserID"]));

                if (retval > 0)
                {
                    msg = "Shipper deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "Shipper is in use cannot be deleted.";
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
                _service = new ShipperService();
                retval = ShipperService.getStateList(Convert.ToInt64(CountryID));

                
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