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
    public class CustomerService
    {
        SqlConnection con;
        public CustomerService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        #region Customer
        public Int64 RegisterCustomer(CustomerModel objModel)
        {
            Int64 retVal = 0;
            var olddata = getCustomerByCustomerIDDT(objModel.CustomerID);
            var editMode = objModel.CustomerID > 0 ? "Edit" : "Add";

            string query = "insupdCustomers_New";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", objModel.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", objModel.CustomerName);
                cmd.Parameters.AddWithValue("@CustomerNo", objModel.CustomerNo);
                cmd.Parameters.AddWithValue("@Address", objModel.Address);
                cmd.Parameters.AddWithValue("@Address2", objModel.Address2);
                cmd.Parameters.AddWithValue("@Address3", objModel.Address3);
                cmd.Parameters.AddWithValue("@CountryID", objModel.strCountryID);
                cmd.Parameters.AddWithValue("@StateID", objModel.strStateID);
                cmd.Parameters.AddWithValue("@City", objModel.City);
                cmd.Parameters.AddWithValue("@Zip", objModel.Zip);
                cmd.Parameters.AddWithValue("@ISBillingAddSameAsMailing", objModel.ISBillingAddSameAsMailing);
                cmd.Parameters.AddWithValue("@BillAddress", objModel.BillAddress);
                cmd.Parameters.AddWithValue("@Billingaddress2", objModel.Billingaddress2);
                cmd.Parameters.AddWithValue("@BillingAddress3", objModel.BillingAddress3);
                cmd.Parameters.AddWithValue("@BillingCountryID", objModel.strBillingCountryID);
                cmd.Parameters.AddWithValue("@BillingStateID", objModel.strBillingStateID);
                cmd.Parameters.AddWithValue("@BillingCity", objModel.BillingCity);
                cmd.Parameters.AddWithValue("@BillingZip", objModel.BillingZip);
                cmd.Parameters.AddWithValue("@PrimaryContact", objModel.PrimaryContact);
                cmd.Parameters.AddWithValue("@Telephone", objModel.Telephone);
                cmd.Parameters.AddWithValue("@TelephoneExt", objModel.TelephoneExt);
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@TollFree", objModel.TollFree);
                cmd.Parameters.AddWithValue("@Fax", objModel.Fax);
                cmd.Parameters.AddWithValue("@SecondaryContact", objModel.SecondaryContact);
                cmd.Parameters.AddWithValue("@SecondaryEmail", objModel.SecondaryEmail);
                cmd.Parameters.AddWithValue("@BillingEmail", objModel.BillingEmail);
                cmd.Parameters.AddWithValue("@BillingTelephone", objModel.BillingTelephone);
                cmd.Parameters.AddWithValue("@BillingTelephoneExt", objModel.BillingTelephoneExt);
                cmd.Parameters.AddWithValue("@MCFF", objModel.MCFF);
                cmd.Parameters.AddWithValue("@MCFFType", objModel.MCFFType);
                cmd.Parameters.AddWithValue("@URS", objModel.URS);
                cmd.Parameters.AddWithValue("@ISBlackListed", objModel.ISBlackListed);
                cmd.Parameters.AddWithValue("@ISBroker", objModel.ISBroker);
                cmd.Parameters.AddWithValue("@CurrencySettingID", objModel.strCurrencySettingID);
                cmd.Parameters.AddWithValue("@PaymentTerms", objModel.PaymentTerms);
                cmd.Parameters.AddWithValue("@CreditLimit", objModel.strCreditLimit);
                cmd.Parameters.AddWithValue("@SalesRep", objModel.SalesRep);
                cmd.Parameters.AddWithValue("@FactoringCompany", objModel.strFactoringCompany);
                cmd.Parameters.AddWithValue("@FederalID", objModel.FederalID);
                cmd.Parameters.AddWithValue("@WorkersComp", objModel.WorkersComp);
                cmd.Parameters.AddWithValue("@WebsiteURL", objModel.WebsiteURL);
                cmd.Parameters.AddWithValue("@NumberonInvoice", objModel.NumberonInvoice);
                cmd.Parameters.AddWithValue("@CustomerRate", objModel.CustomerRate);
                cmd.Parameters.AddWithValue("@AddAsShipper", objModel.AddAsShipper);
                cmd.Parameters.AddWithValue("@AddAsConsignee", objModel.AddAsConsignee);
                cmd.Parameters.AddWithValue("@InternalNotes", objModel.InternalNotes);
                cmd.Parameters.AddWithValue("@ShowMilesOnQuote", objModel.ShowMilesOnQuote);
                cmd.Parameters.AddWithValue("@RateType", objModel.RateType);
                cmd.Parameters.AddWithValue("@FSCType", objModel.strFSCType);
                cmd.Parameters.AddWithValue("@FSCRate", objModel.strFSCRate);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@Status", objModel.strStatus);
                cmd.Parameters.AddWithValue("@ApprovalStatus", objModel.strApprovalStatus);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);

                    #region Log
                    var newData = getCustomerByCustomerIDDT(retVal);
                    if (newData.Rows.Count > 0)
                    {
                        DataRow newrow = newData.Rows[0];


                        if (editMode == "Add")
                        {
                            foreach (var col in newData.Columns)
                            {

                                string query1 = "insupdLog";
                                SqlCommand cmd1 = new SqlCommand(query1, con);
                                cmd1.Connection = con;
                                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@UserID", objModel.LastModifiedByID);
                                cmd1.Parameters.AddWithValue("@ModuleName", "Customer");
                                cmd1.Parameters.AddWithValue("@TableName", "tbCustomer");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.CustomerID);
                                cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                cmd1.Parameters.AddWithValue("@OldValue", "");
                                cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                DataTable dt1 = new DataTable();
                                con.Open();
                                sda1.Fill(dt1);
                                con.Close();

                            }
                        }
                        else
                        {
                            DataRow oldrow = olddata.Rows[0];
                            foreach (var col in newData.Columns)
                            {
                                if (Convert.ToString(newrow[col.ToString()]) != Convert.ToString(oldrow[col.ToString()]))
                                {
                                    string query1 = "insupdLog";
                                    SqlCommand cmd1 = new SqlCommand(query1, con);
                                    cmd1.Connection = con;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@UserID", objModel.LastModifiedByID);
                                    cmd1.Parameters.AddWithValue("@ModuleName", "Customer");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbCustomer");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.CustomerID);
                                    cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                    cmd1.Parameters.AddWithValue("@OldValue", Convert.ToString(oldrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    DataTable dt1 = new DataTable();
                                    con.Open();
                                    sda1.Fill(dt1);
                                    con.Close();
                                }
                            }
                        }
                    }
                    #endregion


                }
                else
                {
                    retVal = -1;
                }
            }
            return retVal;
        }

        public List<CustomerModel> getAllCustomers(long UserID)
        {
            List<CustomerModel> objList = new List<CustomerModel>();
            string query = "getAllCustomers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerModel objModel = new CustomerModel();
                        objModel.AssignTo= Convert.ToInt32(dr["AssignTo"]);
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        var status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatus = status == 1 ? "Active" : "Inactive";
                        // objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        //objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.CustomerNo = Convert.ToString(dr["CustomerNo"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        //objModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt32(dr["StateID"]);
                        //objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.Zip = Convert.ToString(dr["Zip"]);
                        //objModel.ISBillingAddSameAsMailing = Convert.ToBoolean(dr["ISBillingAddSameAsMailing"]);
                        //objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        //objModel.Billingaddress2 = Convert.ToString(dr["Billingaddress2"]);
                        //objModel.BillingAddress3 = Convert.ToString(dr["BillingAddress3"]);
                        //objModel.BillingCountryID = Convert.ToInt64(dr["BillingCountryID"]);
                        //objModel.strBillingCountryID = Convert.ToString(dr["BillingCountryID"]);
                        //objModel.BillingStateID = Convert.ToInt64(dr["BillingStateID"]);
                        //objModel.strBillingStateID = Convert.ToString(dr["BillingStateID"]);
                        //objModel.BillingCity = Convert.ToString(dr["BillingCity"]);
                        //objModel.BillingZip = Convert.ToString(dr["BillingZip"]);
                        //objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        //objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        //objModel.Email = Convert.ToString(dr["Email"]);
                        //objModel.TollFree = Convert.ToString(dr["Fax"]);
                        //objModel.Fax = Convert.ToString(dr["CustomerID"]);
                        //objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        //objModel.SecondaryEmail = Convert.ToString(dr["SecondaryEmail"]);
                        //objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        //objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        //objModel.BillingTelephoneExt = Convert.ToString(dr["BillingTelephoneExt"]);
                        //objModel.MCFF = Convert.ToString(dr["MCFF"]);
                        //objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        //objModel.URS = Convert.ToString(dr["URS"]);
                        //objModel.ISBlackListed = Convert.ToBoolean(dr["ISBlackListed"]);
                        //objModel.ISBroker = Convert.ToBoolean(dr["ISBroker"]);
                        //objModel.CurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrencySettingID"]);
                        //objModel.strCurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? "" : Convert.ToString(dr["CurrencySettingID"]);
                        //objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        //objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);
                        //objModel.strCreditLimit = Convert.ToString(dr["CreditLimit"]);
                        //objModel.SalesRep = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["SalesRep"]);
                        //objModel.FactoringCompany = dr["FactoringCompany"] == DBNull.Value ? 0 : Convert.ToInt64(dr["FactoringCompany"]);
                        //objModel.strFactoringCompany = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["FactoringCompany"]);
                        //objModel.FederalID = Convert.ToString(dr["FederalID"]);
                        //objModel.WorkersComp = Convert.ToString(dr["WorkersComp"]);
                        //objModel.WebsiteURL = Convert.ToString(dr["WebsiteURL"]);
                        //objModel.NumberonInvoice = Convert.ToBoolean(dr["NumberonInvoice"]);
                        //objModel.CustomerRate = Convert.ToBoolean(dr["CustomerRate"]);
                        //objModel.AddAsShipper = Convert.ToBoolean(dr["AddAsShipper"]);
                        //objModel.AddAsConsignee = Convert.ToBoolean(dr["AddAsConsignee"]);
                        //objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        //objModel.ShowMilesOnQuote = Convert.ToBoolean(dr["ShowMilesOnQuote"]);
                        //objModel.RateType = dr["RateType"] == DBNull.Value ? "" : Convert.ToString(dr["RateType"]);
                        //objModel.FSCType = Convert.ToInt32(dr["FSCType"]);
                        //objModel.strFSCType = Convert.ToString(dr["FSCType"]);
                        //objModel.FSCRate = dr["FSCRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSCRate"]);
                        //objModel.strFSCRate = dr["FSCRate"] == DBNull.Value ? "" : Convert.ToString(dr["FSCRate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.strCreatedDate = Convert.ToString(dr["strCreatedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);

                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.IsOnHold = Convert.ToBoolean(dr["IsOnHold"]);
                        objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);

                        objModel.TotalCreditLimitUsed = Convert.ToDecimal(dr["TotalCreditLimitUsed"]);
                        objModel.RemainingCreditLimit = Convert.ToDecimal(dr["RemainingCreditLimit"]);

                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }


        public DataTable getAllCustomersExcel(long UserID)
        {
            DataTable dt = new DataTable();

            string query = "getAllCustomers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();



            }
            return dt;
        }

        public List<CustomerModel> getAllCustomers(long UserID, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            List<CustomerModel> objList = new List<CustomerModel>();
            string query = "getAllCustomers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerModel objModel = new CustomerModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        var status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatus = status == 1 ? "Active" : "Inactive";
                        // objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        //objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.CustomerNo = Convert.ToString(dr["CustomerNo"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        //objModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt32(dr["StateID"]);
                        //objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.Zip = Convert.ToString(dr["Zip"]);
                        //objModel.ISBillingAddSameAsMailing = Convert.ToBoolean(dr["ISBillingAddSameAsMailing"]);
                        //objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        //objModel.Billingaddress2 = Convert.ToString(dr["Billingaddress2"]);
                        //objModel.BillingAddress3 = Convert.ToString(dr["BillingAddress3"]);
                        //objModel.BillingCountryID = Convert.ToInt64(dr["BillingCountryID"]);
                        //objModel.strBillingCountryID = Convert.ToString(dr["BillingCountryID"]);
                        //objModel.BillingStateID = Convert.ToInt64(dr["BillingStateID"]);
                        //objModel.strBillingStateID = Convert.ToString(dr["BillingStateID"]);
                        //objModel.BillingCity = Convert.ToString(dr["BillingCity"]);
                        //objModel.BillingZip = Convert.ToString(dr["BillingZip"]);
                        //objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        //objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        //objModel.Email = Convert.ToString(dr["Email"]);
                        //objModel.TollFree = Convert.ToString(dr["Fax"]);
                        //objModel.Fax = Convert.ToString(dr["CustomerID"]);
                        //objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        //objModel.SecondaryEmail = Convert.ToString(dr["SecondaryEmail"]);
                        //objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        //objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        //objModel.BillingTelephoneExt = Convert.ToString(dr["BillingTelephoneExt"]);
                        //objModel.MCFF = Convert.ToString(dr["MCFF"]);
                        //objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        //objModel.URS = Convert.ToString(dr["URS"]);
                        //objModel.ISBlackListed = Convert.ToBoolean(dr["ISBlackListed"]);
                        //objModel.ISBroker = Convert.ToBoolean(dr["ISBroker"]);
                        //objModel.CurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrencySettingID"]);
                        //objModel.strCurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? "" : Convert.ToString(dr["CurrencySettingID"]);
                        //objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        //objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);
                        //objModel.strCreditLimit = Convert.ToString(dr["CreditLimit"]);
                        //objModel.SalesRep = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["SalesRep"]);
                        //objModel.FactoringCompany = dr["FactoringCompany"] == DBNull.Value ? 0 : Convert.ToInt64(dr["FactoringCompany"]);
                        //objModel.strFactoringCompany = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["FactoringCompany"]);
                        //objModel.FederalID = Convert.ToString(dr["FederalID"]);
                        //objModel.WorkersComp = Convert.ToString(dr["WorkersComp"]);
                        //objModel.WebsiteURL = Convert.ToString(dr["WebsiteURL"]);
                        //objModel.NumberonInvoice = Convert.ToBoolean(dr["NumberonInvoice"]);
                        //objModel.CustomerRate = Convert.ToBoolean(dr["CustomerRate"]);
                        //objModel.AddAsShipper = Convert.ToBoolean(dr["AddAsShipper"]);
                        //objModel.AddAsConsignee = Convert.ToBoolean(dr["AddAsConsignee"]);
                        //objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        //objModel.ShowMilesOnQuote = Convert.ToBoolean(dr["ShowMilesOnQuote"]);
                        //objModel.RateType = dr["RateType"] == DBNull.Value ? "" : Convert.ToString(dr["RateType"]);
                        //objModel.FSCType = Convert.ToInt32(dr["FSCType"]);
                        //objModel.strFSCType = Convert.ToString(dr["FSCType"]);
                        //objModel.FSCRate = dr["FSCRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSCRate"]);
                        //objModel.strFSCRate = dr["FSCRate"] == DBNull.Value ? "" : Convert.ToString(dr["FSCRate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.strCreatedDate = Convert.ToString(dr["strCreatedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);

                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.IsOnHold = Convert.ToBoolean(dr["IsOnHold"]);


                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CustomerModel> getAllPendingCustomers(long UserID)
        {
            List<CustomerModel> objList = new List<CustomerModel>();
            string query = "getAllPendingCustomers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerModel objModel = new CustomerModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        var status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatus = status == 1 ? "Active" : "Inactive";
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.CustomerNo = Convert.ToString(dr["CustomerNo"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        objModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt32(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.Zip = Convert.ToString(dr["Zip"]);
                        objModel.ISBillingAddSameAsMailing = Convert.ToBoolean(dr["ISBillingAddSameAsMailing"]);
                        objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        objModel.Billingaddress2 = Convert.ToString(dr["Billingaddress2"]);
                        objModel.BillingAddress3 = Convert.ToString(dr["BillingAddress3"]);
                        objModel.BillingCountryID = Convert.ToInt64(dr["BillingCountryID"]);
                        objModel.strBillingCountryID = Convert.ToString(dr["BillingCountryID"]);
                        objModel.BillingStateID = Convert.ToInt64(dr["BillingStateID"]);
                        objModel.strBillingStateID = Convert.ToString(dr["BillingStateID"]);
                        objModel.BillingCity = Convert.ToString(dr["BillingCity"]);
                        objModel.BillingZip = Convert.ToString(dr["BillingZip"]);
                        objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.TollFree = Convert.ToString(dr["Fax"]);
                        objModel.Fax = Convert.ToString(dr["CustomerID"]);
                        objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        objModel.SecondaryEmail = Convert.ToString(dr["SecondaryEmail"]);
                        objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        objModel.BillingTelephoneExt = Convert.ToString(dr["BillingTelephoneExt"]);
                        objModel.MCFF = Convert.ToString(dr["MCFF"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.ISBlackListed = Convert.ToBoolean(dr["ISBlackListed"]);
                        objModel.ISBroker = Convert.ToBoolean(dr["ISBroker"]);
                        objModel.CurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrencySettingID"]);
                        objModel.strCurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? "" : Convert.ToString(dr["CurrencySettingID"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);
                        objModel.strCreditLimit = Convert.ToString(dr["CreditLimit"]);
                        objModel.SalesRep = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["SalesRep"]);
                        objModel.FactoringCompany = dr["FactoringCompany"] == DBNull.Value ? 0 : Convert.ToInt64(dr["FactoringCompany"]);
                        objModel.strFactoringCompany = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["FactoringCompany"]);
                        objModel.FederalID = Convert.ToString(dr["FederalID"]);
                        objModel.WorkersComp = Convert.ToString(dr["WorkersComp"]);
                        objModel.WebsiteURL = Convert.ToString(dr["WebsiteURL"]);
                        objModel.NumberonInvoice = Convert.ToBoolean(dr["NumberonInvoice"]);
                        objModel.CustomerRate = Convert.ToBoolean(dr["CustomerRate"]);
                        objModel.AddAsShipper = Convert.ToBoolean(dr["AddAsShipper"]);
                        objModel.AddAsConsignee = Convert.ToBoolean(dr["AddAsConsignee"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.ShowMilesOnQuote = Convert.ToBoolean(dr["ShowMilesOnQuote"]);
                        objModel.RateType = dr["RateType"] == DBNull.Value ? "" : Convert.ToString(dr["RateType"]);
                        objModel.FSCType = Convert.ToInt32(dr["FSCType"]);
                        objModel.strFSCType = Convert.ToString(dr["FSCType"]);
                        objModel.FSCRate = dr["FSCRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSCRate"]);
                        objModel.strFSCRate = dr["FSCRate"] == DBNull.Value ? "" : Convert.ToString(dr["FSCRate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);

                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public List<CustomerModel> getCustomerMasterReport(long UserID)
        {
            List<CustomerModel> objList = new List<CustomerModel>();
            string query = "getCustomerMasterReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerModel objModel = new CustomerModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        var status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatus = status == 1 ? "Active" : "Inactive";
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.CustomerNo = Convert.ToString(dr["CustomerNo"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        objModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt32(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.Zip = Convert.ToString(dr["Zip"]);
                        objModel.ISBillingAddSameAsMailing = Convert.ToBoolean(dr["ISBillingAddSameAsMailing"]);
                        objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        objModel.Billingaddress2 = Convert.ToString(dr["Billingaddress2"]);
                        objModel.BillingAddress3 = Convert.ToString(dr["BillingAddress3"]);
                        objModel.BillingCountryID = Convert.ToInt64(dr["BillingCountryID"]);
                        objModel.strBillingCountryID = Convert.ToString(dr["BillingCountryID"]);
                        objModel.BillingStateID = Convert.ToInt64(dr["BillingStateID"]);
                        objModel.strBillingStateID = Convert.ToString(dr["BillingStateID"]);
                        objModel.BillingCity = Convert.ToString(dr["BillingCity"]);
                        objModel.BillingZip = Convert.ToString(dr["BillingZip"]);
                        objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.TollFree = Convert.ToString(dr["Fax"]);
                        objModel.Fax = Convert.ToString(dr["CustomerID"]);
                        objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        objModel.SecondaryEmail = Convert.ToString(dr["SecondaryEmail"]);
                        objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        objModel.BillingTelephoneExt = Convert.ToString(dr["BillingTelephoneExt"]);
                        objModel.MCFF = Convert.ToString(dr["MCFF"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.ISBlackListed = Convert.ToBoolean(dr["ISBlackListed"]);
                        objModel.ISBroker = Convert.ToBoolean(dr["ISBroker"]);
                        objModel.CurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrencySettingID"]);
                        objModel.strCurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? "" : Convert.ToString(dr["CurrencySettingID"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);
                        objModel.strCreditLimit = Convert.ToString(dr["CreditLimit"]);
                        objModel.SalesRep = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["SalesRep"]);
                        objModel.FactoringCompany = dr["FactoringCompany"] == DBNull.Value ? 0 : Convert.ToInt64(dr["FactoringCompany"]);
                        objModel.strFactoringCompany = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["FactoringCompany"]);
                        objModel.FederalID = Convert.ToString(dr["FederalID"]);
                        objModel.WorkersComp = Convert.ToString(dr["WorkersComp"]);
                        objModel.WebsiteURL = Convert.ToString(dr["WebsiteURL"]);
                        objModel.NumberonInvoice = Convert.ToBoolean(dr["NumberonInvoice"]);
                        objModel.CustomerRate = Convert.ToBoolean(dr["CustomerRate"]);
                        objModel.AddAsShipper = Convert.ToBoolean(dr["AddAsShipper"]);
                        objModel.AddAsConsignee = Convert.ToBoolean(dr["AddAsConsignee"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.ShowMilesOnQuote = Convert.ToBoolean(dr["ShowMilesOnQuote"]);
                        objModel.RateType = dr["RateType"] == DBNull.Value ? "" : Convert.ToString(dr["RateType"]);
                        objModel.FSCType = Convert.ToInt32(dr["FSCType"]);
                        objModel.strFSCType = Convert.ToString(dr["FSCType"]);
                        objModel.FSCRate = dr["FSCRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSCRate"]);
                        objModel.strFSCRate = dr["FSCRate"] == DBNull.Value ? "" : Convert.ToString(dr["FSCRate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);

                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objModel.ApprovedBy = Convert.ToString(dr["ApprovedByUser"]);
                        objModel.ApprovalDate = Convert.ToDateTime(dr["ApprovalDate"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }

        public DataTable getCustomerMasterReportdt(long UserID)
        {
            DataTable dt = new DataTable();

            string query = "getCustomerMasterReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@USerID", UserID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {


                }

            }
            return dt;
        }
        public List<SelectListItem> getAllFactoringCompany()
        {
            var selectList = new List<SelectListItem>();
            string query = "proc_getAllFactoringCompanyList";
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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["FCID"]).ToString(), Text = Convert.ToString(dr["Name"]) });
                    }

                }

            }
            return selectList;
        }

        public CustomerModel getCustomerByCustomerID(Int64? CustomerID)
        {
            CustomerModel objModel = new CustomerModel();
            string query = "getCustomerByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

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
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = Convert.ToString(dr["ApprovalStatus"]);
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        objModel.strStatus = Convert.ToString(dr["Status"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerName = Convert.ToString(dr["CustomerName"]);
                        objModel.CustomerNo = Convert.ToString(dr["CustomerNo"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.Address2 = Convert.ToString(dr["Address2"]);
                        objModel.Address3 = Convert.ToString(dr["Address3"]);
                        objModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt32(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.Zip = Convert.ToString(dr["Zip"]);
                        objModel.ISBillingAddSameAsMailing = Convert.ToBoolean(dr["ISBillingAddSameAsMailing"]);
                        objModel.BillAddress = Convert.ToString(dr["BillAddress"]);
                        objModel.Billingaddress2 = Convert.ToString(dr["Billingaddress2"]);
                        objModel.BillingAddress3 = Convert.ToString(dr["BillingAddress3"]);
                        objModel.BillingCountryID = Convert.ToInt64(dr["BillingCountryID"]);
                        objModel.strBillingCountryID = Convert.ToString(dr["BillingCountryID"]);
                        objModel.BillingStateID = Convert.ToInt64(dr["BillingStateID"]);
                        objModel.strBillingStateID = Convert.ToString(dr["BillingStateID"]);
                        objModel.BillingCity = Convert.ToString(dr["BillingCity"]);
                        objModel.BillingZip = Convert.ToString(dr["BillingZip"]);
                        objModel.PrimaryContact = Convert.ToString(dr["PrimaryContact"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.SecondaryContact = Convert.ToString(dr["SecondaryContact"]);
                        objModel.SecondaryEmail = Convert.ToString(dr["SecondaryEmail"]);
                        objModel.BillingEmail = Convert.ToString(dr["BillingEmail"]);
                        objModel.BillingTelephone = Convert.ToString(dr["BillingTelephone"]);
                        objModel.BillingTelephoneExt = Convert.ToString(dr["BillingTelephoneExt"]);
                        objModel.MCFF = Convert.ToString(dr["MCFF"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.ISBlackListed = Convert.ToBoolean(dr["ISBlackListed"]);
                        objModel.ISBroker = Convert.ToBoolean(dr["ISBroker"]);
                        objModel.CurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurrencySettingID"]);
                        objModel.strCurrencySettingID = dr["CurrencySettingID"] == DBNull.Value ? "" : Convert.ToString(dr["CurrencySettingID"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.CreditLimit = Convert.ToDecimal(dr["CreditLimit"]);
                        objModel.strCreditLimit = Convert.ToString(dr["CreditLimit"]);
                        objModel.SalesRep = Convert.ToString(dr["SalesRep"]);
                        objModel.FactoringCompany = dr["FactoringCompany"] == DBNull.Value ? 0 : Convert.ToInt64(dr["FactoringCompany"]);
                        objModel.strFactoringCompany = dr["FactoringCompany"] == DBNull.Value ? "" : Convert.ToString(dr["FactoringCompany"]);// Convert.ToString(dr["FactoringCompany"]);
                        objModel.FederalID = Convert.ToString(dr["FederalID"]);
                        objModel.WorkersComp = Convert.ToString(dr["WorkersComp"]);
                        objModel.WebsiteURL = Convert.ToString(dr["WebsiteURL"]);
                        objModel.NumberonInvoice = Convert.ToBoolean(dr["NumberonInvoice"]);
                        objModel.CustomerRate = Convert.ToBoolean(dr["CustomerRate"]);
                        objModel.AddAsShipper = Convert.ToBoolean(dr["AddAsShipper"]);
                        objModel.AddAsConsignee = Convert.ToBoolean(dr["AddAsConsignee"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.ShowMilesOnQuote = Convert.ToBoolean(dr["ShowMilesOnQuote"]);
                        objModel.RateType = Convert.ToString(dr["RateType"]);
                        objModel.FSCType = Convert.ToInt32(dr["FSCType"]);
                        objModel.strFSCType = Convert.ToString(dr["FSCType"]);
                        objModel.FSCRate = dr["FSCRate"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FSCRate"]);// Convert.ToDecimal(dr["FSCRate"]);
                        objModel.strFSCRate = dr["FSCRate"] == DBNull.Value ? "" : Convert.ToString(dr["FSCRate"]);// Convert.ToString(dr["FSCRate"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.Status = Convert.ToInt32(dr["Status"]);
                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);


                    }
                }

            }
            return objModel;
        }

        public DataTable getCustomerByCustomerIDDT(Int64 CustomerID)
        {
            DataTable dt = new DataTable();

            string query = "getCustomerByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                }

            }
            return dt;
        }

        public bool CheckMCFF(string MCFFNo, long CustomerID)
        {
            bool isvalid = false;
            string query = "proc_CheckCustomerByMCFF";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MCFFNo", MCFFNo);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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
        public bool CheckCustomerName(CustomerModel model)
        {
            bool isvalid = false;
            string query = "proc_CheckCustomerByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", model.CustomerName.TrimStart(' ').TrimEnd(' '));
                //cmd.Parameters.AddWithValue("@telephone", model.BillingTelephone);
                //cmd.Parameters.AddWithValue("@Address", model.Address);
                //cmd.Parameters.AddWithValue("@Address2", model.Address2);
                //cmd.Parameters.AddWithValue("@Address3", model.Address3);
                //cmd.Parameters.AddWithValue("@CountryID", model.CountryID);
                //cmd.Parameters.AddWithValue("@StateID", model.StateID);
                //cmd.Parameters.AddWithValue("@City", model.City);
                //cmd.Parameters.AddWithValue("@Zip", model.Zip);
                cmd.Parameters.AddWithValue("@CustomerID", model.CustomerID);
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

        public long deleteCustomer(Int64 CustomerID, Int64 LoggedinUserId = 0)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteCustomer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);

                    #region Log
                    string query1 = "insupdLog";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Connection = con;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@UserID", LoggedinUserId);
                    cmd1.Parameters.AddWithValue("@ModuleName", "Customer");
                    cmd1.Parameters.AddWithValue("@TableName", "tbCustomer");
                    cmd1.Parameters.AddWithValue("@PrimaryKey", CustomerID);
                    cmd1.Parameters.AddWithValue("@ColumnName", "IsDeletedInd");
                    cmd1.Parameters.AddWithValue("@OldValue", "False");
                    cmd1.Parameters.AddWithValue("@NewValue", "True");
                    cmd1.Parameters.AddWithValue("@EditMode", "Delete");
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    con.Open();
                    sda1.Fill(dt1);
                    con.Close();
                    #endregion
                }
            }
            return isSuccess;
        }

        public long OnHoldCustomer(Int64 CustomerID, bool IsOnHold, Int64 LoggedinUserId = 0)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("OnHoldCustomer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@IsOnHold", IsOnHold);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);
                    #region Log
                    string query1 = "insupdLog";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Connection = con;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@UserID", LoggedinUserId);
                    cmd1.Parameters.AddWithValue("@ModuleName", "Customer");
                    cmd1.Parameters.AddWithValue("@TableName", "tbCustomer");
                    cmd1.Parameters.AddWithValue("@PrimaryKey", CustomerID);
                    cmd1.Parameters.AddWithValue("@ColumnName", "IsOnHold");
                    cmd1.Parameters.AddWithValue("@OldValue", !IsOnHold == true ? "Yes" : "No");
                    cmd1.Parameters.AddWithValue("@NewValue", IsOnHold == true ? "Yes" : "No");
                    cmd1.Parameters.AddWithValue("@EditMode", "Edit");
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    con.Open();
                    sda1.Fill(dt1);
                    con.Close();
                    #endregion
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

        public static List<SelectListItem> getSaleRepList(long UserID)
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            SqlConnection con = new SqlConnection(constring);
            var selectList = new List<SelectListItem>();
            string query = "getAllUsersByUID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@USerID", UserID);

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
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["UserID"]).ToString(), Text = Convert.ToString(dr["UName"]) });
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
                else
                {

                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                }
            }
            return selectList;
        }

        //public static List<SelectListItem> getCountryList()
        //{
        //    string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
        //    SqlConnection con = new SqlConnection(constring);
        //    var selectList = new List<SelectListItem>();
        //    string query = "GetAllCountry";
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.Connection = con;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        con.Open();
        //        sda.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["CountryID"]).ToString(), Text = Convert.ToString(dr["CountryName"]) });
        //            }
        //        }
        //    }
        //    return selectList;
        //}

        //public static List<SelectListItem> getStateList(long CountryID)
        //{
        //    string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
        //    SqlConnection con = new SqlConnection(constring);
        //    var selectList = new List<SelectListItem>();
        //    string query = "GetAllStates";
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.Connection = con;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@CountryID", CountryID);

        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        con.Open();
        //        sda.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["StateID"]).ToString(), Text = Convert.ToString(dr["StateName"]) });
        //            }
        //        }
        //        else
        //        {

        //            selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

        //        }
        //    }
        //    return selectList;
        //} 
        #endregion
        public Int64 RegisterCustomerNotifications(CustomerNotificationsModel objModel)
        {
            Int64 retVal = 0;


            string query = "insupdCustomertNotification";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerNotificationID", objModel.CustomerNotificationID);
                cmd.Parameters.AddWithValue("@CustomerID", objModel.CustomerID);
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@SendCopyToInitiatingUser", objModel.SendCopyToInitiatingUser);
                cmd.Parameters.AddWithValue("@ENDispatched", objModel.ENDispatched);
                cmd.Parameters.AddWithValue("@ENLoading", objModel.ENLoading);
                cmd.Parameters.AddWithValue("@ENOnRoute", objModel.ENOnRoute);
                cmd.Parameters.AddWithValue("@ENUnloading", objModel.ENUnloading);
                cmd.Parameters.AddWithValue("@ENInYard", objModel.ENInYard);
                cmd.Parameters.AddWithValue("@ENDelivered", objModel.ENDelivered);
                cmd.Parameters.AddWithValue("@ENCompleted", objModel.ENCompleted);
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

        public List<CustomerNotificationsModel> getAllCustomerNotifications(long CustomerID)
        {
            List<CustomerNotificationsModel> objList = new List<CustomerNotificationsModel>();
            string query = "GetAllCustomerNotificationByCustomerID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerNotificationsModel objModel = new CustomerNotificationsModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerNotificationID = Convert.ToInt64(dr["CustomerNotificationID"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.SendCopyToInitiatingUser = Convert.ToBoolean(dr["SendCopyToInitiatingUser"]);
                        objModel.ENDispatched = Convert.ToBoolean(dr["ENDispatched"]);
                        objModel.ENLoading = Convert.ToBoolean(dr["ENLoading"]);
                        objModel.ENOnRoute = Convert.ToBoolean(dr["ENOnRoute"]);
                        objModel.ENUnloading = Convert.ToBoolean(dr["ENUnloading"]);
                        objModel.ENInYard = Convert.ToBoolean(dr["ENInYard"]);
                        objModel.ENDelivered = Convert.ToBoolean(dr["ENDelivered"]);
                        objModel.ENCompleted = Convert.ToBoolean(dr["ENCompleted"]);
                        //objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                        objModel.strSendCopyToInitiatingUser = Convert.ToString(dr["strSendCopyToInitiatingUser"]);
                        objModel.strENDispatched = Convert.ToString(dr["strENDispatched"]);
                        objModel.strENLoading = Convert.ToString(dr["strENLoading"]);
                        objModel.strENOnRoute = Convert.ToString(dr["strENOnRoute"]);
                        objModel.strENUnloading = Convert.ToString(dr["strENUnloading"]);
                        objModel.strENInYard = Convert.ToString(dr["strENInYard"]);
                        objModel.strENDelivered = Convert.ToString(dr["strENDelivered"]);
                        objModel.strENCompleted = Convert.ToString(dr["strENCompleted"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public CustomerNotificationsModel getCustomerNotificationByID(Int64? CustomerNotificationID)
        {
            CustomerNotificationsModel objModel = new CustomerNotificationsModel();
            string query = "getCustomerNotificationByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerNotificationID", CustomerNotificationID);

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
                        objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerNotificationID = Convert.ToInt64(dr["CustomerNotificationID"]);
                        objModel.Email = Convert.ToString(dr["Email"]);
                        objModel.SendCopyToInitiatingUser = Convert.ToBoolean(dr["SendCopyToInitiatingUser"]);
                        objModel.ENDispatched = Convert.ToBoolean(dr["ENDispatched"]);
                        objModel.ENLoading = Convert.ToBoolean(dr["ENLoading"]);
                        objModel.ENOnRoute = Convert.ToBoolean(dr["ENOnRoute"]);
                        objModel.ENUnloading = Convert.ToBoolean(dr["ENUnloading"]);
                        objModel.ENInYard = Convert.ToBoolean(dr["ENInYard"]);
                        objModel.ENDelivered = Convert.ToBoolean(dr["ENDelivered"]);
                        objModel.ENCompleted = Convert.ToBoolean(dr["ENCompleted"]);


                    }
                }

            }
            return objModel;
        }

        public long deleteCustomerNotificationsByID(Int64 CustomerNotificationID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteCustomerNotification", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerNotificationID", CustomerNotificationID);
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