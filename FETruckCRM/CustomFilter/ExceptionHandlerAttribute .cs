using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FETruckCRM.Models;

namespace FETruckCRM.CustomFilter
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            SqlConnection con;

                        string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);

            if (!filterContext.ExceptionHandled)
            {


                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    actionName = filterContext.RouteData.Values["action"].ToString(),
                    // LogTime = DateTime.Now
                };
                string query = "proc_insException";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExceptionMessage", logger.ExceptionMessage);
                    cmd.Parameters.AddWithValue("@ExceptionStackTrace", logger.ExceptionStackTrace);
                    cmd.Parameters.AddWithValue("@ControllerName", logger.ControllerName);
                    cmd.Parameters.AddWithValue("@actionName", logger.actionName);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    sda.Fill(dt);
                    con.Close();
                }
                //ApplicationModel ctx = new ApplicationModel();
                //ctx.ExceptionLoggers.Add(logger);
                //ctx.SaveChanges();

                filterContext.ExceptionHandled = true;
            }
        }
    }
}