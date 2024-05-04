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
using DocumentFormat.OpenXml.Bibliography;
using FETruckCRM.Common;
namespace FETruckCRM.Data
{
    public class LoadService
    {
        SqlConnection con;
        public LoadService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public static List<SelectListItem> getCountryList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllCountry";
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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["CountryID"]).ToString(), Text = Convert.ToString(dr["CountryName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getRecoveredLoadList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getRecoveredLoadcount";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["LoadID"]).ToString(), Text = Convert.ToString(dr["LoadNo"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getCarrierList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllCarriers";
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
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["CareerID"]).ToString(), Text = Convert.ToString(dr["CareerName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getAllManagerList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllManagers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["Alias"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getTeamLeadList(long ManagerID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetTeamLeadsByManagers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ManagerID", ManagerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["Alias"]) });
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
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

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
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["SiteID"]).ToString(), Text = Convert.ToString(dr["SiteName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getCustomerList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getAllCustomersList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["CustomerID"]).ToString(), Text = Convert.ToString(dr["CustomerName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getDispatcherList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getDispatcherList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getSalesRepList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getSalesRepList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getManagerList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getAllManagers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getLoadStatusList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getLoadStatusList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "All" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["LoadStatusID"]).ToString(), Text = Convert.ToString(dr["Status"]) });
                    }
                }
            }
            return selectList;
        }
        public static List<SelectListItem> getStateList(long CountryID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllStates";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryID", CountryID);

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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["StateID"]).ToString(), Text = Convert.ToString(dr["StateName"]) });
                    }
                }
                else
                {

                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                }
            }
            return selectList;
        }
        public static List<Autocomplete> AutoCompleteCustomer(long userid, string search)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("getCustomerBySearch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@SearchText", search);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new Autocomplete { val = Convert.ToInt64(dr["CustomerID"]).ToString(), label = Convert.ToString(dr["CustomerName"]), IsOnHold = Convert.ToBoolean(dr["IsOnHold"]) });
                    }
                }
            }


            return selectList;
        }
        public static List<Autocomplete> AutoCompleteShipper(long userid, string search)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("getShipperBySearch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@SearchText", search);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new Autocomplete { val = Convert.ToInt64(dr["ShipperID"]).ToString(), label = Convert.ToString(dr["ShipperName"]), Location = Convert.ToString(dr["Location"]) });
                    }
                }
            }


            return selectList;
        }
        public static List<Autocomplete> AutoCompleteConsignee(long userid, string search)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("getConsigneeBySearch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@SearchText", search);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new Autocomplete { val = Convert.ToInt64(dr["ShipperID"]).ToString(), label = Convert.ToString(dr["ShipperName"]), Location = Convert.ToString(dr["Location"]) });
                    }
                }
            }


            return selectList;
        }
        public static List<Autocomplete> AutoCompleteCarrier(long userid, string search)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("getCareerBySearch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@SearchText", search);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new Autocomplete { val = Convert.ToInt64(dr["Careerid"]).ToString(), label = Convert.ToString(dr["careername"]) });
                    }
                }
            }


            return selectList;
        }
        public static List<Autocomplete> AutoCompleteMC(long userid, string search)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("getCareerBySearchMC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@SearchText", search);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new Autocomplete { val = Convert.ToInt64(dr["Careerid"]).ToString(), label = Convert.ToString(dr["MCFFNo"]), CarrierName = Convert.ToString(dr["careername"]).ToString(), IsCarrierBlackListed = Convert.ToBoolean(dr["Blacklisted"]) });
                    }
                }
            }


            return selectList;
        }
        public static DataSet BindDropDowns(long userid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getLoadDropdownlist", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public long CheckStatus(Int64 LoadID, int LoadStatusID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("CheckLoadStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@LoadstatusID", LoadStatusID);
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
        public long ChangeStatus(Int64 LoadID, int LoadStatusID, Int64 LoggedinUserId)
        {
            long isSuccess = 0;
            string oldLoadStatusName = "";
            int OldStatusID = 0;
            string query4 = "getCurrentLoadStatus";
            SqlCommand cmd4 = new SqlCommand(query4, con);
            cmd4.Connection = con;
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            cmd4.Parameters.AddWithValue("@LoadID", LoadStatusID);
            SqlDataAdapter sda4 = new SqlDataAdapter(cmd4);
            DataTable dt4 = new DataTable();
            con.Open();
            sda4.Fill(dt4);
            con.Close();
            if (dt4.Rows.Count > 0)
            {
                OldStatusID = Convert.ToInt32(dt4.Rows[0]["status"]);
            }


            string query3 = "getLoadStatusName";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.Connection = con;
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@LoadstatusID", OldStatusID);
            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            con.Open();
            sda3.Fill(dt3);
            con.Close();
            if (dt3.Rows.Count > 0)
            {
                oldLoadStatusName = Convert.ToString(dt3.Rows[0]["status"]);
            }
            using (SqlCommand cmd = new SqlCommand("ChangeLoadStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@LoadstatusID", LoadStatusID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);

                    if (isSuccess > 0)
                    {
                        string LoadStatusName = "";
                        string query2 = "getLoadStatusName";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        cmd2.Connection = con;
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@LoadstatusID", LoadStatusID);
                        SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        con.Open();
                        sda2.Fill(dt2);
                        con.Close();
                        if (dt2.Rows.Count > 0)
                        {
                            LoadStatusName = Convert.ToString(dt2.Rows[0]["status"]);


                        }


                        string query1 = "insupdLog";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Connection = con;
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@UserID", LoggedinUserId);
                        cmd1.Parameters.AddWithValue("@ModuleName", "Load");
                        cmd1.Parameters.AddWithValue("@TableName", "tbLoad");
                        cmd1.Parameters.AddWithValue("@PrimaryKey", LoadID);
                        cmd1.Parameters.AddWithValue("@ColumnName", "LoadstatusID");
                        cmd1.Parameters.AddWithValue("@OldValue", oldLoadStatusName);
                        cmd1.Parameters.AddWithValue("@NewValue", LoadStatusName);
                        cmd1.Parameters.AddWithValue("@EditMode", "Status Update");
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
        public long ChangeShipperInvoiceSentStatus(Int64 LoadID, bool IsShipperInvoiceSent, string ShipperInvoiceSentDate)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("updShipperInvoiceSentStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@IsShipperInvoiceSent", IsShipperInvoiceSent == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@ShipperInvoiceSentDate", ShipperInvoiceSentDate);
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
        public long ChangeShipperPaymentRecdStatus(Int64 LoadID, bool IsCheckedPaymentRecd, string ShipperPaymentReceivedDate)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("updShipperPaymentReceivedStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@IsShipperPaymentReceived", IsCheckedPaymentRecd);
                cmd.Parameters.AddWithValue("@ShipperPaymentReceivedDate", ShipperPaymentReceivedDate);
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
        public long ChangeCarrierInvoiceRecdStatus(Int64 LoadID, bool IsCheckedPaymentRecd, string CarrierInvoiceReceivedDate)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("updCarrierInvoiceReceivedStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@IsCarrierInvoiceReceived", IsCheckedPaymentRecd);
                cmd.Parameters.AddWithValue("@CarrierInvoiceReceivedDate", CarrierInvoiceReceivedDate);
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
        public long IsCarrierPaymentMade(Int64 LoadID, bool IsCheckedPaymentRecd, string CarrierInvoiceReceivedDate)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("updCarrierPaymentMadeStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
                cmd.Parameters.AddWithValue("@IsCarrierPaymentMade", IsCheckedPaymentRecd);
                cmd.Parameters.AddWithValue("@CarrierInvoiceReceivedDate", CarrierInvoiceReceivedDate);
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
        public static long CheckCreditLimit(Int64 CustomerID, decimal rate)
        {
            long isSuccess = 0;
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("checkCustomerCreditLimit", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Rate", rate);
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
        public static long UpdateLoadCreationDate(Int64 LoadID)
        {
            long isSuccess = 0;
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            using (SqlCommand cmd = new SqlCommand("updateLoadCreationdate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
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
        public static Int64 SaveLoad(LoadModeldata load, List<LoadShipperModeldata> shippers, List<LoadShipperModeldata> consignee, long LoggedUserID, List<LoadOthercharges> otherCharges, List<LoadCarrierOthercharges> CarrierotherCharges)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            long ret = 0;
            load.LoadID = string.IsNullOrEmpty(load.LoadID) ? "0" : load.LoadID;
            var olddata = getload(load.LoadID);
            var editMode = load.LoadID != "0" ? "Edit" : "Add";
            using (SqlCommand cmd = new SqlCommand("insupdLoadnew", con))
            {
                var ship = GetShipper();
                foreach (var item in shippers)
                {
                    var rw = ship.NewRow();
                    rw["LoadShipperID"] = item.LoadShipperID;
                    rw["LoadID"] = item.LoadID;
                    rw["ShipperID"] = item.ShipperID;
                    rw["ShipperName"] = item.shippername;
                    rw["Location"] = item.location;
                    rw["ShipperDate"] = item.date;
                    rw["IsShowShipperTime"] = item.isshowtime;
                    rw["ShipperTime"] = item.time;
                    rw["Description"] = item.description;
                    rw["Type"] = item.type;
                    rw["Quantity"] = item.qty;
                    rw["Weight"] = item.weight;
                    rw["ShipperValues"] = item.value;
                    rw["ShipperNotes"] = item.notes;
                    rw["PONumber"] = item.ponumbers;
                    rw["CustomBrokerID"] = item.custombrokers;
                    rw["CreatedDate"] = DateTime.Now;
                    rw["CreatedByID"] = LoggedUserID;
                    rw["LastModifiedDate"] = DateTime.Now;
                    rw["LastModifiedByID"] = LoggedUserID;
                    rw["IsDeletedInd"] = false;
                    ship.Rows.Add(rw);
                }
                var cons = GetConsignee();
                foreach (var item in consignee)
                {
                    var rw = cons.NewRow();
                    rw["LoadConsigneeID"] = item.LoadConsigneeID;
                    rw["LoadID"] = item.LoadID;
                    rw["ConsigneeID"] = item.ConsigneeID;
                    rw["ConsigneeName"] = item.ConsigneeName;
                    rw["ConsigneeLocation"] = item.location;
                    rw["ConsigneeDate"] = item.date;
                    rw["IsShowTime"] = item.isshowtime;
                    rw["ConsigneeTime"] = item.time;
                    rw["Description"] = item.description;
                    rw["Type"] = item.type;
                    rw["Quantity"] = item.qty;
                    rw["Weight"] = item.weight;
                    rw["Value"] = item.value;
                    rw["DeliveryNotes"] = item.notes;
                    rw["PONumber"] = item.ponumbers;
                    rw["ProMiles"] = item.promiles;
                    rw["ProMilesEmpty"] = item.empty;
                    rw["CreatedDate"] = DateTime.Now;
                    rw["CreatedByID"] = LoggedUserID;
                    rw["LastModifiedDate"] = DateTime.Now;
                    rw["LastModifiedByID"] = LoggedUserID;
                    rw["IsDeletedInd"] = false;
                    cons.Rows.Add(rw);
                }
                var otheCharges = GetOtherCharges();
                if (otherCharges != null)
                {
                    foreach (var item in otherCharges)
                    {
                        var rw = otheCharges.NewRow();
                        rw["LoadID"] = item.LoadID;
                        rw["OtherchargesDescription"] = item.OtherchargesDescription;
                        rw["OtherChargesDate"] = item.OtherChargesDate;
                        rw["OtherChargesType"] = item.OtherChargesType;
                        rw["Amount"] = item.Amount;
                        rw["CreatedDate"] = DateTime.Now.ToString();
                        rw["CreatedByID"] = LoggedUserID;
                        rw["LastModifiedDate"] = DateTime.Now.ToString();
                        rw["LastModifiedByID"] = LoggedUserID;
                        otheCharges.Rows.Add(rw);
                    }

                }
                var CarrierotheCharges = GetCarrierOtherCharges();
                if (CarrierotherCharges != null)
                {
                    foreach (var item in CarrierotherCharges)
                    {
                        var rw = CarrierotheCharges.NewRow();
                        rw["LoadID"] = item.LoadID;
                        rw["Type"] = item.Type;
                        rw["OtherchargesDescription"] = item.OtherchargesDescription;
                        rw["Amount"] = item.Amount;
                        rw["OtherChargesDate"] = item.OtherChargesDate;
                        rw["OtherChargesType"] = item.OtherChargesType;
                        rw["CreatedDate"] = DateTime.Now.ToString();
                        rw["CreatedByID"] = LoggedUserID;
                        rw["LastModifiedDate"] = DateTime.Now.ToString();
                        rw["LastModifiedByID"] = LoggedUserID;
                        CarrierotheCharges.Rows.Add(rw);
                    }

                }
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandTimeout = 3600;
                cmd.Parameters.AddWithValue("@LoadID", load.LoadID);
                cmd.Parameters.AddWithValue("@LoadType", load.LoadType);
                cmd.Parameters.AddWithValue("@LoadNo", load.LoadNo);
                cmd.Parameters.AddWithValue("@CustomerID", load.CustomerID);
                cmd.Parameters.AddWithValue("@BillTo", load.billto);
                cmd.Parameters.AddWithValue("@Dispatcher", load.drpDispatcher);
                cmd.Parameters.AddWithValue("@SaleRep1", load.drpSalesResp);
                cmd.Parameters.AddWithValue("@SaleRep2", load.drpSalesResp);
                cmd.Parameters.AddWithValue("@Status", load.drpStatus);
                cmd.Parameters.AddWithValue("@WO", load.txtWo);
                cmd.Parameters.AddWithValue("@Type", load.txtdrpType);
                cmd.Parameters.AddWithValue("@Rate", load.txtRate);
                cmd.Parameters.AddWithValue("@PDs", load.txtPDS);
                cmd.Parameters.AddWithValue("@FSC", load.txtfscrate);
                cmd.Parameters.AddWithValue("@IsRatePercentage", load.chkrate);
                cmd.Parameters.AddWithValue("@OtherCharges", load.txtothercharges);
                cmd.Parameters.AddWithValue("@RatePercent", load.txtRateNw);
                cmd.Parameters.AddWithValue("@LType", load.drpLoadTypeNew);
                cmd.Parameters.AddWithValue("@CareerID", load.hdntxtCarrier);
                cmd.Parameters.AddWithValue("@DriverID", "");
                cmd.Parameters.AddWithValue("@EquipmenttypeID", load.drpEquipmentType);
                cmd.Parameters.AddWithValue("@CareerFee", load.txtCarrierFee);
                cmd.Parameters.AddWithValue("@CurrencyID", load.drpCurrency);
                cmd.Parameters.AddWithValue("@LoggedUserID", LoggedUserID);
                cmd.Parameters.AddWithValue("@tbLoadConsigneeTypee", cons);
                cmd.Parameters.AddWithValue("@tbLoadShipperType", ship);
                cmd.Parameters.AddWithValue("@LoadOtherChargesType", otheCharges);
                cmd.Parameters.AddWithValue("@LoadCarrierOtherChargesType", CarrierotheCharges);
                cmd.Parameters.AddWithValue("@CerrierPDs", load.CerrierPDs);
                cmd.Parameters.AddWithValue("@CarrierFSC", load.CarrierFSC);
                cmd.Parameters.AddWithValue("@IsCarrierRatePercentage", load.IsCarrierRatePercentage);
                cmd.Parameters.AddWithValue("@FinalCarrierFee", load.FinalCarrierFee);
                cmd.Parameters.AddWithValue("@CarrierOthercharges", load.CarrierOthercharges);
                cmd.Parameters.AddWithValue("@PaymentType", load.paymentType);
                cmd.Parameters.AddWithValue("@AdvancePayment", load.AdvancePayment);
                cmd.Parameters.AddWithValue("@BillingTypeID", load.drpBillingType);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                try
                {
                    con.Open();
                    sda.Fill(dt);
                    // cmd.ExecuteNonQuery();
                    con.Close();
                    if (dt.Tables[0].Rows.Count > 0 && Convert.ToInt64(dt.Tables[0].Rows[0][0]) > 0)
                    {
                        ret = Convert.ToInt64(dt.Tables[0].Rows[0][0]);

                        #region Log
                        var newData = getload(ret.ToString());
                        if (newData.Tables.Count > 0 && newData.Tables[0].Rows.Count > 0)
                        {
                            DataRow newrow = newData.Tables[0].Rows[0];


                            if (editMode == "Add")
                            {
                                foreach (var col in newData.Tables[0].Columns)
                                {

                                    string query1 = "insupdLog";
                                    SqlCommand cmd1 = new SqlCommand(query1, con);
                                    cmd1.Connection = con;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@UserID", LoggedUserID);
                                    cmd1.Parameters.AddWithValue("@ModuleName", "Load");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbLoad");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", load.LoadID);
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
                                DataRow oldrow = olddata.Tables[0].Rows[0];
                                foreach (var col in newData.Tables[0].Columns)
                                {
                                    if (Convert.ToString(newrow[col.ToString()]) != Convert.ToString(oldrow[col.ToString()]))
                                    {
                                        string query1 = "insupdLog";
                                        SqlCommand cmd1 = new SqlCommand(query1, con);
                                        cmd1.Connection = con;
                                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@UserID", LoggedUserID);
                                        cmd1.Parameters.AddWithValue("@ModuleName", "Load");
                                        cmd1.Parameters.AddWithValue("@TableName", "tbLoad");
                                        cmd1.Parameters.AddWithValue("@PrimaryKey", load.LoadID);
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
                        ret = -1;
                    }
                }
                catch (Exception ex)
                {
                    ret = -1;
                    throw;
                }
            }
            return ret;
        }
        public static DataTable GetShipper()
        {
            DataTable tbLoadShipperType = new DataTable();

            tbLoadShipperType.Columns.Add("LoadShipperID");
            tbLoadShipperType.Columns.Add("LoadID");
            tbLoadShipperType.Columns.Add("ShipperID");
            tbLoadShipperType.Columns.Add("ShipperName");

            tbLoadShipperType.Columns.Add("Location");
            tbLoadShipperType.Columns.Add("ShipperDate");
            tbLoadShipperType.Columns.Add("IsShowShipperTime");
            tbLoadShipperType.Columns.Add("ShipperTime");
            tbLoadShipperType.Columns.Add("Description");
            tbLoadShipperType.Columns.Add("Type");
            tbLoadShipperType.Columns.Add("Quantity");
            tbLoadShipperType.Columns.Add("Weight");
            tbLoadShipperType.Columns.Add("ShipperValues");
            tbLoadShipperType.Columns.Add("ShipperNotes");
            tbLoadShipperType.Columns.Add("PONumber");
            tbLoadShipperType.Columns.Add("CustomBrokerID");

            tbLoadShipperType.Columns.Add("CreatedDate");
            tbLoadShipperType.Columns.Add("CreatedByID");
            tbLoadShipperType.Columns.Add("LastModifiedDate");
            tbLoadShipperType.Columns.Add("LastModifiedByID");
            tbLoadShipperType.Columns.Add("IsDeletedInd");

            return tbLoadShipperType;
        }
        public static DataTable GetConsignee()
        {
            DataTable Consignee = new DataTable();

            Consignee.Columns.Add("LoadConsigneeID");
            Consignee.Columns.Add("LoadID");
            Consignee.Columns.Add("ConsigneeID");
            Consignee.Columns.Add("ConsigneeName");
            Consignee.Columns.Add("ConsigneeLocation");
            Consignee.Columns.Add("ConsigneeDate");

            Consignee.Columns.Add("IsShowTime");
            Consignee.Columns.Add("ConsigneeTime");
            Consignee.Columns.Add("Description");
            Consignee.Columns.Add("Type");
            Consignee.Columns.Add("Weight");
            Consignee.Columns.Add("Value");
            Consignee.Columns.Add("Quantity");
            Consignee.Columns.Add("DeliveryNotes");
            Consignee.Columns.Add("PONumber");
            Consignee.Columns.Add("ProMiles");
            Consignee.Columns.Add("ProMilesEmpty");
            Consignee.Columns.Add("CreatedDate");
            Consignee.Columns.Add("CreatedByID");
            Consignee.Columns.Add("LastModifiedDate");
            Consignee.Columns.Add("LastModifiedByID");
            Consignee.Columns.Add("IsDeletedInd");

            return Consignee;
        }
        public static DataTable GetOtherCharges()
        {
            DataTable othCharges = new DataTable();
            othCharges.Columns.Add("LoadID");
            othCharges.Columns.Add("OtherchargesDescription");
            othCharges.Columns.Add("Amount");
            othCharges.Columns.Add("OtherChargesDate");
            othCharges.Columns.Add("OtherChargesType");
            othCharges.Columns.Add("CreatedDate");
            othCharges.Columns.Add("CreatedByID");
            othCharges.Columns.Add("LastModifiedDate");
            othCharges.Columns.Add("LastModifiedByID");
            return othCharges;
        }
        public static DataTable GetCarrierOtherCharges()
        {
            DataTable othCharges = new DataTable();
            othCharges.Columns.Add("LoadID");
            othCharges.Columns.Add("Type");
            othCharges.Columns.Add("OtherchargesDescription");
            othCharges.Columns.Add("Amount");
            othCharges.Columns.Add("OtherChargesDate");
            othCharges.Columns.Add("OtherChargesType");
            othCharges.Columns.Add("CreatedDate");
            othCharges.Columns.Add("CreatedByID");
            othCharges.Columns.Add("LastModifiedDate");
            othCharges.Columns.Add("LastModifiedByID");
            return othCharges;
        }
        public static DataSet getload(string loadid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getLoadData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                con.Open();
                sda.Fill(dt);
                con.Close();

            }

            return dt;

        }
        public static DataSet getloaddataForPDF(string loadid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getrateconfirmationPDFData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                con.Open();
                sda.Fill(dt);
                con.Close();

            }

            return dt;

        }
        public static RateConModel getRateConloaddataForPDF(string loadid)
        {
            RateConModel obj = new RateConModel();
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getrateconfirmationPDFData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var dr = dt.Tables[0].Rows[0];
                        LoadModel objModel = new LoadModel();
                        objModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                        objModel.LoadType = Convert.ToInt32(dr["LoadType"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        objModel.BillTo = Convert.ToInt64(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.Alias = Convert.ToString(dr["Alias"]);
                        objModel.CareerTelephone = Convert.ToString(dr["CareerTelephone"]);
                        objModel.CareerFax = Convert.ToString(dr["CareerFax"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        objModel.FinalCarrierFee = Convert.ToString(dr["FinalCarrierFee"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.SaleRep1 = dr["SaleRep1"] != DBNull.Value ? Convert.ToInt64(dr["SaleRep1"]) : 0;
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        objModel.Type = Convert.ToString(dr["Type"]);
                        objModel.Rate = dr["Rate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Rate"]);
                        objModel.PDs = dr["PDs"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDs"]);
                        objModel.FSC = dr["FSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSC"]);
                        objModel.IsRatePercentage = Convert.ToBoolean(dr["IsRatePercentage"]);
                        objModel.OtherCharges = Convert.ToString(dr["CarrierOtherCharges"]);
                        objModel.RatePercent = Convert.ToDecimal(dr["RatePercent"]);
                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.DriverID = Convert.ToInt64(dr["DriverID"]);
                        objModel.EquipmenttypeID = Convert.ToInt64(dr["EquipmenttypeID"]);
                        objModel.CareerFee = dr["CareerFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CareerFee"]);
                        objModel.AdvancePayment = dr["AdvancePayment"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["AdvancePayment"]);
                        objModel.CarrierFSC = dr["CarrierFSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CarrierFSC"]);
                        objModel.Currency = Convert.ToString(dr["Currency"]);
                        objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        objModel.Dispatcher = Convert.ToString(dr["Dispatcher"]);
                        objModel.Dispatcher1 = Convert.ToString(dr["Dispatcher1"]);
                        objModel.LoadStatus = Convert.ToString(dr["LoadStatus"]);
                        //objModel.PaymentTerm = Convert.ToString(dr["PaymentTerms"]);
                        obj.Loaddata = objModel;

                    }
                    if (dt.Tables.Count > 1)
                    {

                        List<LoadShipperModel> LSModel = new List<LoadShipperModel>();
                        if (dt.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[1].Rows)
                            {
                                LoadShipperModel objlsModel = new LoadShipperModel();
                                objlsModel.LoadShipperID = Convert.ToInt64(dr["LoadShipperID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                                objlsModel.Location = Convert.ToString(dr["Location"]);
                                objlsModel.Contact = Convert.ToString(dr["Telephone"]);
                                objlsModel.strShipperDate = dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["ShipperDate"]).ToShortDateString();
                                objlsModel.ShipperTime = Convert.ToString(dr["ShipperTime"]);
                                objlsModel.IsShowShipperTime = Convert.ToBoolean(dr["IsShowShipperTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.ShipperValues = Convert.ToString(dr["ShipperValues"]);
                                objlsModel.ShipperNotes = Convert.ToString(dr["ShipperNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                            obj.LoadShipperdata = LSModel;
                        }
                    }

                    if (dt.Tables.Count > 2)
                    {

                        List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                        if (dt.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[2].Rows)
                            {
                                LoadConsigneeModel objlsModel = new LoadConsigneeModel();
                                objlsModel.LoadConsigneeID = Convert.ToInt64(dr["LoadConsigneeID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ConsigneeID = Convert.ToInt64(dr["ConsigneeID"]);
                                objlsModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                                objlsModel.Contact = Convert.ToString(dr["Telephone"]);
                                objlsModel.strConsigneeDate = dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["ConsigneeDate"]).ToShortDateString();
                                objlsModel.ConsigneeTime = Convert.ToString(dr["ConsigneeTime"]);
                                objlsModel.IsShowTime = Convert.ToBoolean(dr["IsShowTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]); //Convert.ToInt64(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.Value = Convert.ToString(dr["Value"]);
                                objlsModel.DeliveryNotes = Convert.ToString(dr["DeliveryNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                        }
                        obj.LoadConsigneedata = LSModel;
                    }
                }

                if (dt.Tables.Count > 3)
                {

                    List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                    if (dt.Tables[3].Rows.Count > 0)
                    {
                        DataRow dr = dt.Tables[3].Rows[0];
                        PreferencesModel objlsModel = new PreferencesModel();
                        objlsModel.StandardLoadSheetNotes = Convert.ToString(dr["StandardLoadSheetNotes"]);



                        obj.LoadPreferencedata = objlsModel;
                    }
                }
                if (dt.Tables.Count > 4)
                {


                    if (dt.Tables[4].Rows.Count > 0)
                    {
                        if (dt.Tables[4].Rows.Count > 0)
                        {
                            DataRow dr = dt.Tables[4].Rows[0];
                            LoadAdditionalNotes objlsModel = new LoadAdditionalNotes();
                            objlsModel.LoadAdditionalNotesID = Convert.ToString(dr["LoadAdditionalNotesID"]);
                            objlsModel.LoadID = Convert.ToString(dr["LoadID"]);
                            objlsModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                            objlsModel.DriverPayNotes = Convert.ToString(dr["DriverPayNotes"]);
                            objlsModel.DPNAppearOnReport = Convert.ToString(dr["DPNAppearOnReport"]);
                            objlsModel.InvoiceNotes = Convert.ToString(dr["InvoiceNotes"]);
                            objlsModel.INAppearOnInvoice = Convert.ToString(dr["INAppearOnInvoice"]);
                            objlsModel.InvoiceDescription = Convert.ToString(dr["InvoiceDescription"]);
                            objlsModel.DeletedRefusalNotes = Convert.ToString(dr["DeletedRefusalNotes"]);
                            objlsModel.RecInvoiceNo = Convert.ToString(dr["RecInvoiceNo"]);
                            objlsModel.RecInvoiceDate = Convert.ToString(dr["RecInvoiceDate"]);
                            objlsModel.RecAmount = Convert.ToString(dr["RecAmount"]);
                            objlsModel.RecCustomer = Convert.ToString(dr["RecCustomer"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.RecContact = Convert.ToString(dr["RecContact"]);
                            objlsModel.RecTelephone = Convert.ToString(dr["RecTelephone"]);
                            objlsModel.RecTelephoneExt = Convert.ToString(dr["RecTelephoneExt"]);
                            objlsModel.RecTollFree = Convert.ToString(dr["RecTollFree"]);
                            objlsModel.RecFax = Convert.ToString(dr["RecFax"]);
                            objlsModel.RecNotes = Convert.ToString(dr["RecNotes"]);
                            objlsModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                            objlsModel.ConDispatcherName = Convert.ToString(dr["ConDispatcherName"]);
                            objlsModel.ConDispatcherPhone = Convert.ToString(dr["ConDispatcherPhone"]);
                            objlsModel.conDispatcherEmail = Convert.ToString(dr["conDispatcherEmail"]);
                            objlsModel.conDriverName = Convert.ToString(dr["conDriverName"]);
                            objlsModel.ConDriverPhone = Convert.ToString(dr["ConDriverPhone"]);
                            objlsModel.conDriverEmail = Convert.ToString(dr["conDriverEmail"]);
                            objlsModel.ConTruck = Convert.ToString(dr["ConTruck"]);
                            objlsModel.ConTrailer = Convert.ToString(dr["ConTrailer"]);
                            objlsModel.ConNotes = Convert.ToString(dr["ConNotes"]);
                            objlsModel.CreatedByID = Convert.ToString(dr["CreatedByID"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.LastModifiedByID = Convert.ToString(dr["LastModifiedByID"]);
                            objlsModel.LastModifiedDate = Convert.ToString(dr["LastModifiedDate"]);
                            obj.LoadAddiNotes = objlsModel;
                        }
                    }
                }
            }
            return obj;
        }
        public static RateConModel getShipperRateConloaddataForPDF(string loadid)
        {
            RateConModel obj = new RateConModel();
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getshipperrateconfirmationPDFData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var dr = dt.Tables[0].Rows[0];
                        LoadModel objModel = new LoadModel();
                        objModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                        objModel.LoadType = Convert.ToInt32(dr["LoadType"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        objModel.BillTo = Convert.ToInt64(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.Alias = Convert.ToString(dr["Alias"]);
                        objModel.CareerTelephone = Convert.ToString(dr["CareerTelephone"]);
                        objModel.CareerFax = Convert.ToString(dr["CareerFax"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        objModel.FinalCarrierFee = Convert.ToString(dr["FinalCarrierFee"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.SaleRep1 = dr["SaleRep1"] != DBNull.Value ? Convert.ToInt64(dr["SaleRep1"]) : 0;
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        objModel.Type = Convert.ToString(dr["Type"]);
                        objModel.Rate = dr["Rate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Rate"]);
                        objModel.PDs = dr["PDs"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDs"]);// Convert.ToString(dr["PDs"]);
                        objModel.FSC = dr["FSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSC"]);
                        objModel.IsRatePercentage = Convert.ToBoolean(dr["IsRatePercentage"]);
                        objModel.OtherCharges = Convert.ToString(dr["CarrierOtherCharges"]);
                        objModel.RatePercent = Convert.ToDecimal(dr["RatePercent"]);
                        objModel.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);
                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.DriverID = Convert.ToInt64(dr["DriverID"]);
                        objModel.EquipmenttypeID = Convert.ToInt64(dr["EquipmenttypeID"]);
                        objModel.CareerFee = dr["CareerFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CareerFee"]);
                        objModel.CarrierFSC = dr["CarrierFSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CarrierFSC"]);
                        objModel.Currency = Convert.ToString(dr["Currency"]);
                        objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.BillingFax = Convert.ToString(dr["BillingFax"]);
                        objModel.Dispatcher = Convert.ToString(dr["Dispatcher"]);
                        objModel.Dispatcher1 = Convert.ToString(dr["Dispatcher1"]);
                        objModel.LoadStatus = Convert.ToString(dr["LoadStatus"]);
                        objModel.PaymentTerm = Convert.ToString(dr["PaymentTerms"]);
                        obj.Loaddata = objModel;

                    }
                    if (dt.Tables.Count > 1)
                    {

                        List<LoadShipperModel> LSModel = new List<LoadShipperModel>();
                        if (dt.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[1].Rows)
                            {
                                LoadShipperModel objlsModel = new LoadShipperModel();
                                objlsModel.LoadShipperID = Convert.ToInt64(dr["LoadShipperID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                                objlsModel.Location = Convert.ToString(dr["Location"]);
                                objlsModel.Contact = Convert.ToString(dr["Telephone"]);
                                objlsModel.strShipperDate = dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["ShipperDate"]).ToShortDateString();
                                objlsModel.ShipperTime = Convert.ToString(dr["ShipperTime"]);
                                objlsModel.IsShowShipperTime = Convert.ToBoolean(dr["IsShowShipperTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.ShipperValues = Convert.ToString(dr["ShipperValues"]);
                                objlsModel.ShipperNotes = Convert.ToString(dr["ShipperNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                            obj.LoadShipperdata = LSModel;
                        }
                    }

                    if (dt.Tables.Count > 2)
                    {

                        List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                        if (dt.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[2].Rows)
                            {
                                LoadConsigneeModel objlsModel = new LoadConsigneeModel();
                                objlsModel.LoadConsigneeID = Convert.ToInt64(dr["LoadConsigneeID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ConsigneeID = Convert.ToInt64(dr["ConsigneeID"]);
                                objlsModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                                objlsModel.Contact = Convert.ToString(dr["Telephone"]);
                                objlsModel.strConsigneeDate = dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["ConsigneeDate"]).ToShortDateString();
                                objlsModel.ConsigneeTime = Convert.ToString(dr["ConsigneeTime"]);
                                objlsModel.IsShowTime = Convert.ToBoolean(dr["IsShowTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]); //Convert.ToInt64(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.Value = Convert.ToString(dr["Value"]);
                                objlsModel.DeliveryNotes = Convert.ToString(dr["DeliveryNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                        }
                        obj.LoadConsigneedata = LSModel;
                    }
                }

                if (dt.Tables.Count > 3)
                {

                    List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                    if (dt.Tables[3].Rows.Count > 0)
                    {
                        DataRow dr = dt.Tables[3].Rows[0];
                        PreferencesModel objlsModel = new PreferencesModel();
                        objlsModel.StandardLoadSheetNotes = Convert.ToString(dr["StandardLoadSheetNotes"]);



                        obj.LoadPreferencedata = objlsModel;
                    }
                }
                if (dt.Tables.Count > 4)
                {


                    if (dt.Tables[4].Rows.Count > 0)
                    {
                        if (dt.Tables[4].Rows.Count > 0)
                        {
                            DataRow dr = dt.Tables[4].Rows[0];
                            LoadAdditionalNotes objlsModel = new LoadAdditionalNotes();
                            objlsModel.LoadAdditionalNotesID = Convert.ToString(dr["LoadAdditionalNotesID"]);
                            objlsModel.LoadID = Convert.ToString(dr["LoadID"]);
                            objlsModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                            objlsModel.DriverPayNotes = Convert.ToString(dr["DriverPayNotes"]);
                            objlsModel.DPNAppearOnReport = Convert.ToString(dr["DPNAppearOnReport"]);
                            objlsModel.InvoiceNotes = Convert.ToString(dr["InvoiceNotes"]);
                            objlsModel.INAppearOnInvoice = Convert.ToString(dr["INAppearOnInvoice"]);
                            objlsModel.InvoiceDescription = Convert.ToString(dr["InvoiceDescription"]);
                            objlsModel.DeletedRefusalNotes = Convert.ToString(dr["DeletedRefusalNotes"]);
                            objlsModel.RecInvoiceNo = Convert.ToString(dr["RecInvoiceNo"]);
                            objlsModel.RecInvoiceDate = Convert.ToString(dr["RecInvoiceDate"]);
                            objlsModel.RecAmount = Convert.ToString(dr["RecAmount"]);
                            objlsModel.RecCustomer = Convert.ToString(dr["RecCustomer"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.RecContact = Convert.ToString(dr["RecContact"]);
                            objlsModel.RecTelephone = Convert.ToString(dr["RecTelephone"]);
                            objlsModel.RecTelephoneExt = Convert.ToString(dr["RecTelephoneExt"]);
                            objlsModel.RecTollFree = Convert.ToString(dr["RecTollFree"]);
                            objlsModel.RecFax = Convert.ToString(dr["RecFax"]);
                            objlsModel.RecNotes = Convert.ToString(dr["RecNotes"]);
                            objlsModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                            objlsModel.ConDispatcherName = Convert.ToString(dr["ConDispatcherName"]);
                            objlsModel.ConDispatcherPhone = Convert.ToString(dr["ConDispatcherPhone"]);
                            objlsModel.conDispatcherEmail = Convert.ToString(dr["conDispatcherEmail"]);
                            objlsModel.conDriverName = Convert.ToString(dr["conDriverName"]);
                            objlsModel.ConDriverPhone = Convert.ToString(dr["ConDriverPhone"]);
                            objlsModel.conDriverEmail = Convert.ToString(dr["conDriverEmail"]);
                            objlsModel.ConTruck = Convert.ToString(dr["ConTruck"]);
                            objlsModel.ConTrailer = Convert.ToString(dr["ConTrailer"]);
                            objlsModel.ConNotes = Convert.ToString(dr["ConNotes"]);
                            objlsModel.CreatedByID = Convert.ToString(dr["CreatedByID"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.LastModifiedByID = Convert.ToString(dr["LastModifiedByID"]);
                            objlsModel.LastModifiedDate = Convert.ToString(dr["LastModifiedDate"]);

                            obj.LoadAddiNotes = objlsModel;
                        }
                    }

                }


            }

            return obj;

        }
        public static RateConModel getloaddataForInvoicePDFNew(string loadid)
        {
            RateConModel obj = new RateConModel();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getInvoicePDFData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var dr = dt.Tables[0].Rows[0];
                        LoadModel objModel = new LoadModel();
                        objModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                        objModel.LoadType = Convert.ToInt32(dr["LoadType"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        objModel.BillTo = Convert.ToInt64(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        //objModel.Alias = Convert.ToString(dr["Alias"]);
                        objModel.CareerTelephone = Convert.ToString(dr["CareerTelephone"]);
                        objModel.CareerFax = Convert.ToString(dr["CareerFax"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        //objModel.FinalCarrierFee = Convert.ToString(dr["FinalCarrierFee"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.SaleRep1 = dr["SaleRep1"] != DBNull.Value ? Convert.ToInt64(dr["SaleRep1"]) : 0;
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        objModel.Type = Convert.ToString(dr["Type"]);
                        objModel.Rate = dr["Rate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Rate"]);
                        objModel.PDs = dr["PDs"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDs"]);// Convert.ToString(dr["PDs"]);
                        objModel.FSC = dr["FSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSC"]);
                        objModel.IsRatePercentage = Convert.ToBoolean(dr["IsRatePercentage"]);
                        //objModel.OtherCharges = Convert.ToString(dr["CarrierOtherCharges"]);
                        objModel.RatePercent = Convert.ToDecimal(dr["RatePercent"]);
                        objModel.AdvancePayment = dr["AdvancePayment"]== DBNull.Value ? 0 : Convert.ToDecimal(dr["AdvancePayment"]);
                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.DriverID = Convert.ToInt64(dr["DriverID"]);
                        objModel.EquipmenttypeID = Convert.ToInt64(dr["EquipmenttypeID"]);
                        objModel.CareerFee = dr["CareerFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CareerFee"]);
                        // objModel.CarrierFSC = dr["CarrierFSC"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CarrierFSC"]);
                        //objModel.Currency = Convert.ToString(dr["Currency"]);
                        //objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        //objModel.Dispatcher = Convert.ToString(dr["Dispatcher"]);
                        // objModel.Dispatcher1 = Convert.ToString(dr["Dispatcher1"]);
                        // objModel.LoadStatus = Convert.ToString(dr["LoadStatus"]);
                        objModel.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        objModel.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
                        objModel.CurrencyID = Convert.ToString(dr["CurrencyID"]);
                        objModel.PaymentTerm = Convert.ToString(dr["PaymentTerm"]);
                        objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        objModel.OtherCharges = Convert.ToString(dr["OtherCharges"]);
                        obj.Loaddata = objModel;
                    }
                    if (dt.Tables.Count > 1)
                    {
                        List<LoadShipperModel> LSModel = new List<LoadShipperModel>();
                        if (dt.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[1].Rows)
                            {
                                LoadShipperModel objlsModel = new LoadShipperModel();
                                objlsModel.LoadShipperID = Convert.ToInt64(dr["LoadShipperID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                                objlsModel.Location = Convert.ToString(dr["Location"]);
                                objlsModel.ShipperDate = Convert.ToDateTime(dr["ShipperDate"]);
                                objlsModel.ShipperTime = Convert.ToString(dr["ShipperTime"]);
                                objlsModel.IsShowShipperTime = Convert.ToBoolean(dr["IsShowShipperTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.ShipperValues = Convert.ToString(dr["ShipperValues"]);
                                objlsModel.ShipperNotes = Convert.ToString(dr["ShipperNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                            obj.LoadShipperdata = LSModel;
                        }
                    }

                    if (dt.Tables.Count > 2)
                    {

                        List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                        if (dt.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Tables[2].Rows)
                            {
                                LoadConsigneeModel objlsModel = new LoadConsigneeModel();
                                objlsModel.LoadConsigneeID = Convert.ToInt64(dr["LoadConsigneeID"]);
                                objlsModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                                objlsModel.ConsigneeID = Convert.ToInt64(dr["ConsigneeID"]);
                                objlsModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                                objlsModel.ConsigneeDate = Convert.ToDateTime(dr["ConsigneeDate"]);
                                objlsModel.ConsigneeTime = Convert.ToString(dr["ConsigneeTime"]);
                                objlsModel.IsShowTime = Convert.ToBoolean(dr["IsShowTime"]);
                                objlsModel.Description = Convert.ToString(dr["Description"]);
                                objlsModel.Type = Convert.ToString(dr["Type"]);
                                objlsModel.Quantity = Convert.ToString(dr["Quantity"]); //Convert.ToInt64(dr["Quantity"]);
                                objlsModel.Weight = Convert.ToString(dr["Weight"]);
                                objlsModel.Value = Convert.ToString(dr["Value"]);
                                objlsModel.DeliveryNotes = Convert.ToString(dr["DeliveryNotes"]);
                                objlsModel.PONumber = Convert.ToString(dr["PONumber"]);
                                objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                objlsModel.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                                objlsModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                                objlsModel.MajorInspectionDirection = Convert.ToString(dr["MajorInspectionDirections"]);
                                objlsModel.Appointments = Convert.ToString(dr["Appointments"]);
                                LSModel.Add(objlsModel);
                            }
                        }
                        obj.LoadConsigneedata = LSModel;
                    }



                }

                if (dt.Tables.Count > 3)
                {

                    List<LoadConsigneeModel> LSModel = new List<LoadConsigneeModel>();
                    if (dt.Tables[3].Rows.Count > 0)
                    {
                        DataRow dr = dt.Tables[3].Rows[0];
                        PreferencesModel objlsModel = new PreferencesModel();
                        objlsModel.StandardInvoiceNotes = Convert.ToString(dr["StandardInvoiceNotes"]);



                        obj.LoadPreferencedata = objlsModel;
                    }
                }
                if (dt.Tables.Count > 4)
                {


                    if (dt.Tables[4].Rows.Count > 0)
                    {
                        if (dt.Tables[4].Rows.Count > 0)
                        {
                            DataRow dr = dt.Tables[4].Rows[0];
                            LoadAdditionalNotes objlsModel = new LoadAdditionalNotes();
                            objlsModel.LoadAdditionalNotesID = Convert.ToString(dr["LoadAdditionalNotesID"]);
                            objlsModel.LoadID = Convert.ToString(dr["LoadID"]);
                            objlsModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                            objlsModel.DriverPayNotes = Convert.ToString(dr["DriverPayNotes"]);
                            objlsModel.DPNAppearOnReport = Convert.ToString(dr["DPNAppearOnReport"]);
                            objlsModel.InvoiceNotes = Convert.ToString(dr["InvoiceNotes"]);
                            objlsModel.INAppearOnInvoice = Convert.ToString(dr["INAppearOnInvoice"]);
                            objlsModel.InvoiceDescription = Convert.ToString(dr["InvoiceDescription"]);
                            objlsModel.DeletedRefusalNotes = Convert.ToString(dr["DeletedRefusalNotes"]);
                            objlsModel.RecInvoiceNo = Convert.ToString(dr["RecInvoiceNo"]);
                            objlsModel.RecInvoiceDate = Convert.ToString(dr["RecInvoiceDate"]);
                            objlsModel.RecAmount = Convert.ToString(dr["RecAmount"]);
                            objlsModel.RecCustomer = Convert.ToString(dr["RecCustomer"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.RecContact = Convert.ToString(dr["RecContact"]);
                            objlsModel.RecTelephone = Convert.ToString(dr["RecTelephone"]);
                            objlsModel.RecTelephoneExt = Convert.ToString(dr["RecTelephoneExt"]);
                            objlsModel.RecTollFree = Convert.ToString(dr["RecTollFree"]);
                            objlsModel.RecFax = Convert.ToString(dr["RecFax"]);
                            objlsModel.RecNotes = Convert.ToString(dr["RecNotes"]);
                            objlsModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                            objlsModel.ConDispatcherName = Convert.ToString(dr["ConDispatcherName"]);
                            objlsModel.ConDispatcherPhone = Convert.ToString(dr["ConDispatcherPhone"]);
                            objlsModel.conDispatcherEmail = Convert.ToString(dr["conDispatcherEmail"]);
                            objlsModel.conDriverName = Convert.ToString(dr["conDriverName"]);
                            objlsModel.ConDriverPhone = Convert.ToString(dr["ConDriverPhone"]);
                            objlsModel.conDriverEmail = Convert.ToString(dr["conDriverEmail"]);
                            objlsModel.ConTruck = Convert.ToString(dr["ConTruck"]);
                            objlsModel.ConTrailer = Convert.ToString(dr["ConTrailer"]);
                            objlsModel.ConNotes = Convert.ToString(dr["ConNotes"]);
                            objlsModel.CreatedByID = Convert.ToString(dr["CreatedByID"]);
                            objlsModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                            objlsModel.LastModifiedByID = Convert.ToString(dr["LastModifiedByID"]);
                            objlsModel.LastModifiedDate = Convert.ToString(dr["LastModifiedDate"]);

                            obj.LoadAddiNotes = objlsModel;
                        }
                    }

                }

                if (dt.Tables.Count > 5)
                {

                    List<LoadOthercharges> LSModel = new List<LoadOthercharges>();
                    if (dt.Tables[5].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Tables[5].Rows)
                        {
                            LoadOthercharges objlsModel = new LoadOthercharges();
                            objlsModel.LoadOtherChargesID = Convert.ToString(dr["LoadOtherChargesID"]);
                            objlsModel.LoadID = Convert.ToString(dr["LoadID"]);
                            objlsModel.OtherchargesDescription = Convert.ToString(dr["OtherchargesDescription"]);
                            objlsModel.Amount = Convert.ToDecimal(dr["Amount"]).ToString("C");
                            objlsModel.OtherChargesDate = Convert.ToString(dr["OtherChargesDate"]);
                            objlsModel.OtherChargesType = Convert.ToString(dr["OtherChargesType"]);
                            objlsModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToShortDateString();
                            objlsModel.CreatedByID = Convert.ToString(dr["CreatedByID"]);
                            objlsModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]).ToShortDateString();
                            objlsModel.LastModifiedByID = Convert.ToString(dr["LastModifiedByID"]);
                            LSModel.Add(objlsModel);
                        }
                    }
                    obj.LoadOthCharges = LSModel;
                }
            }

            return obj;

        }
        public static DataSet getloaddataForInvoicePDF(string loadid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getInvoicePDFData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loadid", loadid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public static DataTable getloaddataForExcel(string userid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("getAllLoadRawReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["PaymentType"] = Convert.ToInt32(dr["PaymentType"]) == 0 ? "Not Set" : (Enum.GetName(typeof(Common.PaymentTypeEnum), Convert.ToInt32(dr["PaymentType"])));
                }
            }
            return dt;
        }
        public static DataTable getManagerloaddataForExcel(string userid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("getAllLoadRawReportByManagers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["PaymentType"] = Convert.ToInt32(dr["PaymentType"]) == 0 ? "Not Set" : (Enum.GetName(typeof(Common.PaymentTypeEnum), Convert.ToInt32(dr["PaymentType"])));
                }
            }
            return dt;
        }
        public static DataTable getloaddataForExcelWithFilters(string userid, string MID, string TID, string EID, string SID)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("getAllLoadRawReportByFilters", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@ManagerID", MID);
                cmd.Parameters.AddWithValue("@TeamLeadID", TID);
                cmd.Parameters.AddWithValue("@EmployeeID", EID);
                cmd.Parameters.AddWithValue("@SiteID", SID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["PaymentType"] = Convert.ToInt32(dr["PaymentType"]) == 0 ? "Not Set" : (Enum.GetName(typeof(Common.PaymentTypeEnum), Convert.ToInt32(dr["PaymentType"])));
                }

            }

            return dt;

        }
        public static DataTable getAccRecedataForExcel(string userid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("getAccountsRecievableReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
            }

            return dt;

        }
        public static DataTable getAccPayedataForExcel(string userid)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("getAccountsPayableReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
            }

            return dt;
        }
        public List<UserDashboardModel> getAllLoads(long UserID)
        {
            List<UserDashboardModel> objList = new List<UserDashboardModel>();
            string query = "getAllLoad";
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
                        UserDashboardModel objModel = new UserDashboardModel();
                        objModel.LoadID = Convert.ToString(dr["LoadID"]);
                        objModel.LoadType = Convert.ToString(dr["LoadType"]);
                        objModel.Role = Convert.ToString(dr["Role"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        objModel.BillTo = Convert.ToString(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.strShipperDate = dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        objModel.strLoadDate = dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        objModel.strDeliveredDate = dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.Location = Convert.ToString(dr["Location"]);
                        objModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                        objModel.Status = Convert.ToString(dr["Status"]);
                        //objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.strRate = Convert.ToDecimal(dr["Rate"]).ToString("C").Replace(".00", "");
                        objModel.strCareerFee = Convert.ToDecimal(dr["CareerFee"]).ToString("C").Replace(".00", "");
                        //objModel.strMargin = Convert.ToDecimal(dr["Margin"]).ToString("C").Replace(".00", "");
                        //objModel.strMarginPercent = Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        objModel.LoadTypeName = Convert.ToString(dr["LoadtypeName"]);
                        objModel.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
                        objModel.ShipperInvoiceSentDate = Convert.ToString(dr["ShipperInvoiceSentDate"]);
                        objModel.ShipperPaymentReceivedDate = Convert.ToString(dr["ShipperPaymentReceivedDate"]);
                        objModel.CarrierInvoiceReceivedDate = Convert.ToString(dr["CarrierInvoiceReceivedDate"]);
                        objModel.CarrierPaymentMadeDate = Convert.ToString(dr["CarrierPaymentMadeDate"]);
                        objModel.IsShipperPaymentReceived = Convert.ToBoolean(dr["IsShipperPaymentReceived"]);
                        objModel.IsCarrierInvoiceReceived = Convert.ToBoolean(dr["IsCarrierInvoiceReceived"]);
                        objModel.IsCarrierPaymentMade = Convert.ToBoolean(dr["IsCarrierPaymentMade"]);
                        objModel.IsShipperInvoiceSent = Convert.ToBoolean(dr["IsShipperInvoiceSent"]);

                        //objModel.LoadID = 1;// Convert.ToInt64(dr["LoadID"]);
                        //objModel.LoadType = 1;//  Convert.ToInt32(dr["LoadType"]);
                        //objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        //objModel.BillTo = 1;// Convert.ToInt64(dr["BillTo"]);
                        //objModel.CareerName = "dfds";// Convert.ToString(dr["CareerName"]);
                        //objModel.CreatedDate =  Convert.ToDateTime(dr["CreatedDate"]);
                        //objModel.strShipperDate = "dfds";//dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        //objModel.strLoadDate = "dfds";// dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        //objModel.strDeliveredDate = "dfds";//dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        //objModel.CustomerName = "dfds";// Convert.ToString(dr["CustomerName"]);
                        //objModel.Location = "dfds";//Convert.ToString(dr["Location"]);
                        //objModel.ConsigneeLocation = "dfds";// Convert.ToString(dr["ConsigneeLocation"]);
                        //objModel.Status = 1;// Convert.ToInt32(dr["Status"]);
                        //objModel.AddedByUser = "dfds";//Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = "dfds";// Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = "dfds";//Convert.ToString(dr["TeamManager"]);
                        //objModel.strRate = "dfds";//Convert.ToDecimal(dr["Rate"]).ToString("C");
                        //objModel.strCareerFee = "dfds";//Convert.ToDecimal(dr["CareerFee"]).ToString("C");
                        //objModel.strMargin = "dfds";//Convert.ToDecimal(dr["Margin"]).ToString("C");
                        //objModel.strMarginPercent = "dfds";// Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        //objModel.LoadTypeName = "dfds";// Convert.ToString(dr["LoadtypeName"]);




                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<UserDashboardModel> getAllLoadsByPaging(long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<UserDashboardModel> objList = new List<UserDashboardModel>();
            string query = "GetAllLoadsByPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserDashboardModel objModel = new UserDashboardModel();
                        objModel.LoadID = Convert.ToString(dr["LoadID"]);
                        objModel.LoadType = Convert.ToString(dr["LoadType"]);
                        objModel.Role = Convert.ToString(dr["Role"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        // objModel.BillTo = Convert.ToString(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.strShipperDate = dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        // objModel.strLoadDate = dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        objModel.strDeliveredDate = dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.Location = Convert.ToString(dr["Location"]);
                        objModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                        objModel.Status = Convert.ToString(dr["Status"]);
                        //objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.strRate = Convert.ToDecimal(dr["Rate"]).ToString("C").Replace(".00", "");
                        objModel.strCareerFee = Convert.ToDecimal(dr["CareerFee"]).ToString("C").Replace(".00", "");
                        //objModel.strMargin = Convert.ToDecimal(dr["Margin"]).ToString("C").Replace(".00", "");
                        //objModel.strMarginPercent = Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        //      objModel.LoadTypeName = Convert.ToString(dr["LoadtypeName"]);
                        objModel.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
                        objModel.ShipperInvoiceSentDate = Convert.ToString(dr["ShipperInvoiceSentDate"]);
                        objModel.ShipperPaymentReceivedDate = Convert.ToString(dr["ShipperPaymentReceivedDate"]);
                        objModel.CarrierInvoiceReceivedDate = Convert.ToString(dr["CarrierInvoiceReceivedDate"]);
                        objModel.CarrierPaymentMadeDate = Convert.ToString(dr["CarrierPaymentMadeDate"]);
                        objModel.IsShipperPaymentReceived = Convert.ToBoolean(dr["IsShipperPaymentReceived"]);
                        objModel.IsCarrierInvoiceReceived = Convert.ToBoolean(dr["IsCarrierInvoiceReceived"]);
                        objModel.IsCarrierPaymentMade = Convert.ToBoolean(dr["IsCarrierPaymentMade"]);
                        objModel.IsShipperInvoiceSent = Convert.ToBoolean(dr["IsShipperInvoiceSent"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        int paymentType = Convert.ToInt32(dr["PaymentType"]);
                        objModel.PaymentType = paymentType == 0 ? "Not Set" : Enum.GetName(typeof(PaymentTypeEnum), paymentType);
                        //objModel.LoadID = 1;// Convert.ToInt64(dr["LoadID"]);
                        //objModel.LoadType = 1;//  Convert.ToInt32(dr["LoadType"]);
                        //objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        //objModel.BillTo = 1;// Convert.ToInt64(dr["BillTo"]);
                        //objModel.CareerName = "dfds";// Convert.ToString(dr["CareerName"]);
                        //objModel.CreatedDate =  Convert.ToDateTime(dr["CreatedDate"]);
                        //objModel.strShipperDate = "dfds";//dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        //objModel.strLoadDate = "dfds";// dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        //objModel.strDeliveredDate = "dfds";//dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        //objModel.CustomerName = "dfds";// Convert.ToString(dr["CustomerName"]);
                        //objModel.Location = "dfds";//Convert.ToString(dr["Location"]);
                        //objModel.ConsigneeLocation = "dfds";// Convert.ToString(dr["ConsigneeLocation"]);
                        //objModel.Status = 1;// Convert.ToInt32(dr["Status"]);
                        //objModel.AddedByUser = "dfds";//Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = "dfds";// Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = "dfds";//Convert.ToString(dr["TeamManager"]);
                        //objModel.strRate = "dfds";//Convert.ToDecimal(dr["Rate"]).ToString("C");
                        //objModel.strCareerFee = "dfds";//Convert.ToDecimal(dr["CareerFee"]).ToString("C");
                        //objModel.strMargin = "dfds";//Convert.ToDecimal(dr["Margin"]).ToString("C");
                        //objModel.strMarginPercent = "dfds";// Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        //objModel.LoadTypeName = "dfds";// Convert.ToString(dr["LoadtypeName"]);




                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<UserDashboardModel> getAllPendingLoadsByPaging(long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<UserDashboardModel> objList = new List<UserDashboardModel>();
            string query = "GetAllPendingLoadsByPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserDashboardModel objModel = new UserDashboardModel();
                        objModel.LoadID = Convert.ToString(dr["LoadID"]);
                        objModel.LoadType = Convert.ToString(dr["LoadType"]);
                        objModel.Role = Convert.ToString(dr["Role"]);
                        objModel.WO = Convert.ToString(dr["WO"]);
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                        // objModel.BillTo = Convert.ToString(dr["BillTo"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.strShipperDate = dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        // objModel.strLoadDate = dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        objModel.strDeliveredDate = dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.Location = Convert.ToString(dr["Location"]);
                        objModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                        objModel.Status = Convert.ToString(dr["Status"]);
                        //objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.strRate = Convert.ToDecimal(dr["Rate"]).ToString("C").Replace(".00", "");
                        objModel.strCareerFee = Convert.ToDecimal(dr["CareerFee"]).ToString("C").Replace(".00", "");
                        //objModel.strMargin = Convert.ToDecimal(dr["Margin"]).ToString("C").Replace(".00", "");
                        //objModel.strMarginPercent = Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        //      objModel.LoadTypeName = Convert.ToString(dr["LoadtypeName"]);
                        objModel.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
                        objModel.ShipperInvoiceSentDate = Convert.ToString(dr["ShipperInvoiceSentDate"]);
                        objModel.ShipperPaymentReceivedDate = Convert.ToString(dr["ShipperPaymentReceivedDate"]);
                        objModel.CarrierInvoiceReceivedDate = Convert.ToString(dr["CarrierInvoiceReceivedDate"]);
                        objModel.CarrierPaymentMadeDate = Convert.ToString(dr["CarrierPaymentMadeDate"]);
                        objModel.IsShipperPaymentReceived = Convert.ToBoolean(dr["IsShipperPaymentReceived"]);
                        objModel.IsCarrierInvoiceReceived = Convert.ToBoolean(dr["IsCarrierInvoiceReceived"]);
                        objModel.IsCarrierPaymentMade = Convert.ToBoolean(dr["IsCarrierPaymentMade"]);
                        objModel.IsShipperInvoiceSent = Convert.ToBoolean(dr["IsShipperInvoiceSent"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        int paymentType = Convert.ToInt32(dr["PaymentType"]);
                        objModel.PaymentType = paymentType == 0 ? "Not Set" : Enum.GetName(typeof(PaymentTypeEnum), paymentType);
                        //objModel.LoadID = 1;// Convert.ToInt64(dr["LoadID"]);
                        //objModel.LoadType = 1;//  Convert.ToInt32(dr["LoadType"]);
                        //objModel.LoadNo = Convert.ToInt64(dr["LoadNo"]);
                        //objModel.BillTo = 1;// Convert.ToInt64(dr["BillTo"]);
                        //objModel.CareerName = "dfds";// Convert.ToString(dr["CareerName"]);
                        //objModel.CreatedDate =  Convert.ToDateTime(dr["CreatedDate"]);
                        //objModel.strShipperDate = "dfds";//dr["ShipperDate"] == DBNull.Value ? "" : Convert.ToString(dr["ShipperDate"]);
                        //objModel.strLoadDate = "dfds";// dr["LoadDate"] == DBNull.Value ? "" : Convert.ToString(dr["LoadDate"]);
                        //objModel.strDeliveredDate = "dfds";//dr["ConsigneeDate"] == DBNull.Value ? "" : Convert.ToString(dr["ConsigneeDate"]);
                        //objModel.CustomerName = "dfds";// Convert.ToString(dr["CustomerName"]);
                        //objModel.Location = "dfds";//Convert.ToString(dr["Location"]);
                        //objModel.ConsigneeLocation = "dfds";// Convert.ToString(dr["ConsigneeLocation"]);
                        //objModel.Status = 1;// Convert.ToInt32(dr["Status"]);
                        //objModel.AddedByUser = "dfds";//Convert.ToString(dr["AddedByUser"]);
                        //objModel.TeamLead = "dfds";// Convert.ToString(dr["TeamLead"]);
                        //objModel.TeamManager = "dfds";//Convert.ToString(dr["TeamManager"]);
                        //objModel.strRate = "dfds";//Convert.ToDecimal(dr["Rate"]).ToString("C");
                        //objModel.strCareerFee = "dfds";//Convert.ToDecimal(dr["CareerFee"]).ToString("C");
                        //objModel.strMargin = "dfds";//Convert.ToDecimal(dr["Margin"]).ToString("C");
                        //objModel.strMarginPercent = "dfds";// Convert.ToDecimal(dr["MarginPercent"]).ToString();
                        //objModel.LoadTypeName = "dfds";// Convert.ToString(dr["LoadtypeName"]);




                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CarrierDashboardModel> getAllLoadsByCarriers(long UserID, string CarrierID, string FilterTypeID, string FromDate, string ToDate)
        {
            List<CarrierDashboardModel> objList = new List<CarrierDashboardModel>();
            string query = "getCarrierReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@CarrierID", CarrierID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);

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
                        CarrierDashboardModel objModel = new CarrierDashboardModel();
                        objModel.CarrierName = Convert.ToString(dr["CareerName"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.CarrierPay = Convert.ToString(dr["CarriwerPay"]);
                        objModel.Miles = Convert.ToString(dr["Miles"]);
                        objModel.RevenueMiles = Convert.ToString(dr["RevenuePerMile"]);
                        objModel.PayPerMiles = Convert.ToString(dr["PayPerMile"]);
                        objModel.IsPolicyExpires = Convert.ToBoolean(dr["Expiry"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CarrierDashboardModel> getAllLoadsByCarriersPaging(long UserID, string CarrierID, string FilterTypeID, string FromDate, string ToDate, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<CarrierDashboardModel> objList = new List<CarrierDashboardModel>();
            string query = "getCarrierReportPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@CarrierID", CarrierID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@Todate", ToDate);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        CarrierDashboardModel objModel = new CarrierDashboardModel();
                        objModel.CarrierName = Convert.ToString(dr["CareerName"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.CarrierPay = Convert.ToString(dr["CarriwerPay"]);
                        objModel.Miles = Convert.ToString(dr["Miles"]);
                        objModel.RevenueMiles = Convert.ToString(dr["RevenuePerMile"]);
                        objModel.PayPerMiles = Convert.ToString(dr["PayPerMile"]);
                        objModel.IsPolicyExpires = Convert.ToBoolean(dr["Expiry"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CustomerDashboardModel> getAllLoadsByCustomer(long UserID, string CustomerID, string FilterTypeID, string FromDate, string ToDate)
        {
            List<CustomerDashboardModel> objList = new List<CustomerDashboardModel>();
            string query = "getCustomerReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);

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
                        CustomerDashboardModel objModel = new CustomerDashboardModel();
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CustomerDashboardModel> getAllLoadsByCustomerPaging(long UserID, string CustomerID, string FilterTypeID, string FromDate, string ToDate, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<CustomerDashboardModel> objList = new List<CustomerDashboardModel>();
            string query = "getCustomerReportPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerDashboardModel objModel = new CustomerDashboardModel();
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<DispatcherDashboardModel> getAllLoadsByDispatcher(long UserID, string DispatcherID, string FilterTypeID, string FromDate, string ToDate)
        {
            List<DispatcherDashboardModel> objList = new List<DispatcherDashboardModel>();
            string query = "getDispatcherReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@DispatcherID", DispatcherID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);

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
                        DispatcherDashboardModel objModel = new DispatcherDashboardModel();
                        objModel.DispatcherName = Convert.ToString(dr["Alias"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<DispatcherDashboardModel> getAllLoadsByDispatcherPaging(long UserID, string DispatcherID, string FilterTypeID, string FromDate, string ToDate, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<DispatcherDashboardModel> objList = new List<DispatcherDashboardModel>();
            string query = "getDispatcherReportPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@DispatcherID", DispatcherID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DispatcherDashboardModel objModel = new DispatcherDashboardModel();
                        objModel.DispatcherName = Convert.ToString(dr["Alias"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<SalesRepDashboardModel> getAllLoadsBySaleRep(long UserID, string SalesRepID, string FilterTypeID, string FromDate, string ToDate)
        {
            List<SalesRepDashboardModel> objList = new List<SalesRepDashboardModel>();
            string query = "getSalesRepresentativeReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@SaleRepID", SalesRepID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);

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
                        SalesRepDashboardModel objModel = new SalesRepDashboardModel();
                        objModel.SaleRepName = Convert.ToString(dr["SaleRep"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<SalesRepDashboardModel> getAllLoadsBySaleRepPaging(long UserID, string SalesRepID, string FilterTypeID, string FromDate, string ToDate, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<SalesRepDashboardModel> objList = new List<SalesRepDashboardModel>();
            string query = "getSalesRepresentativeReportPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@SaleRepID", SalesRepID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        SalesRepDashboardModel objModel = new SalesRepDashboardModel();
                        objModel.SaleRepName = Convert.ToString(dr["SaleRep"]);
                        objModel.TotalLoads = Convert.ToString(dr["Loads"]);
                        objModel.GrossRevenue = Convert.ToString(dr["GrossRevenue"]);
                        objModel.NetProfit = Convert.ToString(dr["NetProfit"]);
                        objModel.OpenLoad = Convert.ToString(dr["OpenLoad"]);
                        objModel.DeliveredLoad = Convert.ToString(dr["DeliveredLoad"]);
                        objModel.CompletedLoad = Convert.ToString(dr["CompletedLoad"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<LoadDashboardModel> getAllLoadsByLoadStatus(long UserID, string LoadStatusID, string FilterTypeID, string FromDate, string ToDate)
        {
            List<LoadDashboardModel> objList = new List<LoadDashboardModel>();
            string query = "getLoadDashboardReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@LoadStatusID", LoadStatusID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);

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
                        LoadDashboardModel objModel = new LoadDashboardModel();
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.LoadStatus = Convert.ToString(dr["Status"]);
                        objModel.Dispatcher = Convert.ToString(dr["Dispatcher"]);
                        objModel.DateAdded = Convert.ToString(dr["CreatedDate"]);
                        objModel.CarrierName = Convert.ToString(dr["CareerName"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                        objModel.ShipperDate = Convert.ToString(dr["ShipperDate"]);
                        objModel.Location = Convert.ToString(dr["Location"]);
                        objModel.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                        objModel.ConsigneeDate = Convert.ToString(dr["ConsigneeDate"]);
                        objModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<LoadDashboardModel> getAllLoadsByLoadStatusPaging(long UserID, string LoadStatusID, string FilterTypeID, string FromDate, string ToDate, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<LoadDashboardModel> objList = new List<LoadDashboardModel>();
            string query = "getLoadDashboardReportPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@LoadStatusID", LoadStatusID);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        LoadDashboardModel objModel = new LoadDashboardModel();
                        objModel.LoadId = Convert.ToInt32(dr["LoadId"]);
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.LoadStatus = Convert.ToString(dr["Status"]);
                        objModel.Dispatcher = Convert.ToString(dr["Dispatcher"]);
                        objModel.DateAdded = Convert.ToString(dr["CreatedDate"]);
                        objModel.CarrierName = Convert.ToString(dr["CareerName"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                        objModel.ShipperDate = Convert.ToString(dr["ShipperDate"]);
                        objModel.Location = Convert.ToString(dr["Location"]);
                        objModel.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                        objModel.ConsigneeDate = Convert.ToString(dr["ConsigneeDate"]);
                        objModel.ConsigneeLocation = Convert.ToString(dr["ConsigneeLocation"]);
                        objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public static DataSet getDashboardReport(long userid, string FilterTypeID, string FromDate, string ToDate)
        {
            var selectList = new List<Autocomplete>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            DataSet dt = new DataSet();
            using (SqlCommand cmd = new SqlCommand("getdashboardReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@DateType", FilterTypeID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
    }
}