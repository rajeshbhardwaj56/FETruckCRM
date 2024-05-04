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
using System.Net.Mail;
using System.Net;

namespace FETruckCRM.Data
{
    public class LoginService
    {
        SqlConnection con;
        public LoginService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public bool IsValidUser(string email, string password)
        {
            var encryptpassowrd = MD5Hash(password);
            bool IsValid = false;
            using (SqlCommand cmd = new SqlCommand("proc_CheckUserLogin", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", encryptpassowrd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public UserModel getUserDetailsByUsername(string Email)
        {
            UserModel objModel = null;
            using (SqlCommand cmd = new SqlCommand("proc_getUserByUserName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    objModel = new UserModel();
                    objModel.UserID = Convert.ToInt64(dr["UserID"]);
                    objModel.FirstName = Convert.ToString(dr["FirstName"]);
                    objModel.LastName = Convert.ToString(dr["LastName"]);
                    objModel.Username = Convert.ToString(dr["Username"]);
                    objModel.EmailId = Convert.ToString(dr["EmailId"]);
                    objModel.Password = Convert.ToString(dr["Password"]);
                    objModel.Status = Convert.ToBoolean(dr["Status"]);
                    objModel.RoleID = Convert.ToInt32(dr["RoleID"]);
                    objModel.OTP = Convert.ToString(dr["OTP"]);
                    if (dr["OTP"] != DBNull.Value)
                        objModel.OTP = Convert.ToString(dr["OTP"]);
                    if (dr["OtpDateTime"] != DBNull.Value)
                        objModel.OTPDateTime = Convert.ToDateTime(dr["OtpDateTime"]);

                }
            }
            return objModel;
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public bool ChangePassword(string username, string password)
        {
            var encryptpassowrd = MD5Hash(password);
            bool IsValid = false;
            using (SqlCommand cmd = new SqlCommand("ChangeAdminPassword", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", encryptpassowrd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public bool ChangeUserPassword(string UserID, string password)
        {
            var encryptpassowrd = MD5Hash(password);
            bool IsValid = false;
            using (SqlCommand cmd = new SqlCommand("ChangeUserPassword", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@Password", encryptpassowrd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public bool ChangeUserPasswordByEmail(string EmailID, string password)
        {
            var encryptpassowrd = MD5Hash(password);
            bool IsValid = false;
            using (SqlCommand cmd = new SqlCommand("ChangeUserPasswordByEmail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@Password", encryptpassowrd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public bool SendUserOTP(UserModel model)
        {
            bool IsValid = false;
            using (SqlCommand cmd = new SqlCommand("proc_SendUserOtp", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID",model.EmailId);
                cmd.Parameters.AddWithValue("@OTP", model.OTP);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    if(SendOTPEmail(model));
                    IsValid = true;
                }
            }
            return IsValid;
        }

        //public bool SendEmail(UserModel model)
        //{
        //    bool IsValid = false;
        //    using (SqlCommand cmd = new SqlCommand("ChangeAdminPassword", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Username", username);
        //        cmd.Parameters.AddWithValue("@Password", encryptpassowrd);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        con.Open();
        //        sda.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
        //        {
        //            IsValid = true;
        //        }
        //    }
        //    return IsValid;
        //}


        public bool SendOTPEmail(UserModel model)
        {
            bool res = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<p><span style='color:#000080;'>Hi</span><strong><span style='color:#000080;'> ##USERNAME##,</span></strong></p><p> &nbsp;</p>");
                sb.Append("<p><span style='color: rgb(0, 0, 128);'>Your OTP for the login is ##OTP##. OTP is secret and can be used only once, will be expired after 10 mins. Therefore, do not disclose this to anyone. </span>\r\n");
                sb.Append("<p>&nbsp;</p><p><strong><span style='color:#000080;'>Regards,</span></strong></p>  <p>   <strong><span style='color:#000080;'>CRM Team</span></strong></p>  <p> <img src='http://www.crmeternity.com/assets/images/ellogo.png' width='100px' /></p>  ");
                sb.Replace("##USERNAME##", model.FirstName+" "+model.LastName);
                sb.Replace("##OTP##", model.OTP);
                MailMessage message = new MailMessage();
                using (var smtp = new SmtpClient())
                {

                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                    message.To.Add(new MailAddress(model.EmailId));
                    message.Subject = "Fleet Experts - One Time Password";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = sb.ToString();
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    smtp.Host = ConfigurationManager.AppSettings["host"]; //for gmail host  
                    smtp.UseDefaultCredentials = Convert.ToInt32(ConfigurationManager.AppSettings["defaultcredential"]) == 1 ? true : false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
                    smtp.EnableSsl = Convert.ToInt32(ConfigurationManager.AppSettings["enablessl"]) == 1 ? true : false;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    res = true;
                }



            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool SendEmail(UserModel model)
        {
            bool res = false; 
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Dear " + model.FirstName + " " + model.LastName + ",<br/> Click on below given link to Reset Your Password<br/>");
               // sb.Append("<a href=http://localhost:57355/Account/reset?un=" + model.UserID);
                sb.Append("<a href="+ Convert.ToString(ConfigurationManager.AppSettings["ResetPasswordURL"]) + model.UserID);
                sb.Append(">Click here to change your password</a><br/>");
                sb.Append("<b>Thanks</b>,<br> Fleet Experts <br/>");
                MailMessage message = new MailMessage();
                using (var smtp = new SmtpClient())
                {

                    message.From = new MailAddress(ConfigurationManager.AppSettings["fromemail"]);
                    message.To.Add(new MailAddress(model.EmailId));
                    message.Subject = "Fleet Experts - Reset Password";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = sb.ToString();
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    smtp.Host = ConfigurationManager.AppSettings["host"]; //for gmail host  
                    smtp.UseDefaultCredentials = Convert.ToInt32(ConfigurationManager.AppSettings["defaultcredential"]) == 1 ? true : false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
                    smtp.EnableSsl = Convert.ToInt32(ConfigurationManager.AppSettings["enablessl"]) == 1 ? true : false;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    res = true;
                }



            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool CheckAdminUsername(string Username)
        {
            bool isvalid = false;
            string query = "proc_CheckAdminUsername";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
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

        public int CheckUserOtp(string emailId,string otp)
        {
            int result = 0;
            string query = "proc_CheckUserOtp";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emailId", emailId);
                cmd.Parameters.AddWithValue("@otp", otp);
                con.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return result;
        }

        public bool CheckUserByUserID(string UserID)
        {
            bool isvalid = false;
            string query = "proc_CheckUserByUserID";
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

    }
}