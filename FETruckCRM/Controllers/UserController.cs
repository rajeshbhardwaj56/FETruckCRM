using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using FETruckCRM.Common;
using FETruckCRM.CustomFilter;
using FETruckCRM.Data;
using FETruckCRM.Models;

namespace FETruckCRM.Controllers
{
    [SessionExpire]
    [ExceptionHandler]

    public class UserController : Controller
    {
        // GET: User
        UserService _service;
        CustomerService _customerService;
        // GET: Admin/User
        #region User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loaddata()
        {
            _service = new UserService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);

            IList<FETruckCRM.Models.UserModel> data = _service.getAllUsers(loggedUserID);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddUser(Int64? id = 0)
        {

            _service = new UserService();

            UserModel objModel = new UserModel();
            objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            objModel.RoleList = UserService.getRoleList();
            objModel.EmployeeTypeList = UserService.getEmployeetypeList();
            objModel.SiteList = UserService.getSiteList();
            objModel.FormList = UserService.getFormList();
            objModel.UserList = UserService.getUsersList(0);
            objModel.GroupList = UserService.getGroupList();
            objModel.TeamLeadList = UserService.getTeamLeadListByManager(0,0);
            objModel.TeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(0,"0",0);
            objModel.OpsTeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(0, "0", 0);
            objModel.strTeamManager = " ";
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add User";
            objModel.SelectedSiteIds = new[] { 0} ;
            objModel.SelectedOpsManagers = new[] { 0} ;
            if (id > 0)
            {
                _service = new UserService();
                objModel = _service.getUserByUserID(id);
                objModel.UserList = UserService.getUsersList(objModel.UserID);
                objModel.EmployeeTypeList = UserService.getEmployeetypeList();
                objModel.SiteList = UserService.getSiteList();
                var siteIds = objModel.SiteID;
                if (objModel.SelectedSiteIds!=null && objModel.SelectedSiteIds.Length > 0)
                {
                    siteIds = string.Join(",", objModel.SelectedSiteIds);
                }
                objModel.GroupList = UserService.getGroupList();
                objModel.TeamLeadList = UserService.getTeamLeadListByManager(objModel.UserID, Convert.ToInt64(objModel.strTeamManager));
                objModel.TeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(objModel.UserID, siteIds, Convert.ToInt32(objModel.EmployeeType));
                objModel.OpsTeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(objModel.UserID, siteIds, Convert.ToInt32(objModel.EmployeeType));
                objModel.StatusList = HtmlHelperExtension.GetStatusListItems();
                objModel.RoleList = UserService.getRoleList();
                objModel.FormList = UserService.getFormList(objModel.RoleID);
                //objModel.FormList = UserService.getFormList();
                objModel.strTeamManager = string.IsNullOrEmpty(objModel.strTeamManager) ? " " : objModel.strTeamManager;
                ViewBag.Submit = "Update";
                ViewBag.Title = "Edit User";
            }
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult AddUser(UserModel UserModel)
        {
            string msg = string.Empty;
            bool isSuccess = false;
            if (UserModel.strGroupID == Convert.ToString((int)RolesEnum.TeamManager))
            {
                UserModel.strTeamLead = "0";
                UserModel.strTeamManager = "0";
            }
            else if (UserModel.strGroupID == Convert.ToString((int)RolesEnum.TeamLead))
            {
                UserModel.strTeamLead = "0";
            }

            _service = new UserService();
            UserModel.StatusList = HtmlHelperExtension.GetStatusListItems();
            UserModel.RoleList = UserService.getRoleList();
            UserModel.FormList = UserService.getFormList();
            UserModel.GroupList = UserService.getGroupList();
            UserModel.UserList = UserService.getUsersList(UserModel.UserID);
            //UserModel.TeamLeadList = UserService.getTeamLeadList(UserModel.UserID);
            //UserModel.TeamManagerList = UserService.getTeamManagerList(UserModel.UserID);
            UserModel.TeamLeadList = UserService.getTeamLeadListByManager(UserModel.UserID, Convert.ToInt64(UserModel.strTeamManager));
            UserModel.TeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(UserModel.UserID, Convert.ToString(UserModel.SiteID), Convert.ToInt32(UserModel.EmployeeType));
            UserModel.OpsTeamManagerList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(UserModel.UserID, Convert.ToString(UserModel.SiteID), Convert.ToInt32(UserModel.EmployeeType));

            UserModel.EmployeeTypeList = UserService.getEmployeetypeList();

            UserModel.SiteList = UserService.getSiteList();

            // List<SelectListItem> selectedItems = UserModel.FormList.Where(p =>   UserModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Submit = UserModel.UserID > 0 ? "Update" : "Save";
            ViewBag.Title = (UserModel.UserID > 0 ? "Edit" : "Add") + " User";
            try
            {
                if (ModelState.IsValid)
                {



                    if ( !_service.CheckUserEmail(UserModel.EmailId, UserModel.UserID))
                    {
                        if (UserModel.EmployeeType == "1")
                        {
                            if (!_service.CheckEmployeeCode(UserModel.EmployeeCode, UserModel.UserID))
                            {
                                if (UserModel.UserID == 0)
                                {
                                    var encryptpassowrd = HtmlHelperExtension.MD5Hash(UserModel.Password);
                                    UserModel.Password = encryptpassowrd;
                                }
                                else if (!string.IsNullOrEmpty(UserModel.NewPassword))
                                {
                                    var encryptpassowrd = HtmlHelperExtension.MD5Hash(UserModel.NewPassword);
                                    UserModel.Password = encryptpassowrd;
                                }
                                UserModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                                var res = _service.RegisterUser(UserModel);

                                if (res > 0)
                                {

                                    msg = "User " + (UserModel.UserID > 0 ? "updated" : "saved") + " successfully";
                                    isSuccess = true;
                                }
                                else
                                {
                                    msg = "something went wrong try later!";
                                }
                            }
                            else
                            {
                                msg = "User with same employee code has already exist!";
                            }
                        }
                        else
                        {

                            if (UserModel.UserID == 0)
                            {
                                var encryptpassowrd = HtmlHelperExtension.MD5Hash(UserModel.Password);
                                UserModel.Password = encryptpassowrd;
                            }
                            else if (!string.IsNullOrEmpty(UserModel.NewPassword))
                            {
                                var encryptpassowrd = HtmlHelperExtension.MD5Hash(UserModel.NewPassword);
                                UserModel.Password = encryptpassowrd;
                            }
                            UserModel.CreatedByID = Convert.ToInt64(Session["UserID"]);
                            var res = _service.RegisterUser(UserModel);

                            if (res > 0)
                            {

                                msg = "User " + (UserModel.UserID > 0 ? "updated" : "saved") + " successfully";
                                isSuccess = true;
                            }
                            else
                            {
                                msg = "something went wrong try later!";
                            }
                        }
                    }

                    else
                    {
                        msg = "User with same email id has already exist!";

                    }

                }
                else
                {

                    msg = "Data is not correct";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return Json(new { msg = msg, IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Delete(long UserID)
        {
            long retval = -1;
            string msg = "";
            try
            {
                _service = new UserService();
                retval = _service.deleteUser(UserID, Convert.ToInt64(Session["UserID"]));

                if (retval > 0)
                {
                    msg = "User deleted successfully.";
                }
                else if (retval == -2)
                {
                    msg = "User is in use cannot be deleted.";
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
        public ActionResult getTeamManager(long UserID)
        {
            _service = new UserService();
            string retval = _service.getUsersTeamManager(UserID);
            return Json(new { data = retval }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FormPermission()
        {
            _service = new UserService();

            GroupFormPermission objModel = new GroupFormPermission();
            objModel.RoleList = UserService.getRoleList();
            objModel.FormList = UserService.getFormList();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Form Permissions";
            objModel.RoleList = UserService.getRoleList();
            objModel.FormList = UserService.getFormList();
            return View(objModel);
        }
        // Post:Register 
        [HttpPost]
        public ActionResult FormPermission(GroupFormPermission Model)
        {
            _service = new UserService();
            Model.RoleList = UserService.getRoleList();
            Model.FormList = UserService.getFormList();

            // List<SelectListItem> selectedItems = UserModel.FormList.Where(p =>   UserModel.strFormid.Contains(int.Parse(p.Value))).ToList();
            ViewBag.Submit = "Save";
            ViewBag.Title = "Add Form Permissions";
            try
            {
                if (ModelState.IsValid)
                {



                    Model.CreatedByID = Convert.ToInt64(Session["UserID"]);
                    var result = _service.deleteFormPermission(Convert.ToInt32(Model.strRoleID));
                    var formids = Model.strFormids.Split(',');
                    long res = 0;
                    foreach (var item in formids)
                    {
                        Model.strFormid = item;
                        Model.CreatedByID = Convert.ToInt64(Session["UserID"]);
                        _service.AddFormPermissions(Model);
                        res++;
                    }
                    if (res > 0)
                    {

                        TempData["Success"] = "Forms Permission saved successfully";
                        return RedirectToAction("FormPermission");
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                        TempData["Error"] = "something went wrong try later!";
                    }



                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    TempData["Error"] = "Data is not correct";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                TempData["Error"] = e.Message;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult loadForms(string RoleID)
        {
            var retval = "";
            string msg = "";
            try
            {
                _service = new UserService();
                List<GroupFormPermission> list = _service.getFormsByRoleID(Convert.ToInt32(RoleID));

                foreach (var item in list)
                {
                    retval += item.FormID + ",";
                }


                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                retval = null;
            }
            return Json(new { data = retval.TrimEnd(','), msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loadFormsdata(string RoleID)
        {
            var selectList = new List<SelectListItem>();
            string msg = "";
            try
            {
                _service = new UserService();
                List<GroupFormPermission> list = _service.getFormsByRoleID(Convert.ToInt32(RoleID));

                foreach (var item in list)
                {
                    selectList.Add(new SelectListItem { Value = item.FormID.ToString(), Text = item.FormName });
                }


                // TODO: Add delete logic here
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                selectList = null;
            }
            return Json(new { data = selectList, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loadManager(string SID,string ETID)
        {
            var selectList = new List<SelectListItem>();
            string msg = "";
            try
            {
                selectList = UserService.getTeamManagerListBYSiteIDEmployeeTypeID(Convert.ToInt64(Session["UserID"]),Convert.ToString(SID), Convert.ToInt32(ETID) );
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                selectList = null;
            }
            return Json(new { data = selectList, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loadTeamLeadManager(string MID)
        {
            var selectList = new List<SelectListItem>();
            string msg = "";
            try
            {
                selectList = UserService.getTeamLeadListByManager(Convert.ToInt64(Session["UserID"]), Convert.ToInt32(MID));
            }
            catch (Exception ce)
            {
                msg = ce.Message; ;
                selectList = null;
            }
            return Json(new { data = selectList, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignCustomer()
        {
            _service = new UserService();
            _customerService = new CustomerService();
            var model = new UserCustomerModel();
            BindUserList(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult AssignCustomer(UserCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _service = new UserService();
                long result = _service.AssignCustomer(Convert.ToInt64(Session["UserID"]), model.SelectedUserId, model.SelectedCustomerId);
                if (result > 0)
                    TempData["Success"] = "Customer assigned successfully.";
                else
                    TempData["Error"] = "Something went wrong!";

            }
            BindUserList(model);
            return View(model);
        }


        public void BindUserList(UserCustomerModel model)
        {
            _service = new UserService();
            var users = _service.getAllUsers(Convert.ToInt64(Session["UserID"]));
            foreach (var user in users)
                model.UserList.Add(new SelectListItem() { Text = user.Alias, Value = Convert.ToString(user.UserID) });
        }

        public ActionResult BindCustomerList(UserCustomerModel model)
        {
            model.CustomerList = new List<SelectListItem>();
            _customerService = new CustomerService();
            var customers = _customerService.getAllCustomers(Convert.ToInt64(Session["UserID"]));
            customers = customers.Where(x => x.CreatedByID != model.SelectedUserId && x.AssignTo != model.SelectedUserId).ToList();

            foreach (var customer in customers)
                model.CustomerList.Add(new SelectListItem() { Text = customer.CustomerName, Value = Convert.ToString(customer.CustomerID) });

            return Json(model.CustomerList, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult _Menu()
        {
            GroupFormPermissionList obj = new GroupFormPermissionList();
            _service = new UserService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var loggedUserRoleID = Convert.ToInt32(Session["RoleID"]);
            obj.FormPermissionList = _service.getAllFormList(loggedUserID, loggedUserRoleID);
            return PartialView(obj);
        }

        #endregion

    }
}