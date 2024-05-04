using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

    public class LogController : Controller
    {
        // GET: User
        LogService _service;
        // GET: Admin/User
        #region Log
        public ActionResult Index()
        {
            var model=new LogInputModel();
            model.FromDate = DateTime.Now.AddYears(-1).ToShortDateString();
            model.ToDate = DateTime.Now.ToShortDateString();
            model.UserList = new List<SelectListItem>();
            model.UserList.Add(new SelectListItem() {Text="--ALL---",Value="0",Selected=true});
            var userList = UserService.getUsersList(0).Where(x=>!String.IsNullOrEmpty(x.Text) && !String.IsNullOrEmpty(x.Value)).OrderBy(x=>x.Text);
            userList.ForEach(x => {model.UserList.Add(new SelectListItem() { Text = x.Text,Value=x.Value });});
            return View(model);
        }
        public string loadLogdata(int userId,string frmDate,string toDate,string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            _service = new LogService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Params["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Params["sSortDir_0"];
            var sorCol = "LogDate";
            if (sortColumnIndex == 1)
            {
                sorCol = "User";
            }
            if (sortColumnIndex == 2)
            {
                sorCol = "Module";
            }


            DataSet ds = _service.getLogs(userId,frmDate,toDate, iDisplayStart, iDisplayLength, sSearch, sorCol, sortDirection);
            var objList = new List<LogModel>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    LogModel objModel = new LogModel();
                    objModel.User = Convert.ToString(dr["User"]);
                    objModel.LogDate = Convert.ToString(dr["LogDate"]);
                    objModel.ModuleName = Convert.ToString(dr["ModuleName"]);
                    objModel.ColumnName = Convert.ToString(dr["ColumnName"]);
                    objModel.OldValue = Convert.ToString(dr["OldValue"]);
                    objModel.NewValue = Convert.ToString(dr["NewValue"]);
                    objModel.EditMode = Convert.ToString(dr["EditMode"]);
                    objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                    objList.Add(objModel);
                }
            }

            long totalRecord = objList.Count > 0 ? objList.FirstOrDefault().TotalRecords : 0;
            var result = objList;
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

        public ActionResult ExportToExcel(int userId,string frmDate,string toDate)
        {
            _service = new LogService();
            var loggedUserID = Convert.ToInt64(Session["UserID"]);
            var sorCol = "LogDate";
            var ds= _service.getLogs(userId, frmDate, toDate, 0, int.MaxValue, "", sorCol, "desc");
            var dt = new System.Data.DataTable();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                dt = ds.Tables[0];
            
            var grdReport = new System.Web.UI.WebControls.GridView();
            grdReport.DataSource = dt;
            grdReport.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        #endregion

    }
}