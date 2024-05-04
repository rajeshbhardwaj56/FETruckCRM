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
    public class EquipmentTypeService
    {
        SqlConnection con;
        public EquipmentTypeService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterEquipmentType(EquipmentTypeModel objModel)
        {
            Int64 retVal = 0;


            string query = "insupdEquipmenttype";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EquipmentTypeID", objModel.EquipmentTypeID);
                cmd.Parameters.AddWithValue("@EquipmentTypeName", objModel.EquipmentTypeName);
                cmd.Parameters.AddWithValue("@StatusInd", objModel.strStatusInd=="1");
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

        public List<EquipmentTypeModel> getAllEquipmentTypes(long UserID)
        {
            List<EquipmentTypeModel> objList = new List<EquipmentTypeModel>();
            string query = "getallEquipmentList";
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
                        EquipmentTypeModel objModel = new EquipmentTypeModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.EquipmentTypeID = Convert.ToInt64(dr["EquipmentTypeID"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);

                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

       

    
      
        public EquipmentTypeModel getEquipmentTypeByEquipmentTypeID(Int64? EquipmentTypeID)
        {
            EquipmentTypeModel objModel = new EquipmentTypeModel();
            string query = "getEquipmentTypeByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EquipmentTypeID", EquipmentTypeID);

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
                        objModel.EquipmentTypeID = Convert.ToInt64(dr["EquipmentTypeID"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = (Convert.ToBoolean(dr["StatusInd"]) == true) ? "1" : "0";
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                    }
                }

            }
            return objModel;
        }
        public bool CheckEquipmentTypeName(string EquipmentTypeName, long EquipmentTypeID)
        {
            bool isvalid = false;
            string query = "proc_CheckEquipmentTypeByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EquipmentTypeName", EquipmentTypeName);
                cmd.Parameters.AddWithValue("@EquipmentTypeID", EquipmentTypeID);
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

        public long deleteEquipmentType(Int64 EquipmentTypeID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteEquipmentType", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EquipmentTypeID", EquipmentTypeID);
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