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

    public class EquipmentTypeController : Controller
    {
        // GET: EquipmentType
        EquipmentTypeService _service;
        // GET: Admin/EquipmentType
        #region EquipmentType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new EquipmentTypeService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            IList<FETruckCRM.Models.EquipmentTypeModel> data = _service.getAllEquipmentTypes(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddEquipmentType(Int64? id = 0)
        {
            _service = new EquipmentTypeService();

            EquipmentTypeModel objModel = new EquipmentTypeModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Equipment Type";
            if (id > 0)
            {
                _service = new EquipmentTypeService();

                objModel = _service.getEquipmentTypeByEquipmentTypeID(id);  
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit Equipment Type";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddEquipmentType(EquipmentTypeModel EquipmentTypeModel)
        {
            _service = new EquipmentTypeService();
            EquipmentTypeModel.StatusList = HtmlHelperExtension.GetStatusListItems();
           // List<SelectListItem> selectedItems = EquipmentTypeModel.FormList.Where(p =>   EquipmentTypeModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Submit = EquipmentTypeModel.EquipmentTypeID > 0 ? "Update" : "Save";
            ViewBag.Title = (EquipmentTypeModel.EquipmentTypeID > 0 ? "Edit" : "Add") + " EquipmentType";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_service.CheckEquipmentTypeName(EquipmentTypeModel.EquipmentTypeName, EquipmentTypeModel.EquipmentTypeID))
                    {
                       
                        EquipmentTypeModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        var res = _service.RegisterEquipmentType(EquipmentTypeModel);

                        if (res > 0)
                        {

                            TempData["Success"] = "EquipmentType " + (EquipmentTypeModel.EquipmentTypeID > 0 ? "updated" : "saved") + " successfully";
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
                        ModelState.AddModelError("EquipmentTypename", "Equipment Type with same name already exist!");

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
            return View(EquipmentTypeModel);
        }

        [HttpPost]
        public ActionResult Delete(long EquipmentTypeID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new EquipmentTypeService();
                retval = _service.deleteEquipmentType(EquipmentTypeID);

                if (retval > 0)
                {
                    msg = "EquipmentType deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "EquipmentType is in use cannot be deleted.";
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