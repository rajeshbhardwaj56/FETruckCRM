using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckCRM.Common;
using TruckCRM.Data;
using TruckCRM.Models;

namespace TruckCRM.Controllers
{
    [SessionExpire]
    public class NewLoadController : Controller
    {

        public ActionResult Index(string loadid)
        {

            ViewBag.Load = Convert.ToString(loadid);
            return View();
        }

        [HttpPost]
        public JsonResult AutoCompleteCustomer(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = ShipperService.AutoCompleteCustomer(loggedUserID, prefix);
            return Json(entities);
        }
        [HttpPost]
        public JsonResult AutoCompleteShipper(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = ShipperService.AutoCompleteShipper(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteConsignee(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = ShipperService.AutoCompleteConsignee(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult AutoCompleteCarrier(string prefix)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            List<Autocomplete> entities = ShipperService.AutoCompleteCarrier(loggedUserID, prefix);
            return Json(entities);
        }

        [HttpPost]
        public JsonResult BindDropDowns()
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var dt = ShipperService.BindDropDowns(loggedUserID);
            //
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Json(json);
        }

        [HttpPost]
        public JsonResult saveload(LoadModeldata load, List<LoadShipperModeldata> shippers, List<LoadShipperModeldata> consignee)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            try
            {
                ShipperService.SaveLoad(load, shippers, consignee, loggedUserID);
            }
            catch (Exception ex)
            {
                 
            }
            string json = JsonConvert.SerializeObject("", Formatting.Indented);
            return Json(json);
        }

        [HttpPost]
        public JsonResult getload(string loadid)
        {
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var dt = ShipperService.getload(loadid);
            //
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Json(json);
        }
        //
    }


}