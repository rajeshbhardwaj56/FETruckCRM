using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FETruckCRM.Common
{
    public static class HtmlHelperExtension
    {
       public static string getMonthName(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);
            return date.ToString("MMM")+"("+year.ToString()+")";
        }

        public static List<SelectListItem> GetFilterType()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "1", Text = "Ship Date" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Delivery Date" });
            selectList.Add(new SelectListItem { Value = "3", Text = "Date added" });
            selectList.Add(new SelectListItem { Value = "4", Text = "Invoiced Date" });
            return selectList;
        }

        public static List<SelectListItem> GetDateFilter()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "1", Text = "Custom Range" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Last Year" });
            selectList.Add(new SelectListItem { Value = "3", Text = "Last Month" });
            selectList.Add(new SelectListItem { Value = "4", Text = "Last Week" });
            selectList.Add(new SelectListItem { Value = "5", Text = "Yesterday" });
            selectList.Add(new SelectListItem { Value = "6", Text = "Today" });
            selectList.Add(new SelectListItem { Value = "7", Text = "This Week-To-Date" });
            selectList.Add(new SelectListItem { Value = "8", Text = "This Month-To-Date" });
            selectList.Add(new SelectListItem { Value = "9", Text = "This Year-To-Date" });
            return selectList;
        }
        public static List<SelectListItem> GetApprovalStatusListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "1", Text = "Pending Approval" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Approved" });
            selectList.Add(new SelectListItem { Value = "3", Text = "Not Approved" });

            return selectList;
        }


        public static List<SelectListItem> GetCurrencyListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "1", Text = "American Dollars" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Canadian Dollar" });
            selectList.Add(new SelectListItem { Value = "3", Text = "Euros" });

            return selectList;
        }
        public static List<SelectListItem> GetStatusListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "0", Text = "Inactive" });
            selectList.Add(new SelectListItem { Value = "1", Text = "Active" });

            return selectList;
        }
        public static List<SelectListItem> GetAppointmentListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "1", Text = "No" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Yes" });

            return selectList;
        }

        public static List<SelectListItem> GetMCFFListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "MC" });
            selectList.Add(new SelectListItem { Value = "1", Text = "FF" });

            return selectList;
        }

        public static List<SelectListItem> GetFSCTypeListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0", Text = "$" });
            selectList.Add(new SelectListItem { Value = "1", Text = "%" });

            return selectList;
        }

        public static List<SelectListItem> GetQuantityType()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "ltr", Text = "Liter" });
            selectList.Add(new SelectListItem { Value = "Kg", Text = "Kilogram" });
            selectList.Add(new SelectListItem { Value = "Pkt", Text = "Packet" });

            return selectList;
        }
        public static List<SelectListItem> GetPaymentSelectListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });
            selectList.Add(new SelectListItem { Value = "0", Text = "Pre Paid" });
            selectList.Add(new SelectListItem { Value = "1", Text = "Post Paid" });

            return selectList;
        }
        public static List<SelectListItem> GetQuantitySelectListItems()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "0.0", Text = "Cancel" });
            selectList.Add(new SelectListItem { Value = "0.5", Text = "0.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "1.0", Text = "1 Ltr" });
            selectList.Add(new SelectListItem { Value = "1.5", Text = "1.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "2.0", Text = "2 Ltr" });
            selectList.Add(new SelectListItem { Value = "2.5", Text = "2.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "3.0", Text = "3 Ltr" });
            selectList.Add(new SelectListItem { Value = "3.5", Text = "3.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "4.0", Text = "4 Ltr" });
            selectList.Add(new SelectListItem { Value = "4.5", Text = "4.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "5.0", Text = "5 Ltr" });
            selectList.Add(new SelectListItem { Value = "5.5", Text = "5.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "6.0", Text = "6 Ltr" });
            selectList.Add(new SelectListItem { Value = "6.5", Text = "6.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "7.0", Text = "7 Ltr" });
            selectList.Add(new SelectListItem { Value = "7.5", Text = "7.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "8.0", Text = "8 Ltr" });
            selectList.Add(new SelectListItem { Value = "8.5", Text = "8.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "9.0", Text = "9 Ltr" });
            selectList.Add(new SelectListItem { Value = "9.5", Text = "9.5 Ltr" });
            selectList.Add(new SelectListItem { Value = "10.0", Text = "10 Ltr" });
            return selectList;
        }

        public static bool WriteLog(string filePath, string strMessage)
        {
            try
            {
                FileStream objFilestream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
    }
}