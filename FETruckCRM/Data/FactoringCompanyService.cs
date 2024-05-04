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
    public class FactoringCompanyService
    {
        SqlConnection con;
        public FactoringCompanyService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterFactoringCompany(FactoringCompanyModel objModel)
        {
            Int64 retVal = 0;


            string query = "insupdFactoringCompany";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FCID", objModel.FCID);
                cmd.Parameters.AddWithValue("@Name", objModel.Name);
                cmd.Parameters.AddWithValue("@RemittanceEmail", objModel.RemittanceEmail);
                cmd.Parameters.AddWithValue("@Status", objModel.strStatusInd);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(objModel.Address)?"": objModel.Address);
                cmd.Parameters.AddWithValue("@Address2", string.IsNullOrEmpty(objModel.Address2) ? "" : objModel.Address2);
                cmd.Parameters.AddWithValue("@Address3", string.IsNullOrEmpty(objModel.Address3) ? "" : objModel.Address3);
                cmd.Parameters.AddWithValue("@CountryID", objModel.strCountryID);
                cmd.Parameters.AddWithValue("@StateID", objModel.strStateID);
                cmd.Parameters.AddWithValue("@City", string.IsNullOrEmpty(objModel.City) ? "" : objModel.City);
                cmd.Parameters.AddWithValue("@ZipCode", string.IsNullOrEmpty(objModel.ZipCode) ? "" : objModel.ZipCode);
                cmd.Parameters.AddWithValue("@PrimaryContact", string.IsNullOrEmpty(objModel.ContactName) ? "" : objModel.ContactName);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(objModel.ContactEmail) ? "" : objModel.ContactEmail);
                cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrEmpty(objModel.Telephone) ? "" : objModel.Telephone);
                cmd.Parameters.AddWithValue("@TelephoneExtn", string.IsNullOrEmpty(objModel.TelephoneExt) ? "" : objModel.TelephoneExt);
                cmd.Parameters.AddWithValue("@TollFree", string.IsNullOrEmpty(objModel.TollFree) ? "" : objModel.TollFree);
                cmd.Parameters.AddWithValue("@Fax", string.IsNullOrEmpty(objModel.Fax) ? "" : objModel.Fax);
                cmd.Parameters.AddWithValue("@SecondaryContact", string.IsNullOrEmpty(objModel.SecondaryContact) ? "" : objModel.SecondaryContact);
                cmd.Parameters.AddWithValue("@SecTelephone", string.IsNullOrEmpty(objModel.SecTelephone) ? "" : objModel.SecTelephone);
                cmd.Parameters.AddWithValue("@SecTelephoneExtn", string.IsNullOrEmpty(objModel.SecTelephoneExtn) ? "" : objModel.SecTelephoneExtn);
                cmd.Parameters.AddWithValue("@CurrencySettings", objModel.CurrencySettings);
                cmd.Parameters.AddWithValue("@PaymentTerms",  objModel.PaymentTerms);
                cmd.Parameters.AddWithValue("@TaxID", string.IsNullOrEmpty(objModel.TaxID) ? "" : objModel.TaxID);
                cmd.Parameters.AddWithValue("@InternalNotes", objModel.InternalNotes);
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

        public List<FactoringCompanyModel> getAllFactoringCompanys(long UserID)
        {
            List<FactoringCompanyModel> objList = new List<FactoringCompanyModel>();
            string query = "proc_getallFactoringCompany";
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
                        FactoringCompanyModel objModel = new FactoringCompanyModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.FCID = Convert.ToInt64(dr["FCID"]);
                        objModel.Name = Convert.ToString(dr["Name"]);
                        objModel.RemittanceEmail = Convert.ToString(dr["RemittanceEmail"]);
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                       var status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatusInd = status == 1 ? "Active" :"Inactive";
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.ContactName = Convert.ToString(dr["PrimaryContact"]);
                        objModel.ContactEmail = Convert.ToString(dr["Email"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExtn"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);

                    



                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

        public FactoringCompanyModel getFactoringCompanyByFactoringCompanyID(Int64? FactoringCompanyID)
        {
            FactoringCompanyModel objModel = new FactoringCompanyModel();
            string query = "getFactoringCompanyByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FCID", FactoringCompanyID);

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
                        objModel.FCID = Convert.ToInt64(dr["FCID"]);
                        objModel.Name = Convert.ToString(dr["Name"]);
                        objModel.RemittanceEmail = Convert.ToString(dr["RemittanceEmail"]);
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        var status = Convert.ToInt32(dr["Status"]);

                        objModel.strStatusInd = Convert.ToString(dr["Status"]);//== 1 ? "Pending Approval" : (status == 2 ? "Approved" : "Not Approved");
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExtn = Convert.ToString(dr["TelephoneExtn"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        objModel.SecTelephone = Convert.ToString(dr["SecTelephone"]).ToString();
                        objModel.SecTelephoneExtn = Convert.ToString(dr["SecTelephoneExtn"]);
                        objModel.TaxID = Convert.ToString(dr["TaxID"]);
                        objModel.CurrencySettings = Convert.ToInt32(dr["CurrencySettings"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);


                    }
                }

            }
            return objModel;
        }
        public bool CheckFactoringCompanyName(string FactoringCompanyName, long FactoringCompanyID)
        {
            bool isvalid = false;
            string query = "proc_CheckFactoringCompanyByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FactoringCompanyName", FactoringCompanyName);
                cmd.Parameters.AddWithValue("@FactoringCompanyID", FactoringCompanyID);
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

        public long deleteFactoringCompany(Int64 FactoringCompanyID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("proc_deleteFactoringCompany", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FCID", FactoringCompanyID);
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
                else { 
                
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                }
            }
            return selectList;
        }
        public static List<SelectListItem> getPaymentTermsList()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "GetAllPaymentTerms";
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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["PaymentTermsID"]).ToString(), Text = Convert.ToString(dr["PaymentTerm"]) });
                    }
                }
                else
                {

                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                }
            }
            return selectList;
        }
    }
}