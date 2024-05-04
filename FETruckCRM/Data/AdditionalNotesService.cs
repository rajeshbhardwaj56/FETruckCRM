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
    public class AdditionalNotesService
    {
        SqlConnection con;
        public AdditionalNotesService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterAdditionalNotes(AdditionalNotesModel objModel)
        {
            Int64 retVal = 0;
            string query = "insupdLoadAdditionalNotes";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadAdditionalNotesID", objModel.LoadAdditionalNotesID);
                cmd.Parameters.AddWithValue("@LoadID", objModel.LoadID);
                cmd.Parameters.AddWithValue("@LoadNo", objModel.LoadNo);
                cmd.Parameters.AddWithValue("@DriverPayNotes", objModel.DriverPayNotes);
                cmd.Parameters.AddWithValue("@DPNAppearOnReport", objModel.DPNAppearOnReport);
                cmd.Parameters.AddWithValue("@InvoiceNotes", objModel.InvoiceNotes);
                cmd.Parameters.AddWithValue("@INAppearOnInvoice", objModel.INAppearOnInvoice);
                cmd.Parameters.AddWithValue("@InvoiceDescription", objModel.InvoiceDescription);
                cmd.Parameters.AddWithValue("@DeletedRefusalNotes", objModel.DeletedRefusalNotes);
                cmd.Parameters.AddWithValue("@RecInvoiceNo", objModel.RecInvoiceNo);
                cmd.Parameters.AddWithValue("@RecInvoiceDate", objModel.RecInvoiceDate);
                cmd.Parameters.AddWithValue("@RecAmount", objModel.RecAmount);
                cmd.Parameters.AddWithValue("@RecCustomer", objModel.RecCustomer);
                cmd.Parameters.AddWithValue("@RecContact", objModel.RecContact);
                cmd.Parameters.AddWithValue("@RecTelephone", objModel.RecTelephone);
                cmd.Parameters.AddWithValue("@RecTelephoneExt", objModel.RecTelephoneExt);
                cmd.Parameters.AddWithValue("@RecTollFree", objModel.RecTollFree);
                cmd.Parameters.AddWithValue("@RecFax", objModel.RecFax);
                cmd.Parameters.AddWithValue("@RecNotes", objModel.RecNotes);
                cmd.Parameters.AddWithValue("@InternalNotes", objModel.InternalNotes);
                cmd.Parameters.AddWithValue("@ConDispatcherName", objModel.ConDispatcherName);
                cmd.Parameters.AddWithValue("@ConDispatcherPhone", objModel.ConDispatcherPhone);
                cmd.Parameters.AddWithValue("@conDispatcherEmail", objModel.conDispatcherEmail);
                cmd.Parameters.AddWithValue("@conDriverName", objModel.conDriverName);
                cmd.Parameters.AddWithValue("@ConDriverPhone", objModel.ConDriverPhone);
                cmd.Parameters.AddWithValue("@conDriverEmail", objModel.conDriverEmail);
                cmd.Parameters.AddWithValue("@ConTruck", objModel.ConTruck);
                cmd.Parameters.AddWithValue("@ConTrailer", objModel.ConTrailer);
                cmd.Parameters.AddWithValue("@ConNotes", objModel.ConNotes);
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
        public AdditionalNotesModel getAdditionalNotesByLoadID(Int64 LoadID)
        {
            AdditionalNotesModel objModel = new AdditionalNotesModel();
            string query = "getLoadAdditionalNotes";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);
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
                        objModel.LoadAdditionalNotesID = Convert.ToInt64(dr["LoadAdditionalNotesID"]);
                        objModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                        objModel.LoadNo = Convert.ToString(dr["LoadNo"]);
                        objModel.DriverPayNotes = Convert.ToString(dr["DriverPayNotes"]);
                        objModel.DPNAppearOnReport = Convert.ToString(dr["DPNAppearOnReport"]);
                        objModel.InvoiceNotes = Convert.ToString(dr["InvoiceNotes"]);
                        objModel.INAppearOnInvoice = Convert.ToString(dr["INAppearOnInvoice"]);
                        objModel.InvoiceDescription = Convert.ToString(dr["InvoiceDescription"]);
                        objModel.DeletedRefusalNotes = Convert.ToString(dr["DeletedRefusalNotes"]);
                        objModel.RecInvoiceNo = Convert.ToString(dr["RecInvoiceNo"]);
                        objModel.RecInvoiceDate = Convert.ToString(dr["RecInvoiceDate"]);
                        objModel.RecAmount = Convert.ToDecimal(dr["RecAmount"]);
                        objModel.RecCustomer = Convert.ToString(dr["RecCustomer"]);
                        objModel.RecContact = Convert.ToString(dr["RecContact"]);
                        objModel.RecTelephone = Convert.ToString(dr["RecTelephone"]);
                        objModel.RecTelephoneExt = Convert.ToString(dr["RecTelephoneExt"]);
                        objModel.RecTollFree = Convert.ToString(dr["RecTollFree"]);
                        objModel.RecFax = Convert.ToString(dr["RecFax"]);
                        objModel.RecNotes = Convert.ToString(dr["RecNotes"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.ConDispatcherName = Convert.ToString(dr["ConDispatcherName"]);
                        objModel.ConDispatcherPhone = Convert.ToString(dr["ConDispatcherPhone"]);
                        objModel.conDispatcherEmail = Convert.ToString(dr["conDispatcherEmail"]);
                        objModel.conDriverName = Convert.ToString(dr["conDriverName"]);
                        objModel.ConDriverPhone = Convert.ToString(dr["ConDriverPhone"]);
                        objModel.conDriverEmail = Convert.ToString(dr["conDriverEmail"]);
                        objModel.ConTruck = Convert.ToString(dr["ConTruck"]);
                        objModel.ConTrailer = Convert.ToString(dr["ConTrailer"]);
                        objModel.ConNotes = Convert.ToString(dr["ConNotes"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

                    }
                }

            }
            return objModel;
        }

    }



    public class LoadFilesService
    {
        SqlConnection con;
        public LoadFilesService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }

        public Int64 RegisterLoadFiles(LoadFilesModel objModel)
        {
            Int64 retVal = 0;
            string query = "insupdLoadFiles";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadFilesID", objModel.LoadFilesID);
                cmd.Parameters.AddWithValue("@LoadID", objModel.LoadID);
                cmd.Parameters.AddWithValue("@FileName", objModel.FileName);
                cmd.Parameters.AddWithValue("@FileSize", objModel.FileSize);
                cmd.Parameters.AddWithValue("@FileURL", objModel.FileURL);
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

        public List<LoadFilesModel> getAllLoadfiles(long LoadID)
        {
            List<LoadFilesModel> objList = new List<LoadFilesModel>();
            string query = "GetAllLoadfiles";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@LoadID", LoadID);

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
                        LoadFilesModel objModel = new LoadFilesModel();
                        objModel.LoadFilesID = Convert.ToInt64(dr["LoadFilesID"]);
                        objModel.LoadID = Convert.ToInt64(dr["LoadID"]);
                        objModel.FileURL = Convert.ToString(dr["FileURL"]);
                        objModel.FileName = Convert.ToString(dr["FileName"]);
                        objModel.FileSize = Convert.ToString(dr["FileSize"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

       

        public long deleteLoadFiles(Int64 LoadfilesID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteLoadfiles", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadfilesID", LoadfilesID);
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