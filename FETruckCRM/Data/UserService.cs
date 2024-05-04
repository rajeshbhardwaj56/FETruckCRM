using FETruckCRM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web.Services.Description;
using Spire.Pdf.Exporting.XPS.Schema;

namespace FETruckCRM.Data
{
    public class UserService
    {
        SqlConnection con;
        public UserService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterUser(UserModel objModel)
        {
            Int64 retVal = 0;
            var olddata = getUsertableByUserID(objModel.UserID);
            var editMode = objModel.UserID > 0 ? "Edit" : "Add";
            string query = "InsUpdUsers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", objModel.UserID);
                cmd.Parameters.AddWithValue("@FirstName", objModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", objModel.LastName);
                cmd.Parameters.AddWithValue("@Phone", objModel.Phone);
                cmd.Parameters.AddWithValue("@EmailId", objModel.EmailId);
                cmd.Parameters.AddWithValue("@Password", objModel.Password);
                cmd.Parameters.AddWithValue("@Status", objModel.strStatus == "1");
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@RoleID", objModel.strRoleID);
                cmd.Parameters.AddWithValue("@GroupID", objModel.strGroupID);
                cmd.Parameters.AddWithValue("@TeamLead", objModel.strTeamLead);
                cmd.Parameters.AddWithValue("@TeamManager", objModel.strTeamManager);
                cmd.Parameters.AddWithValue("@Fax", objModel.Fax);
                cmd.Parameters.AddWithValue("@EmployeeType", objModel.EmployeeType);
                cmd.Parameters.AddWithValue("@Alias", objModel.Alias);
                cmd.Parameters.AddWithValue("@SiteID", 1);// objModel.SiteID);
                cmd.Parameters.AddWithValue("@FormPermission", objModel.strFormid != null ? string.Join(",", objModel.strFormid) : "");
                cmd.Parameters.AddWithValue("@EmployeeCode", objModel.EmployeeCode);
                cmd.Parameters.AddWithValue("@OpsManagers", objModel.hdnSelectedOpsManagers);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);
                    #region Log
                    var newData = getUsertableByUserID(retVal);
                    if (newData.Rows.Count > 0)
                    {
                        DataRow newrow = newData.Rows[0];


                        if (editMode == "Add")
                        {
                            foreach (var col in newData.Columns)
                            {

                                string query1 = "insupdLog";
                                SqlCommand cmd1 = new SqlCommand(query1, con);
                                cmd1.Connection = con;
                                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@UserID", objModel.CreatedByID);
                                cmd1.Parameters.AddWithValue("@ModuleName", "User");
                                cmd1.Parameters.AddWithValue("@TableName", "tbUser");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.UserID);
                                cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                cmd1.Parameters.AddWithValue("@OldValue", "");
                                cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                DataTable dt1 = new DataTable();
                                con.Open();
                                sda1.Fill(dt1);
                                con.Close();

                            }
                        }
                        else
                        {
                            DataRow oldrow = olddata.Rows[0];
                            foreach (var col in newData.Columns)
                            {
                                if (Convert.ToString(newrow[col.ToString()]) != Convert.ToString(oldrow[col.ToString()]))
                                {
                                    string query1 = "insupdLog";
                                    SqlCommand cmd1 = new SqlCommand(query1, con);
                                    cmd1.Connection = con;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@UserID", objModel.CreatedByID);
                                    cmd1.Parameters.AddWithValue("@ModuleName", "User");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbUser");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.UserID);
                                    cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                    cmd1.Parameters.AddWithValue("@OldValue", Convert.ToString(oldrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    DataTable dt1 = new DataTable();
                                    con.Open();
                                    sda1.Fill(dt1);
                                    con.Close();
                                }
                            }
                        }
                    } 
                    #endregion
                }
                else
                {
                    retVal = -1;
                }
            }
            return retVal;
        }

        public List<UserModel> getAllUsers(long UserID)
        {
            List<UserModel> objList = new List<UserModel>();
            string query = "getUserListing";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserModel objModel = new UserModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.UserID = Convert.ToInt64(dr["UserID"]);
                        objModel.FirstName = Convert.ToString(dr["FirstName"]);
                        objModel.LastName = Convert.ToString(dr["LastName"]);
                        objModel.strTeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.strTeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.EmailId = Convert.ToString(dr["EmailId"]);
                        objModel.Username = Convert.ToString(dr["Username"]);
                        objModel.Phone = Convert.ToString(dr["Phone"]);
                        objModel.Password = Convert.ToString(dr["Password"]);
                        objModel.Role = Convert.ToString(dr["Role"]);
                        objModel.Status = Convert.ToBoolean(dr["Status"]);
                        objModel.strStatus = Convert.ToBoolean(dr["Status"]) == true ? "Active" : "Inactive";
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.EmployeeType = Convert.ToString(dr["EmployeeType"]);
                        objModel.Alias = Convert.ToString(dr["Alias"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.strCreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.SiteName = Convert.ToString(dr["SiteName"]);
                        objModel.EmployeeCode = Convert.ToString(dr["employeeCode"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

        public string getUsersTeamManager(long userID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var retval = "";
            string query = "getteamManager";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    retval = Convert.ToString(dr["TeamManager"]);
                }
            }
            return retval;
        }

        

        //public static long getUserRoleByID()
        //{ 


        //}
        public static List<SelectListItem> getUsersList(long userID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "proc_GetAllUsers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["Name"]) });
                    }
                }
            }
            return selectList;
        }


        public static List<SelectListItem> getRoleList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllRoles";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["RoleID"]).ToString(), Text = Convert.ToString(dr["Role"]) });
                    }
                }
            }
            return selectList;
        }


        public static List<SelectListItem> getEmployeetypeList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllEmployeetypeList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["EmployeeTypeID"]).ToString(), Text = Convert.ToString(dr["EmployeeType"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getSiteList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllSitesList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["SiteID"]).ToString(), Text = Convert.ToString(dr["SiteName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getGroupList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllGroups";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["GroupID"]).ToString(), Text = Convert.ToString(dr["GroupName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getTeamLeadList(long userID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllTeamLeads";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getTeamLeadListByManager(long userID,long ManagerID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getAllTeamLeadsByManager";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ManagerID", ManagerID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getTeamManagerList(long userID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllManagers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getTeamManagerListBYSiteIDEmployeeTypeID(long userID,string SiteID,int EmployeeTypeID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getAllManagersBySiteAndEmployeeType";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@SiteID", SiteID);
                cmd.Parameters.AddWithValue("@EmployeeTypeID", EmployeeTypeID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getFormList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllForms";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    //selectList.Add(new SelectListItem { Value = "", Text = "Please Select",Selected=true });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["FormID"]).ToString(), Text = Convert.ToString(dr["FormName"]) });
                    }
                }
            }
            return selectList;
        }

        public static List<SelectListItem> getFormList(int RoleID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "proc_getFormByRoleID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", RoleID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    //selectList.Add(new SelectListItem { Value = "", Text = "Please Select",Selected=true });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["FormID"]).ToString(), Text = Convert.ToString(dr["FormName"]) });
                    }
                }
            }
            return selectList;
        }

        public DataTable getUsertableByUserID(Int64? UserID)
        {
            DataTable dt = new DataTable();
            string query = "getuserdetailsByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                }

            }
            return dt;
        }

        public UserModel getUserByUserID(Int64? UserID)
        {
            UserModel objModel = new UserModel();
            string query = "getuserdetailsByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        objModel.UserID = Convert.ToInt64(dr["UserID"]);
                        objModel.FirstName = Convert.ToString(dr["FirstName"]);
                        objModel.LastName = Convert.ToString(dr["LastName"]);
                        objModel.EmailId = Convert.ToString(dr["EmailId"]);
                        objModel.Username = Convert.ToString(dr["Username"]);
                        objModel.GroupID = dr["GroupID"] != DBNull.Value ? Convert.ToInt64(dr["GroupID"]) : 0;
                        objModel.strGroupID = Convert.ToString(dr["GroupID"]);
                        objModel.TeamLead = dr["TeamLead"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dr["TeamLead"]);
                        objModel.strTeamLead = dr["TeamLead"] == DBNull.Value ? "0" : Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = dr["TeamManager"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dr["TeamManager"]);
                        objModel.strTeamManager = dr["TeamManager"] == DBNull.Value ? "0" : Convert.ToString(dr["TeamManager"]);
                        objModel.Phone = Convert.ToString(dr["Phone"]);
                        objModel.Password = Convert.ToString(dr["Password"]);
                        objModel.oldPassword = Convert.ToString(dr["Password"]);
                        objModel.Status = Convert.ToBoolean(dr["Status"]);
                        objModel.strStatus = (Convert.ToBoolean(dr["Status"]) == true) ? "1" : "0";
                        objModel.RoleID = Convert.ToInt32(dr["RoleID"]);
                        objModel.strRoleID = Convert.ToString(dr["RoleID"]); 
                        objModel.Fax = Convert.ToString(dr["Fax"]); 
                        objModel.EmployeeType = Convert.ToString(dr["EmployeeType"]); 
                        objModel.Alias = Convert.ToString(dr["Alias"]); 
                        objModel.RoleName = Convert.ToString(dr["Role"]); 
                        objModel.SiteID = Convert.ToString(dr["SiteID"]); 
                        objModel.EmployeeCode = Convert.ToString(dr["EmployeeCode"]); 

                        objModel.strFormid = null;

                        if (ds.Tables.Count > 1)
                        {
                            DataTable dt1 = ds.Tables[1];

                            if (dt1.Rows.Count > 0)
                            {
                                List<Int32> termsList = new List<Int32>();
                                foreach (DataRow dr1 in dt1.Rows)
                                {

                                    termsList.Add(Convert.ToInt32(dr1["FormID"]));

                                }
                                objModel.strFormid = termsList.ToArray();

                            }

                        }
                        if (ds.Tables.Count > 2)
                        {
                            DataTable dt2 = ds.Tables[2];

                            if (dt2.Rows.Count > 0)
                            {
                                List<Int32> termsList = new List<Int32>();
                                foreach (DataRow dr1 in dt2.Rows)
                                {

                                    termsList.Add(Convert.ToInt32(dr1["SiteID"]));

                                }
                                objModel.SelectedSiteIds = termsList.ToArray();

                            }

                        }
                        if (ds.Tables.Count > 3)
                        {
                            DataTable dt3 = ds.Tables[3];

                            if (dt3.Rows.Count > 0)
                            {
                                List<Int32> termsList = new List<Int32>();
                                foreach (DataRow dr1 in dt3.Rows)
                                {

                                    termsList.Add(Convert.ToInt32(dr1["TeamManagerID"]));

                                }
                                objModel.SelectedOpsManagers = termsList.ToArray();

                            }

                        }


                    }
                }

            }
            return objModel;
        }
        public bool CheckUserEmail(string EmailID, long UserID)
        {
            bool isvalid = false;
            string query = "proc_CheckUserEmailID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    isvalid = true;
                }
                else
                {
                    isvalid = false;
                }

            }
            return isvalid;
        }

        public bool CheckEmployeeCode(string employeeCode, long userID)
        {
            bool isvalid = false;
            string query = "proc_CheckUserEmployeeCode";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    isvalid = true;
                }
                else
                {
                    isvalid = false;
                }

            }
            return isvalid;
        }

        public long deleteUser(Int64 UserID, Int64 LoggedinUserId=0)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("proc_deleteUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);


                    if (isSuccess>0)
                    {
                        string query1 = "insupdLog";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Connection = con;
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@UserID", LoggedinUserId);
                        cmd1.Parameters.AddWithValue("@ModuleName", "User");
                        cmd1.Parameters.AddWithValue("@TableName", "tbUser");
                        cmd1.Parameters.AddWithValue("@PrimaryKey", UserID);
                        cmd1.Parameters.AddWithValue("@ColumnName", "IsDeleted");
                        cmd1.Parameters.AddWithValue("@OldValue", "False");
                        cmd1.Parameters.AddWithValue("@NewValue", "True");
                        cmd1.Parameters.AddWithValue("@EditMode", "Delete");
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        con.Open();
                        sda1.Fill(dt1);
                        con.Close();
                    }

                }
            }
            return isSuccess;
        }

        public Int64 AddFormPermissions(GroupFormPermission objModel)
        {
            Int64 retVal = 0;


            string query = "insupdGroupFormPermission";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", objModel.strRoleID);
                cmd.Parameters.AddWithValue("@FormID", objModel.strFormid);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);
                }
                else
                {
                    retVal = -1;
                }
            }
            return retVal;
        }
        public long deleteFormPermission(int RoleID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteFormPermission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", RoleID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);
                }
            }
            return isSuccess;
        }

        public Int64 AssignCustomer(long loginUserId,int userId,int customerId)
        {
            var _service = new CustomerService();
            Int64 rowEffected = 0;
            string query = "CreateUserCustomerAssociate";
            var customer = _service.getCustomerByCustomerID(customerId);
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId",userId);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                con.Open();
                rowEffected= cmd.ExecuteNonQuery();
                con.Close();
                if (rowEffected > 0 && customer != null)
                {
                        string query1 = "insupdLog";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Connection = con;
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@UserID", loginUserId);
                        cmd1.Parameters.AddWithValue("@ModuleName", "Assign Customer");
                        cmd1.Parameters.AddWithValue("@TableName", "tbCustomer");
                        cmd1.Parameters.AddWithValue("@PrimaryKey", customerId);
                        cmd1.Parameters.AddWithValue("@ColumnName", "Assignto");
                        cmd1.Parameters.AddWithValue("@OldValue", customer.AssignTo);
                        cmd1.Parameters.AddWithValue("@NewValue",userId);
                        cmd1.Parameters.AddWithValue("@EditMode", "Edit");
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        con.Open();
                        sda1.Fill(dt1);
                        con.Close();
                    }
            }
            return rowEffected;
        }


        public List<GroupFormPermission> getFormsByRoleID(int RoleID)
        {
            List<GroupFormPermission> objList = new List<GroupFormPermission>();
            string query = "proc_getFormByRoleID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", RoleID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            GroupFormPermission objModel = new GroupFormPermission();
                            objModel.GroupFormID = Convert.ToInt32(dr["GroupFormID"]);
                            objModel.RoleID = Convert.ToInt32(dr["RoleID"]);
                            objModel.FormID = Convert.ToInt32(dr["FormID"]);
                            objModel.strRoleID = Convert.ToString(dr["RoleID"]);
                            objModel.strFormid = Convert.ToString(dr["FormID"]);
                            objModel.FormName = Convert.ToString(dr["FormName"]);
                            objList.Add(objModel);
                        }
                    }
                }

            }
            return objList;
        }


        public List<FormModel> getAllFormList(long UserID, int RoleID)
        {
            List<FormModel> objList = new List<FormModel>();
            string query = "getUserFormPermissions";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@RoleID", RoleID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FormModel objModel = new FormModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.FormID = Convert.ToInt32(dr["FormID"]);
                        objModel.FormName = Convert.ToString(dr["FormName"]);
                        objModel.FormLevel = Convert.ToInt32(dr["FormLevel"]);
                        objModel.ParentID = Convert.ToInt32(dr["ParentID"]);
                        objModel.Status = Convert.ToBoolean(dr["Status"]);
                        objModel.IsFormPermissions = Convert.ToBoolean(dr["IsFormPermission"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

        #region Settings
        public SettingsModel getSettingsID(int? SettingsID)
        {
            SettingsModel objModel = new SettingsModel();
            string query = "getSetting";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SettingsID", SettingsID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        objModel.SettingsID = Convert.ToInt32(dr["SettingsID"]);
                        objModel.IsLoginWithOTP = Convert.ToBoolean(dr["IsLoginwithOTP"]);
                    }
                }
            }
            return objModel;
        }
        public DataTable getSettingsID_dt(int? SettingsID)
        {
            DataTable dt = new DataTable();
            string query = "getSetting";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SettingsID", SettingsID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                }
            }
            return dt;
        }

        public Int64 RegisterSettings(SettingsModel objModel)
        {
            Int32 retVal = 0;
            var olddata = getSettingsID_dt(objModel.SettingsID);
            var editMode = objModel.SettingsID > 0 ? "Edit" : "Add";
            string query = "insupdSettings";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SettingsID", objModel.SettingsID);
                cmd.Parameters.AddWithValue("@IsLoginwithOTP", objModel.IsLoginWithOTP);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt32(dt.Rows[0][0]);
                    #region Log
                    var newData = getSettingsID_dt(retVal);
                    if (newData.Rows.Count > 0)
                    {
                        DataRow newrow = newData.Rows[0];


                        if (editMode == "Add")
                        {
                            foreach (var col in newData.Columns)
                            {

                                string query1 = "insupdLog";
                                SqlCommand cmd1 = new SqlCommand(query1, con);
                                cmd1.Connection = con;
                                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@UserID", objModel.LoggedInUserID);
                                cmd1.Parameters.AddWithValue("@ModuleName", "Settings");
                                cmd1.Parameters.AddWithValue("@TableName", "tbSettings");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.SettingsID);
                                cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                cmd1.Parameters.AddWithValue("@OldValue", "");
                                cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                DataTable dt1 = new DataTable();
                                con.Open();
                                sda1.Fill(dt1);
                                con.Close();

                            }
                        }
                        else
                        {
                            DataRow oldrow = olddata.Rows[0];
                            foreach (var col in newData.Columns)
                            {
                                if (Convert.ToString(newrow[col.ToString()]) != Convert.ToString(oldrow[col.ToString()]))
                                {
                                    string query1 = "insupdLog";
                                    SqlCommand cmd1 = new SqlCommand(query1, con);
                                    cmd1.Connection = con;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@UserID", objModel.LoggedInUserID);
                                    cmd1.Parameters.AddWithValue("@ModuleName", "Settings");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbSettings");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.SettingsID);
                                    cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                    cmd1.Parameters.AddWithValue("@OldValue", Convert.ToString(oldrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    DataTable dt1 = new DataTable();
                                    con.Open();
                                    sda1.Fill(dt1);
                                    con.Close();
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    retVal = -1;
                }
            }
            return retVal;
        }
        #endregion
    }
}