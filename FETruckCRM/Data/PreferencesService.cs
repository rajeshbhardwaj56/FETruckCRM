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
    public class PreferencesService
    {
        SqlConnection con;
        public PreferencesService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterPreferences(PreferencesModel objModel)
        {
            Int64 retVal = 0;
            string query = "insupdPreferences";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PreferenceID", objModel.PreferenceID);
                cmd.Parameters.AddWithValue("@StandardInvoiceNotes", objModel.StandardInvoiceNotes);
                cmd.Parameters.AddWithValue("@StandardLoadSheetNotes", objModel.StandardLoadSheetNotes);
                cmd.Parameters.AddWithValue("@StandardCustomerSheetNotes", objModel.StandardCustomerSheetNotes);
                cmd.Parameters.AddWithValue("@StandardQuoteNotes", objModel.StandardQuoteNotes);
                cmd.Parameters.AddWithValue("@StandardBOLNotes", objModel.StandardBOLNotes);
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

        public PreferencesModel getAllPreferencess()
        {
            string query = "getPreferencesByID";
            PreferencesModel objModel = new PreferencesModel();
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
                    DataRow dr = dt.Rows[0];
                    objModel.PreferenceID = Convert.ToInt64(dr["PreferenceID"]);
                    objModel.StandardInvoiceNotes = Convert.ToString(dr["StandardInvoiceNotes"]);
                    objModel.StandardLoadSheetNotes = Convert.ToString(dr["StandardLoadSheetNotes"]);
                    objModel.StandardCustomerSheetNotes = Convert.ToString(dr["StandardCustomerSheetNotes"]);
                    objModel.StandardQuoteNotes = Convert.ToString(dr["StandardQuoteNotes"]);
                    objModel.StandardBOLNotes = Convert.ToString(dr["StandardBOLNotes"]);

                }
            }
            return objModel;
        }




    }
}