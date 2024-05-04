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
    public class EmailService
    {
        SqlConnection con;
        public EmailService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterEmail(EmailModel objModel)
        {
            Int64 retVal = 0;


            string query = "insupdEmails";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", objModel.EmailID);
                cmd.Parameters.AddWithValue("@EmailTypeID", objModel.strEmailTypeID);
                cmd.Parameters.AddWithValue("@Subject", objModel.Subject);
                cmd.Parameters.AddWithValue("@EmailAddress", objModel.EmailAddress);
                cmd.Parameters.AddWithValue("@Body", objModel.Body);
                cmd.Parameters.AddWithValue("@Status", 1);
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

        public bool CheckEmailType(string EmailID,string EmailTypeID)
        {
            bool isvalid = false;
            string query = "proc_CheckEmailtype";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@EmailtypeID", EmailTypeID);
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

        public static List<SelectListItem> getAllEmailTypes()
        {
            var objList = new List<SelectListItem>();
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
           var con = new SqlConnection(constring);
            string query = "getAllEmailtypes";
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
                    
                        //EmailTypeModel objModel = new EmailTypeModel();
                        //objModel.EmailTypeID = Convert.ToInt32(dr["EmailTypeID"]);
                        //objModel.EmailType = Convert.ToString(dr["EmailType"]);
                        //objList.Add(objModel);
                        objList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                        foreach (DataRow dr in dt.Rows)
                        {
                            objList.Add(new SelectListItem { Value = Convert.ToInt64(dr["EmailTypeID"]).ToString(), Text = Convert.ToString(dr["EmailType"]) });
                        }

                   
                }
            }
            return objList;
        }
        public List<EmailModel> getAllEmails(long UserID)
        {
            List<EmailModel> objList = new List<EmailModel>();
            string query = "getallEmailList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
               // cmd.Parameters.AddWithValue("@UserID", UserID);

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
                        EmailModel objModel = new EmailModel();
                        objModel.EmailID = Convert.ToInt64(dr["EmailID"]);
                        objModel.EmailTypeID = Convert.ToInt32(dr["EmailTypeID"]);
                        objModel.EmailType = Convert.ToString(dr["EmailType"]);
                        objModel.Subject = Convert.ToString(dr["Subject"]);
                        objModel.Body = Convert.ToString(dr["Body"]);
                        objModel.EmailAddress = Convert.ToString(dr["EmailAddress"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objList.Add(objModel);
                    }
                }
            }
            return objList;
        }

        public EmailModel getEmailByEmailID(Int64? EmailID)
        {
            EmailModel objModel = new EmailModel();
            string query = "getEmailByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);

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
                        objModel.EmailID = Convert.ToInt64(dr["EmailID"]);
                        objModel.EmailTypeID= Convert.ToInt32(dr["EmailTypeID"]);
                        objModel.strEmailTypeID = Convert.ToString(dr["EmailTypeID"]);
                        objModel.Subject = Convert.ToString(dr["Subject"]);
                        objModel.Body = Convert.ToString(dr["Body"]);
                        objModel.EmailAddress = Convert.ToString(dr["EmailAddress"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                    }
                }

            }
            return objModel;
        }

        public EmailModel getEmailByEmailTypeID(int EmailTypeID)
        {
            EmailModel objModel = new EmailModel();
            string query = "getEmailByTypeID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailTypeID", EmailTypeID);

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
                        objModel.EmailID = Convert.ToInt64(dr["EmailID"]);
                        objModel.EmailTypeID = Convert.ToInt32(dr["EmailTypeID"]);
                        objModel.strEmailTypeID = Convert.ToString(dr["EmailTypeID"]);
                        objModel.Subject = Convert.ToString(dr["Subject"]);
                        objModel.Body = Convert.ToString(dr["Body"]);
                        objModel.EmailAddress = Convert.ToString(dr["EmailAddress"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                    }
                }

            }
            return objModel;
        }

        public long deleteEmail(Int64 EmailID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteEmail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
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