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

namespace FETruckCRM.Data
{
    public class LogService
    {
        SqlConnection con;
        public LogService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        

        public DataSet getLogs(int UserId,string FrmDate,string ToDate,int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<LogModel> objList = new List<LogModel>();
            string query = "getLogListing";
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Todate", ToDate);
                cmd.Parameters.AddWithValue("@FromDate", FrmDate);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                
                con.Open();
                sda.Fill(ds);
                con.Close();
                
            }
            return ds;
        }


    }
}