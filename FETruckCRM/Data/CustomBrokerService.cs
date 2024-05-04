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

namespace FETruckCRM.Data
{
    public class CustomBrokerService
    {
        SqlConnection con;
        public CustomBrokerService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterCustomBroker(CustomBrokerModel objModel)
        {
            Int64 retVal = 0;
            string query = "insupdCustomBroker";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomBrokerID", objModel.CustomBrokerID);
                cmd.Parameters.AddWithValue("@BrokerName", objModel.BrokerName);
                cmd.Parameters.AddWithValue("@Crossing", objModel.Crossing);
                cmd.Parameters.AddWithValue("@Telephone", objModel.Telephone);
                cmd.Parameters.AddWithValue("@TelephoneExt", objModel.TelephoneExt);
                cmd.Parameters.AddWithValue("@TollFree", objModel.TollFree);
                cmd.Parameters.AddWithValue("@Fax", objModel.Fax);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@StatusInd", Convert.ToInt32(objModel.strStatusInd));
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

        public List<CustomBrokerModel> getAllCustomBrokers(long UserID)
        {
            List<CustomBrokerModel> objList = new List<CustomBrokerModel>();
            string query = "getallCustomBroker";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@LoggedUserID", UserID);

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
                        CustomBrokerModel objModel = new CustomBrokerModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomBrokerID = Convert.ToInt64(dr["CustomBrokerID"]);
                        objModel.BrokerName = Convert.ToString(dr["BrokerName"]);
                        objModel.Crossing = Convert.ToString(dr["Crossing"]);
                        objModel.StatusInd = Convert.ToInt32(dr["StatusInd"]);
                        var status = Convert.ToInt32(dr["StatusInd"]);
                        objModel.strStatusInd = status == 1 ?  "Active" : "Inactive";
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);





                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

        public CustomBrokerModel getCustomBrokerByCustomBrokerID(Int64? CustomBrokerID)
        {
            CustomBrokerModel objModel = new CustomBrokerModel();
            string query = "getCustomBrokerByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomBrokerID", CustomBrokerID);
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
                        objModel.CustomBrokerID = Convert.ToInt64(dr["CustomBrokerID"]);
                        objModel.BrokerName = Convert.ToString(dr["BrokerName"]);
                        objModel.Crossing = Convert.ToString(dr["Crossing"]);
                        objModel.StatusInd = Convert.ToInt32(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToString(dr["StatusInd"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        
                    }
                }

            }
            return objModel;
        }
        public bool CheckCustomBrokerName(string CustomBrokerName, long CustomBrokerID)
        {
            bool isvalid = false;
            string query = "proc_CheckCustomBrokerByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomBrokerName", CustomBrokerName);
                cmd.Parameters.AddWithValue("@CustomBrokerID", CustomBrokerID);
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

        public long deleteCustomBroker(Int64 CustomBrokerID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteCustomBroker", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomBrokerID", CustomBrokerID);
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

      
    }
}