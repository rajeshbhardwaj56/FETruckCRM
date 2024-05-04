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
using Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static iTextSharp.text.pdf.PdfDiv;

namespace FETruckCRM.Data
{
    public class CareerService
    {
        SqlConnection con;
        public CareerService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterCareer(CareerModel objModel)
        {
            Int64 retVal = 0;
            var olddata = getCareerByCareerIDdt(objModel.CareerID);
            var editMode = objModel.CareerID > 0 ? "Edit" : "Add";

            string query = "insupdCareer";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", objModel.CareerID);
                cmd.Parameters.AddWithValue("@LoadTypeID", objModel.strLoadTypeID);
                cmd.Parameters.AddWithValue("@CareerName", string.IsNullOrEmpty(objModel.CareerName) ? "" : objModel.CareerName);
                cmd.Parameters.AddWithValue("@Username", string.IsNullOrEmpty(objModel.Username) ? "" : objModel.Username);
                cmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(objModel.Password) ? "" : objModel.Password);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(objModel.Address) ? "" : objModel.Address);
                cmd.Parameters.AddWithValue("@AddressLine2", string.IsNullOrEmpty(objModel.AddressLine2) ? "" : objModel.AddressLine2);
                cmd.Parameters.AddWithValue("@AddressLine3", string.IsNullOrEmpty(objModel.AddressLine3) ? "" : objModel.AddressLine3);
                cmd.Parameters.AddWithValue("@CountryID", string.IsNullOrEmpty(objModel.strCountryID) ? "" : objModel.strCountryID);
                cmd.Parameters.AddWithValue("@StateID", string.IsNullOrEmpty(objModel.strStateID) ? "" : objModel.strStateID);
                cmd.Parameters.AddWithValue("@City", string.IsNullOrEmpty(objModel.City) ? "" : objModel.City);
                cmd.Parameters.AddWithValue("@ZipCode", string.IsNullOrEmpty(objModel.ZipCode) ? "" : objModel.ZipCode);
                cmd.Parameters.AddWithValue("@ContactName", string.IsNullOrEmpty(objModel.ContactName) ? "" : objModel.ContactName);
                cmd.Parameters.AddWithValue("@EmailId", string.IsNullOrEmpty(objModel.EmailId) ? "" : objModel.EmailId);
                cmd.Parameters.AddWithValue("@StatusInd", objModel.strStatusInd);
                cmd.Parameters.AddWithValue("@ApprovalStatus", objModel.strApprovalStatus);
                cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrEmpty(objModel.Telephone) ? "" : objModel.Telephone);
                cmd.Parameters.AddWithValue("@TelephoneExt", string.IsNullOrEmpty(objModel.TelephoneExt) ? "" : objModel.TelephoneExt);
                cmd.Parameters.AddWithValue("@TollFree", string.IsNullOrEmpty(objModel.TollFree) ? "" : objModel.TollFree);
                cmd.Parameters.AddWithValue("@Fax", string.IsNullOrEmpty(objModel.Fax) ? "" : objModel.Fax);
                cmd.Parameters.AddWithValue("@PaymentTerms", objModel.PaymentTerms);
                cmd.Parameters.AddWithValue("@TaxID", string.IsNullOrEmpty(objModel.TaxID) ? "" : objModel.TaxID);
                cmd.Parameters.AddWithValue("@MCFFType", string.IsNullOrEmpty(objModel.MCFFType) ? "" : objModel.MCFFType);
                cmd.Parameters.AddWithValue("@MCFFNo", string.IsNullOrEmpty(objModel.MCFFNo) ? "" : objModel.MCFFNo);
                cmd.Parameters.AddWithValue("@URS", string.IsNullOrEmpty(objModel.URS) ? "" : objModel.URS);
                cmd.Parameters.AddWithValue("@DOT", string.IsNullOrEmpty(objModel.DOT) ? "" : objModel.DOT);
                cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(objModel.Notes) ? "" : objModel.Notes);
                cmd.Parameters.AddWithValue("@Blacklisted", objModel.IsCareerAsBlacklisted);
                cmd.Parameters.AddWithValue("@Corporation", objModel.Corporation);
                cmd.Parameters.AddWithValue("@FactoringCompany", objModel.FactoringCompany);
                cmd.Parameters.AddWithValue("@LiabilityCompany", string.IsNullOrEmpty(objModel.LiabilityCompany) ? "" : objModel.LiabilityCompany);
                cmd.Parameters.AddWithValue("@PolicyNo", string.IsNullOrEmpty(objModel.PolicyNo) ? "" : objModel.PolicyNo);
                cmd.Parameters.AddWithValue("@ExpiryDate", string.IsNullOrEmpty(objModel.strExpiryDate) ? "" : objModel.strExpiryDate);
                cmd.Parameters.AddWithValue("@LibCompanyTelephone", string.IsNullOrEmpty(objModel.LibCompanyTelephone) ? "" : objModel.LibCompanyTelephone);
                cmd.Parameters.AddWithValue("@LibCompanyTelephoneExt", string.IsNullOrEmpty(objModel.LibCompanyTelephoneExt) ? "" : objModel.LibCompanyTelephoneExt);
                cmd.Parameters.AddWithValue("@LibCompContact", string.IsNullOrEmpty(objModel.LibCompContact) ? "" : objModel.LibCompContact);
                cmd.Parameters.AddWithValue("@Liability", string.IsNullOrEmpty(objModel.strLiability) ? Convert.ToDecimal(0) : Convert.ToDecimal(objModel.strLiability));
                cmd.Parameters.AddWithValue("@LibCompNotes", string.IsNullOrEmpty(objModel.LibCompNotes) ? "" : objModel.LibCompNotes);
                cmd.Parameters.AddWithValue("@IsAICompSameAsLiabInsurance", objModel.IsAICompSameAsLiabInsurance);
                cmd.Parameters.AddWithValue("@AutoInsuranceCompany", string.IsNullOrEmpty(objModel.AutoInsuranceCompany) ? "" : objModel.AutoInsuranceCompany);
                cmd.Parameters.AddWithValue("@AICPolicyNo", string.IsNullOrEmpty(objModel.AICPolicyNo) ? "" : objModel.AICPolicyNo);
                cmd.Parameters.AddWithValue("@AICExpiryDate", string.IsNullOrEmpty(objModel.strAICExpiryDate) ? "" : objModel.strAICExpiryDate);
                cmd.Parameters.AddWithValue("@AICTelephoneNo", string.IsNullOrEmpty(objModel.AICTelephoneNo) ? "" : objModel.AICTelephoneNo);
                cmd.Parameters.AddWithValue("@AICTelephoneExt", string.IsNullOrEmpty(objModel.AICTelephoneExt) ? "" : objModel.AICTelephoneExt);
                cmd.Parameters.AddWithValue("@AICContact", string.IsNullOrEmpty(objModel.AICContact) ? "" : objModel.AICContact);
                cmd.Parameters.AddWithValue("@AICLiability", string.IsNullOrEmpty(objModel.strAICLiability) ? Convert.ToDecimal(0) : Convert.ToDecimal(objModel.strAICLiability));
                cmd.Parameters.AddWithValue("@AICNotes", string.IsNullOrEmpty(objModel.AICNotes) ? "" : objModel.AICNotes);
                cmd.Parameters.AddWithValue("@IsCCSameAsLiab", objModel.IsCCSameAsLiab);
                cmd.Parameters.AddWithValue("@CargoCompany", string.IsNullOrEmpty(objModel.CargoCompany) ? "" : objModel.CargoCompany);
                cmd.Parameters.AddWithValue("@CCPolicyNo", string.IsNullOrEmpty(objModel.CCPolicyNo) ? "" : objModel.CCPolicyNo);
                cmd.Parameters.AddWithValue("@CCExpiryDate", string.IsNullOrEmpty(objModel.strCCExpiryDate) ? "" : objModel.strCCExpiryDate);
                cmd.Parameters.AddWithValue("@CCTelephoneNo", string.IsNullOrEmpty(objModel.CCTelephoneNo) ? "" : objModel.CCTelephoneNo);
                cmd.Parameters.AddWithValue("@CCtelephoneExt", string.IsNullOrEmpty(objModel.CCtelephoneExt) ? "" : objModel.CCtelephoneExt);
                cmd.Parameters.AddWithValue("@CCContact", string.IsNullOrEmpty(objModel.CCContact) ? "" : objModel.CCContact);
                cmd.Parameters.AddWithValue("@CCCargoIns", string.IsNullOrEmpty(objModel.strCCCargoIns) ? Convert.ToDecimal(0) : Convert.ToDecimal(objModel.strCCCargoIns));
                cmd.Parameters.AddWithValue("@CCNotes", string.IsNullOrEmpty(objModel.CCNotes) ? "" : objModel.CCNotes);
                cmd.Parameters.AddWithValue("@CCWSIB", string.IsNullOrEmpty(objModel.CCWSIB) ? "" : objModel.CCWSIB);
                cmd.Parameters.AddWithValue("@FMCSAInsuranceCompany", string.IsNullOrEmpty(objModel.FMCSAInsuranceCompany) ? "" : objModel.FMCSAInsuranceCompany);
                cmd.Parameters.AddWithValue("@FICPolicyNo", string.IsNullOrEmpty(objModel.FICPolicyNo) ? "" : objModel.FICPolicyNo);
                cmd.Parameters.AddWithValue("@FICExpDate", string.IsNullOrEmpty(objModel.strFICExpDate) ? "" : objModel.strFICExpDate);
                cmd.Parameters.AddWithValue("@FICType", string.IsNullOrEmpty(objModel.FICType) ? "" : objModel.FICType);
                cmd.Parameters.AddWithValue("@FICCoverage", string.IsNullOrEmpty(objModel.strFICCoverage) == true ? Convert.ToDecimal(0) : Convert.ToDecimal(objModel.strFICCoverage));
                cmd.Parameters.AddWithValue("@FICTelephone", string.IsNullOrEmpty(objModel.FICTelephone) ? "" : objModel.FICTelephone);
                cmd.Parameters.AddWithValue("@FICAMBestRating", objModel.FICAMBestRating);
                cmd.Parameters.AddWithValue("@AccountingPrimaryName", string.IsNullOrEmpty(objModel.AccountingPrimaryName) ? "" : objModel.AccountingPrimaryName);
                cmd.Parameters.AddWithValue("@AccountingPrimaryTelephone", string.IsNullOrEmpty(objModel.AccountingPrimaryTelephone) ? "" : objModel.AccountingPrimaryTelephone);
                cmd.Parameters.AddWithValue("@AccountingPrimaryEmail", string.IsNullOrEmpty(objModel.AccountingPrimaryEmail) ? "" : objModel.AccountingPrimaryEmail);
                cmd.Parameters.AddWithValue("@AccountingSecondaryName", string.IsNullOrEmpty(objModel.AccountingSecondaryName) ? "" : objModel.AccountingSecondaryName);
                cmd.Parameters.AddWithValue("@AccountingSecondaryTelephone", string.IsNullOrEmpty(objModel.AccountingSecondaryTelephone) ? "" : objModel.AccountingSecondaryTelephone);
                cmd.Parameters.AddWithValue("@AccountingSecondaryEmail", string.IsNullOrEmpty(objModel.AccountingSecondaryEmail) ? "" : objModel.AccountingSecondaryEmail);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@SizeOfFleet", objModel.SizeOfFleet);
                cmd.Parameters.AddWithValue("@CareerNotes", string.IsNullOrEmpty(objModel.CareerNotes) ? "" : objModel.CareerNotes);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);

                    if (objModel.EquipmentList.Count > 0)
                    {
                        deleteCareerEquipmentType(retVal);

                        foreach (var item in objModel.EquipmentList)
                        {
                            item.CareerID = retVal;
                            AddCareerEquipmentType(item);
                        }
                    }

                    #region Log
                    var newData = getCareerByCareerIDdt(retVal);
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
                                cmd1.Parameters.AddWithValue("@UserID", objModel.LastModifyByID);
                                cmd1.Parameters.AddWithValue("@ModuleName", "Carrier");
                                cmd1.Parameters.AddWithValue("@TableName", "tbCareer");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.CareerID);
                                cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                cmd1.Parameters.AddWithValue("@OldValue", "");
                                cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                System.Data.DataTable dt1 = new System.Data.DataTable();
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
                                    cmd1.Parameters.AddWithValue("@UserID", objModel.LastModifyByID);
                                    cmd1.Parameters.AddWithValue("@ModuleName", "Carrier");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbCareer");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.CareerID);
                                    cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                    cmd1.Parameters.AddWithValue("@OldValue", Convert.ToString(oldrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    System.Data.DataTable dt1 = new System.Data.DataTable();
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

        //public List<CareerModel> getAllCareers(long UserID)
        //{
        //    List<CareerModel> objList = new List<CareerModel>();
        //    string query = "getAllCareer";
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.Connection = con;
        //        cmd.Parameters.AddWithValue("@UserID", UserID);

        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        con.Open();
        //        sda.Fill(dt);
        //        con.Close();

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                CareerModel objModel = new CareerModel();


        //                objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
        //                objModel.CareerName = Convert.ToString(dr["CareerName"]);
        //                objModel.Username = Convert.ToString(dr["Username"]);
        //                objModel.Password = Convert.ToString(dr["Password"]);
        //                objModel.Address = Convert.ToString(dr["Address"]);
        //                objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
        //                objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
        //                objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
        //                objModel.StateID = Convert.ToInt64(dr["StateID"]);
        //                objModel.StateName = Convert.ToString(dr["StateName"]);
        //                objModel.City = Convert.ToString(dr["City"]);
        //                objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
        //                objModel.Telephone = Convert.ToString(dr["Telephone"]);
        //                objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
        //                objModel.TollFree = Convert.ToString(dr["TollFree"]);
        //                objModel.Fax = Convert.ToString(dr["Fax"]);
        //                objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
        //                objModel.TaxID = Convert.ToString(dr["TaxID"]);
        //                objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
        //                objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
        //                objModel.URS = Convert.ToString(dr["URS"]);
        //                objModel.DOT = Convert.ToString(dr["DOT"]);
        //                objModel.Notes = Convert.ToString(dr["Notes"]);
        //                objModel.IsCareerAsBlacklisted = Convert.ToBoolean(dr["Blacklisted"]);
        //                objModel.Corporation = Convert.ToBoolean(dr["Corporation"]);
        //                objModel.FactoringCompany = Convert.ToInt32(dr["FactoringCompany"]);
        //                objModel.LiabilityCompany = Convert.ToString(dr["LiabilityCompany"]);
        //                objModel.PolicyNo = Convert.ToString(dr["PolicyNo"]);
        //                objModel.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);
        //                objModel.LibCompanyTelephone = Convert.ToString(dr["LibCompanyTelephone"]);
        //                objModel.LibCompanyTelephoneExt = Convert.ToString(dr["LibCompanyTelephoneExt"]);
        //                objModel.LibCompContact = Convert.ToString(dr["LibCompContact"]);
        //                objModel.strLiability = Convert.ToString(dr["Liability"]);
        //                objModel.LibCompNotes = Convert.ToString(dr["LibCompNotes"]);
        //                objModel.IsAICompSameAsLiabInsurance = Convert.ToBoolean(dr["IsAICompSameAsLiabInsurance"]);
        //                objModel.AutoInsuranceCompany = Convert.ToString(dr["AutoInsuranceCompany"]);
        //                objModel.AICPolicyNo = Convert.ToString(dr["AICPolicyNo"]);
        //                objModel.AICExpiryDate = Convert.ToDateTime(dr["AICExpiryDate"]);
        //                objModel.AICTelephoneNo = Convert.ToString(dr["AICTelephoneNo"]);
        //                objModel.AICTelephoneExt = Convert.ToString(dr["AICTelephoneExt"]);
        //                objModel.AICContact = Convert.ToString(dr["AICContact"]);
        //                objModel.strAICLiability = Convert.ToString(dr["AICLiability"]);
        //                objModel.AICNotes = Convert.ToString(dr["AICNotes"]);
        //                objModel.IsCCSameAsLiab = Convert.ToBoolean(dr["IsCCSameAsLiab"]);
        //                objModel.CargoCompany = Convert.ToString(dr["CargoCompany"]);
        //                objModel.CCPolicyNo = Convert.ToString(dr["CCPolicyNo"]);
        //                objModel.CCExpiryDate = Convert.ToDateTime(dr["CCExpiryDate"]);
        //                objModel.CCTelephoneNo = Convert.ToString(dr["CCTelephoneNo"]);
        //                objModel.CCtelephoneExt = Convert.ToString(dr["CCtelephoneExt"]);
        //                objModel.CCContact = Convert.ToString(dr["CCContact"]);
        //                objModel.strCCCargoIns = Convert.ToString(dr["CCCargoIns"]);
        //                objModel.CCNotes = Convert.ToString(dr["CCNotes"]);
        //                objModel.CCWSIB = Convert.ToString(dr["CCWSIB"]);
        //                objModel.FMCSAInsuranceCompany = Convert.ToString(dr["FMCSAInsuranceCompany"]);
        //                objModel.FICPolicyNo = Convert.ToString(dr["FICPolicyNo"]);
        //                objModel.FICExpDate = Convert.ToDateTime(dr["FICExpDate"]);
        //                objModel.FICType = Convert.ToString(dr["FICType"]);
        //                objModel.strFICCoverage = Convert.ToString(dr["FICCoverage"]);
        //                objModel.FICTelephone = Convert.ToString(dr["FICTelephone"]);
        //                objModel.FICAMBestRating = Convert.ToInt32(dr["FICAMBestRating"]);
        //                objModel.AccountingPrimaryName = Convert.ToString(dr["AccountingPrimaryName"]);
        //                objModel.AccountingPrimaryTelephone = Convert.ToString(dr["AccountingPrimaryTelephone"]);
        //                objModel.AccountingPrimaryEmail = Convert.ToString(dr["AccountingPrimaryEmail"]);
        //                objModel.AccountingSecondaryName = Convert.ToString(dr["AccountingSecondaryName"]);
        //                objModel.AccountingSecondaryTelephone = Convert.ToString(dr["AccountingSecondaryTelephone"]);
        //                objModel.AccountingSecondaryEmail = Convert.ToString(dr["AccountingSecondaryEmail"]);
        //                objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
        //                objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
        //                objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
        //                objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);

        //                var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
        //                objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");

        //                objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
        //                objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
        //                objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
        //                objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
        //                objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
        //                objModel.TeamManager = Convert.ToString(dr["TeamManager"]);













        //                objList.Add(objModel);
        //            }

        //        }

        //    }
        //    return objList;
        //}
        public List<CareerListModel> getAllCareers(long UserID)
        {
            List<CareerListModel> objList = new List<CareerListModel>();
            string query = "getAllCareer";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CareerListModel objModel = new CareerListModel();


                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.LoadType = Convert.ToString(dr["Loadtype"]);
                        
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                        objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);

                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");

                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
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

        public System.Data.DataTable getAllCareersExcel(long UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "getAllCareerForExcel";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
        public List<CareerListModel> getAllCareersByPaging(long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<CareerListModel> objList = new List<CareerListModel>();
            string query = "getAllCareerByPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            CareerListModel objModel = new CareerListModel();
                            objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                            objModel.CareerName = Convert.ToString(dr["CareerName"]);
                            objModel.Address = Convert.ToString(dr["Address"]);
                            objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                            objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
                            objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                            objModel.StateID = Convert.ToInt64(dr["StateID"]);
                            objModel.StateName = Convert.ToString(dr["StateName"]);
                            objModel.City = Convert.ToString(dr["City"]);
                            objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                            objModel.Telephone = Convert.ToString(dr["Telephone"]);
                            objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
                           objModel.LoadType = Convert.ToString(dr["Loadtype"]);
                            objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                            objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            objModel.strCreatedDate = Convert.ToString(dr["strCreatedDate"]);
                            objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
                            objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);

                            var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                            objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");

                            objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                            objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
                            objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                            objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                            objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                            objModel.TeamManager = Convert.ToString(dr["TeamManager"]);


                            objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);










                            objList.Add(objModel);
                        }

                    }
                }
            }
            return objList;
        }
        public List<CareerModel> getAllPendingCareers(long UserID)
        {
            List<CareerModel> objList = new List<CareerModel>();
            string query = "getAllPendingCareer";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CareerModel objModel = new CareerModel();


                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.Username = Convert.ToString(dr["Username"]);
                        objModel.Password = Convert.ToString(dr["Password"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                        objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.TaxID = Convert.ToString(dr["TaxID"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.DOT = Convert.ToString(dr["DOT"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.IsCareerAsBlacklisted = Convert.ToBoolean(dr["Blacklisted"]);
                        objModel.Corporation = Convert.ToBoolean(dr["Corporation"]);
                        objModel.FactoringCompany = Convert.ToInt32(dr["FactoringCompany"]);
                        objModel.LiabilityCompany = Convert.ToString(dr["LiabilityCompany"]);
                        objModel.PolicyNo = Convert.ToString(dr["PolicyNo"]);
                        objModel.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);
                        objModel.LibCompanyTelephone = Convert.ToString(dr["LibCompanyTelephone"]);
                        objModel.LibCompanyTelephoneExt = Convert.ToString(dr["LibCompanyTelephoneExt"]);
                        objModel.LibCompContact = Convert.ToString(dr["LibCompContact"]);
                        objModel.strLiability = Convert.ToString(dr["Liability"]);
                        objModel.LibCompNotes = Convert.ToString(dr["LibCompNotes"]);
                        objModel.IsAICompSameAsLiabInsurance = Convert.ToBoolean(dr["IsAICompSameAsLiabInsurance"]);
                        objModel.AutoInsuranceCompany = Convert.ToString(dr["AutoInsuranceCompany"]);
                        objModel.AICPolicyNo = Convert.ToString(dr["AICPolicyNo"]);
                        objModel.AICExpiryDate = Convert.ToDateTime(dr["AICExpiryDate"]);
                        objModel.AICTelephoneNo = Convert.ToString(dr["AICTelephoneNo"]);
                        objModel.AICTelephoneExt = Convert.ToString(dr["AICTelephoneExt"]);
                        objModel.AICContact = Convert.ToString(dr["AICContact"]);
                        objModel.strAICLiability = Convert.ToString(dr["AICLiability"]);
                        objModel.AICNotes = Convert.ToString(dr["AICNotes"]);
                        objModel.IsCCSameAsLiab = Convert.ToBoolean(dr["IsCCSameAsLiab"]);
                        objModel.CargoCompany = Convert.ToString(dr["CargoCompany"]);
                        objModel.CCPolicyNo = Convert.ToString(dr["CCPolicyNo"]);
                        objModel.CCExpiryDate = Convert.ToDateTime(dr["CCExpiryDate"]);
                        objModel.CCTelephoneNo = Convert.ToString(dr["CCTelephoneNo"]);
                        objModel.CCtelephoneExt = Convert.ToString(dr["CCtelephoneExt"]);
                        objModel.CCContact = Convert.ToString(dr["CCContact"]);
                        objModel.strCCCargoIns = Convert.ToString(dr["CCCargoIns"]);
                        objModel.CCNotes = Convert.ToString(dr["CCNotes"]);
                        objModel.CCWSIB = Convert.ToString(dr["CCWSIB"]);
                        objModel.FMCSAInsuranceCompany = Convert.ToString(dr["FMCSAInsuranceCompany"]);
                        objModel.FICPolicyNo = Convert.ToString(dr["FICPolicyNo"]);
                        objModel.FICExpDate = Convert.ToDateTime(dr["FICExpDate"]);
                        objModel.FICType = Convert.ToString(dr["FICType"]);
                        objModel.strFICCoverage = Convert.ToString(dr["FICCoverage"]);
                        objModel.FICTelephone = Convert.ToString(dr["FICTelephone"]);
                        objModel.FICAMBestRating = Convert.ToInt32(dr["FICAMBestRating"]);
                        objModel.AccountingPrimaryName = Convert.ToString(dr["AccountingPrimaryName"]);
                        objModel.AccountingPrimaryTelephone = Convert.ToString(dr["AccountingPrimaryTelephone"]);
                        objModel.AccountingPrimaryEmail = Convert.ToString(dr["AccountingPrimaryEmail"]);
                        objModel.AccountingSecondaryName = Convert.ToString(dr["AccountingSecondaryName"]);
                        objModel.AccountingSecondaryTelephone = Convert.ToString(dr["AccountingSecondaryTelephone"]);
                        objModel.AccountingSecondaryEmail = Convert.ToString(dr["AccountingSecondaryEmail"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);

                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");

                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
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

        public List<CareerModel> getCarrierMasterReport(long UserID)
        {
            List<CareerModel> objList = new List<CareerModel>();
            string query = "getCarrierMasterReport";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                //cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CareerModel objModel = new CareerModel();


                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.Username = Convert.ToString(dr["Username"]);
                        objModel.Password = Convert.ToString(dr["Password"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                        objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.StateName = Convert.ToString(dr["StateName"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.TaxID = Convert.ToString(dr["TaxID"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.DOT = Convert.ToString(dr["DOT"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.IsCareerAsBlacklisted = Convert.ToBoolean(dr["Blacklisted"]);
                        objModel.Corporation = Convert.ToBoolean(dr["Corporation"]);
                        objModel.FactoringCompany = Convert.ToInt32(dr["FactoringCompany"]);
                        objModel.LiabilityCompany = Convert.ToString(dr["LiabilityCompany"]);
                        objModel.PolicyNo = Convert.ToString(dr["PolicyNo"]);
                        objModel.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);
                        objModel.LibCompanyTelephone = Convert.ToString(dr["LibCompanyTelephone"]);
                        objModel.LibCompanyTelephoneExt = Convert.ToString(dr["LibCompanyTelephoneExt"]);
                        objModel.LibCompContact = Convert.ToString(dr["LibCompContact"]);
                        objModel.strLiability = Convert.ToString(dr["Liability"]);
                        objModel.LibCompNotes = Convert.ToString(dr["LibCompNotes"]);
                        objModel.IsAICompSameAsLiabInsurance = Convert.ToBoolean(dr["IsAICompSameAsLiabInsurance"]);
                        objModel.AutoInsuranceCompany = Convert.ToString(dr["AutoInsuranceCompany"]);
                        objModel.AICPolicyNo = Convert.ToString(dr["AICPolicyNo"]);
                        objModel.AICExpiryDate = Convert.ToDateTime(dr["AICExpiryDate"]);
                        objModel.AICTelephoneNo = Convert.ToString(dr["AICTelephoneNo"]);
                        objModel.AICTelephoneExt = Convert.ToString(dr["AICTelephoneExt"]);
                        objModel.AICContact = Convert.ToString(dr["AICContact"]);
                        objModel.strAICLiability = Convert.ToString(dr["AICLiability"]);
                        objModel.AICNotes = Convert.ToString(dr["AICNotes"]);
                        objModel.IsCCSameAsLiab = Convert.ToBoolean(dr["IsCCSameAsLiab"]);
                        objModel.CargoCompany = Convert.ToString(dr["CargoCompany"]);
                        objModel.CCPolicyNo = Convert.ToString(dr["CCPolicyNo"]);
                        objModel.CCExpiryDate = Convert.ToDateTime(dr["CCExpiryDate"]);
                        objModel.CCTelephoneNo = Convert.ToString(dr["CCTelephoneNo"]);
                        objModel.CCtelephoneExt = Convert.ToString(dr["CCtelephoneExt"]);
                        objModel.CCContact = Convert.ToString(dr["CCContact"]);
                        objModel.strCCCargoIns = Convert.ToString(dr["CCCargoIns"]);
                        objModel.CCNotes = Convert.ToString(dr["CCNotes"]);
                        objModel.CCWSIB = Convert.ToString(dr["CCWSIB"]);
                        objModel.FMCSAInsuranceCompany = Convert.ToString(dr["FMCSAInsuranceCompany"]);
                        objModel.FICPolicyNo = Convert.ToString(dr["FICPolicyNo"]);
                        objModel.FICExpDate = Convert.ToDateTime(dr["FICExpDate"]);
                        objModel.FICType = Convert.ToString(dr["FICType"]);
                        objModel.strFICCoverage = Convert.ToString(dr["FICCoverage"]);
                        objModel.FICTelephone = Convert.ToString(dr["FICTelephone"]);
                        objModel.FICAMBestRating = Convert.ToInt32(dr["FICAMBestRating"]);
                        objModel.AccountingPrimaryName = Convert.ToString(dr["AccountingPrimaryName"]);
                        objModel.AccountingPrimaryTelephone = Convert.ToString(dr["AccountingPrimaryTelephone"]);
                        objModel.AccountingPrimaryEmail = Convert.ToString(dr["AccountingPrimaryEmail"]);
                        objModel.AccountingSecondaryName = Convert.ToString(dr["AccountingSecondaryName"]);
                        objModel.AccountingSecondaryTelephone = Convert.ToString(dr["AccountingSecondaryTelephone"]);
                        objModel.AccountingSecondaryEmail = Convert.ToString(dr["AccountingSecondaryEmail"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);

                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending Approval" : (approvestatus == 2 ? "Approved" : "Not Approved");

                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "Active" : "Inactive";
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
        public System.Data.DataTable getCarrierMasterReportdt(long UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "getCarrierMasterReportxls";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                //cmd.Parameters.AddWithValue("@UserID", UserID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

        public Int64 AddCareerEquipmentType(CareerEquipmentTypeModel objModel)
        {
            Int64 retVal = 0;


            string query = "insupdCareerEquipmentTypes";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", objModel.CareerID);
                cmd.Parameters.AddWithValue("@EquipmentTypeID", objModel.EquipmentTypeID);
                cmd.Parameters.AddWithValue("@Quantity", objModel.Quantity);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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

        public bool CheckMCFF(string MCFFNo, long CarrierID)
        {
            bool isvalid = false;
            string query = "proc_CheckCarrierByMCFF";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MCFFNo", MCFFNo);
                cmd.Parameters.AddWithValue("@CarrierID", CarrierID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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

        public List<CareerEquipmentTypeModel> getAllCareersEquipments(long CareerID)
        {
            List<CareerEquipmentTypeModel> objList = new List<CareerEquipmentTypeModel>();
            string query = "getCareerEquipment";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", CareerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CareerEquipmentTypeModel objModel = new CareerEquipmentTypeModel();


                        objModel.CareerID = dr["CareerID"] == System.DBNull.Value ? 0 : Convert.ToInt64(dr["CareerID"]);
                        objModel.CareerEquipmentTypeID = dr["CareerEquipmentTypeID"] == System.DBNull.Value ? 0 : Convert.ToInt64(dr["CareerEquipmentTypeID"]);
                        objModel.EquipmentTypeID = Convert.ToInt64(dr["EquipmentTypeID"]);
                        objModel.EquipmentTypeName = Convert.ToString(dr["EquipmentTypeName"]);
                        objModel.Quantity = dr["Quantity"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["Quantity"]);
                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.Isdeleted = Convert.ToBoolean(dr["Isdeleted"]);
                        objModel.IsChecked = ((dr["Quantity"] == System.DBNull.Value || Convert.ToInt32(dr["Quantity"]) == 0) ? false : true);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
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
                System.Data.DataTable dt = new System.Data.DataTable();
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
        public List<SelectListItem> getAllLoadType()
        {
            var selectList = new List<SelectListItem>();
            string query = "proc_getAllLoadTypeList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["LTypeID"]).ToString(), Text = Convert.ToString(dr["Loadtype"]) });
                    }

                }

            }
            return selectList;
        }

        public List<SelectListItem> getAllEquipmentTypeList()
        {
            var selectList = new List<SelectListItem>();
            string query = "proc_getAllEquipmentTypeList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    selectList.Add(new SelectListItem { Value = "", Text = "Please Select" });

                    foreach (DataRow dr in dt.Rows)
                    {
                        selectList.Add(new SelectListItem { Value = Convert.ToInt64(dr["EquipmentTypeID"]).ToString(), Text = Convert.ToString(dr["EquipmentTypeName"]) });
                    }

                }

            }
            return selectList;
        }
        public CareerModel getCareerByCareerID(Int64? CareerID)
        {
            CareerModel objModel = new CareerModel();
            string query = "getCareerByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", CareerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    System.Data.DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        objModel.CareerID = Convert.ToInt64(dr["CareerID"]);
                        objModel.CareerName = Convert.ToString(dr["CareerName"]);
                        objModel.LoadTypeID = dr["LoadTypeID"]!= DBNull.Value? Convert.ToInt32(dr["LoadTypeID"]):0;
                        objModel.strLoadTypeID = Convert.ToString(dr["LoadTypeID"]);
                        objModel.Username = Convert.ToString(dr["Username"]);
                        objModel.Password = Convert.ToString(dr["Password"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                        objModel.AddressLine3 = Convert.ToString(dr["AddressLine3"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
                        objModel.TaxID = Convert.ToString(dr["TaxID"]);
                        objModel.MCFFType = Convert.ToString(dr["MCFFType"]);
                        objModel.MCFFNo = Convert.ToString(dr["MCFFNo"]);
                        objModel.URS = Convert.ToString(dr["URS"]);
                        objModel.DOT = Convert.ToString(dr["DOT"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.IsCareerAsBlacklisted = Convert.ToBoolean(dr["Blacklisted"]);
                        objModel.Corporation = Convert.ToBoolean(dr["Corporation"]);
                        objModel.FactoringCompany = Convert.ToInt32(dr["FactoringCompany"]);
                        objModel.LiabilityCompany = Convert.ToString(dr["LiabilityCompany"]);
                        objModel.PolicyNo = Convert.ToString(dr["PolicyNo"]);
                        objModel.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);
                        objModel.strExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]).ToShortDateString();
                        objModel.LibCompanyTelephone = Convert.ToString(dr["LibCompanyTelephone"]);
                        objModel.LibCompanyTelephoneExt = Convert.ToString(dr["LibCompanyTelephoneExt"]);
                        objModel.LibCompContact = Convert.ToString(dr["LibCompContact"]);
                        objModel.strLiability = Convert.ToString(dr["Liability"]);
                        objModel.LibCompNotes = Convert.ToString(dr["LibCompNotes"]);
                        objModel.IsAICompSameAsLiabInsurance = Convert.ToBoolean(dr["IsAICompSameAsLiabInsurance"]);
                        objModel.AutoInsuranceCompany = Convert.ToString(dr["AutoInsuranceCompany"]);
                        objModel.AICPolicyNo = Convert.ToString(dr["AICPolicyNo"]);
                        objModel.AICExpiryDate = Convert.ToDateTime(dr["AICExpiryDate"]);
                        objModel.strAICExpiryDate = Convert.ToDateTime(dr["AICExpiryDate"]).ToShortDateString();
                        objModel.AICTelephoneNo = Convert.ToString(dr["AICTelephoneNo"]);
                        objModel.AICTelephoneExt = Convert.ToString(dr["AICTelephoneExt"]);
                        objModel.AICContact = Convert.ToString(dr["AICContact"]);
                        objModel.strAICLiability = Convert.ToString(dr["AICLiability"]);
                        objModel.AICNotes = Convert.ToString(dr["AICNotes"]);
                        objModel.IsCCSameAsLiab = Convert.ToBoolean(dr["IsCCSameAsLiab"]);
                        objModel.CargoCompany = Convert.ToString(dr["CargoCompany"]);
                        objModel.CCPolicyNo = Convert.ToString(dr["CCPolicyNo"]);
                        objModel.CCExpiryDate = Convert.ToDateTime(dr["CCExpiryDate"]);
                        objModel.strCCExpiryDate = Convert.ToDateTime(dr["CCExpiryDate"]).ToShortDateString();
                        objModel.CCTelephoneNo = Convert.ToString(dr["CCTelephoneNo"]);
                        objModel.CCtelephoneExt = Convert.ToString(dr["CCtelephoneExt"]);
                        objModel.CCContact = Convert.ToString(dr["CCContact"]);
                        objModel.strCCCargoIns = Convert.ToString(dr["CCCargoIns"]);
                        objModel.CCNotes = Convert.ToString(dr["CCNotes"]);
                        objModel.CCWSIB = Convert.ToString(dr["CCWSIB"]);
                        objModel.FMCSAInsuranceCompany = Convert.ToString(dr["FMCSAInsuranceCompany"]);
                        objModel.FICPolicyNo = Convert.ToString(dr["FICPolicyNo"]);
                        objModel.FICExpDate = Convert.ToDateTime(dr["FICExpDate"]);
                        objModel.strFICExpDate = Convert.ToDateTime(dr["FICExpDate"]).ToShortDateString();
                        objModel.FICType = Convert.ToString(dr["FICType"]);
                        objModel.strFICCoverage = Convert.ToString(dr["FICCoverage"]);
                        objModel.FICTelephone = Convert.ToString(dr["FICTelephone"]);
                        objModel.FICAMBestRating = Convert.ToInt32(dr["FICAMBestRating"]);
                        objModel.AccountingPrimaryName = Convert.ToString(dr["AccountingPrimaryName"]);
                        objModel.AccountingPrimaryTelephone = Convert.ToString(dr["AccountingPrimaryTelephone"]);
                        objModel.AccountingPrimaryEmail = Convert.ToString(dr["AccountingPrimaryEmail"]);
                        objModel.AccountingSecondaryName = Convert.ToString(dr["AccountingSecondaryName"]);
                        objModel.AccountingSecondaryTelephone = Convert.ToString(dr["AccountingSecondaryTelephone"]);
                        objModel.AccountingSecondaryEmail = Convert.ToString(dr["AccountingSecondaryEmail"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifyByID = Convert.ToInt64(dr["LastModifyByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.StatusInd = Convert.ToBoolean(dr["StatusInd"]);
                        objModel.strStatusInd = Convert.ToBoolean(dr["StatusInd"]) == true ? "1" : "0";
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.SizeOfFleet = Convert.ToInt32(dr["SizeOfFleet"]);
                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);


                    }
                }

            }
            return objModel;
        }

        public System.Data.DataTable getCareerByCareerIDdt(Int64? CareerID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "getCareerByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", CareerID);

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
        public bool CheckCareerName(string CareerName, long CareerID)
        {
            bool isvalid = false;
            string query = "proc_CheckCareerByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerName", CareerName);
                cmd.Parameters.AddWithValue("@CareerID", CareerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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

        public long deleteCareer(Int64 CareerID, Int64 LoggedinUserId = 0)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteCareer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", CareerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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
                    cmd1.Parameters.AddWithValue("@ModuleName", "Carrier");
                    cmd1.Parameters.AddWithValue("@TableName", "tbCareer");
                    cmd1.Parameters.AddWithValue("@PrimaryKey", CareerID);
                    cmd1.Parameters.AddWithValue("@ColumnName", "IsDeletedInd");
                    cmd1.Parameters.AddWithValue("@OldValue", "False");
                    cmd1.Parameters.AddWithValue("@NewValue", "True");
                    cmd1.Parameters.AddWithValue("@EditMode", "Delete");
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    System.Data.DataTable dt1 = new System.Data.DataTable();
                    con.Open();
                    sda1.Fill(dt1);
                    con.Close();
                    #endregion
                }
            }
            return isSuccess;
        }
        public long deleteCareerEquipmentType(Int64 CareerID)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteCareerEquipments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CareerID", CareerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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
                System.Data.DataTable dt = new System.Data.DataTable();
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
                System.Data.DataTable dt = new System.Data.DataTable();
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

        #region MCCheck
        public List<MCCheckModel> getAllMCCheck(long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<MCCheckModel> objList = new List<MCCheckModel>();
            string query = "GetAllMCCheck";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MCCheckModel objModel = new MCCheckModel();
                        objModel.MCCheckID = Convert.ToInt64(dr["MCCheckID"]);
                        objModel.MCNumber = Convert.ToInt64(dr["MCNumber"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.CarrierName = Convert.ToString(dr["CarrierName"]);
                        objModel.CommodityValue = Convert.ToInt64(dr["CommodityValue"]);
                        objModel.CommodityType = Convert.ToString(dr["CommodityType"]);
                        objModel.EquipmentType = Convert.ToString(dr["EquipmentType"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.strCreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedBy"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        public Int64 AddMCCheck(MCCheckModel objModel)
        {
            Int64 retVal = 0;
            var olddata = getMCCheckByMCCheckIDdt(objModel.MCCheckID);
            var editMode = objModel.MCCheckID > 0 ? "Edit" : "Add";

            string query = "InsUpdMCCheck";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MCCheckID", objModel.MCCheckID);
                cmd.Parameters.AddWithValue("@MCNumber", objModel.MCNumber);
                cmd.Parameters.AddWithValue("@CarrierName", string.IsNullOrEmpty(objModel.CarrierName) ? "" : objModel.CarrierName);
                cmd.Parameters.AddWithValue("@CommodityValue", objModel.CommodityValue);
                cmd.Parameters.AddWithValue("@CommodityType", string.IsNullOrEmpty(objModel.CommodityType) ? "" : objModel.CommodityType);
                cmd.Parameters.AddWithValue("@EquipmentType", string.IsNullOrEmpty(objModel.EquipmentType) ? "" : objModel.EquipmentType);
                cmd.Parameters.AddWithValue("@ApprovalStatus", string.IsNullOrEmpty(objModel.strApprovalStatus) ? "1" : objModel.strApprovalStatus);
                cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(objModel.Notes) ? "" : objModel.Notes);
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);

                    #region Save Mc Check Docs
                    #region Delete Existing Docs
                   
                    SqlCommand cmd2 = new SqlCommand("deleteMCCheckDocs", con);
                    cmd2.Connection = con;
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@MCCheckID", retVal);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    System.Data.DataTable dt2 = new System.Data.DataTable();
                    con.Open();
                    sda2.Fill(dt2);
                    con.Close();
                    #endregion
                    foreach (var items in objModel.MCCheckDocsList)
                    {
                        string query1 = "insupdMCCheckDocs";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Connection = con;
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@MCCheckID", retVal);
                        cmd1.Parameters.AddWithValue("@MCCheckDocName", items.MCCheckDocName);
                        cmd1.Parameters.AddWithValue("@MCCheckDocURL", items.MCCheckDocURL);
                        cmd1.Parameters.AddWithValue("@CreatedByID", objModel.CreatedByID);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        con.Open();
                        sda1.Fill(dt1);
                        con.Close();
                    }

                    #endregion
                    #region Log
                    var newData = getMCCheckByMCCheckIDdt(retVal);
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
                                cmd1.Parameters.AddWithValue("@ModuleName", "MC Check");
                                cmd1.Parameters.AddWithValue("@TableName", "tbCareer");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.MCCheckID);
                                cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                cmd1.Parameters.AddWithValue("@OldValue", "");
                                cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                System.Data.DataTable dt1 = new System.Data.DataTable();
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
                                    cmd1.Parameters.AddWithValue("@ModuleName", "Carrier");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbCareer");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.MCCheckID);
                                    cmd1.Parameters.AddWithValue("@ColumnName", col.ToString());
                                    cmd1.Parameters.AddWithValue("@OldValue", Convert.ToString(oldrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@NewValue", Convert.ToString(newrow[col.ToString()]));
                                    cmd1.Parameters.AddWithValue("@EditMode", editMode);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    System.Data.DataTable dt1 = new System.Data.DataTable();
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
        public System.Data.DataTable getMCCheckByMCCheckIDdt(Int64? MCCheckID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "getMCCheckBYID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MCCheckID", MCCheckID);

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
        public MCCheckModel MCCheckByMCCheckID(Int64? MCCheckID)
        {
            MCCheckModel objModel = new MCCheckModel();
            string query = "getMCCheckBYID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MCCheckID", MCCheckID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    System.Data.DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        objModel.MCCheckID = Convert.ToInt64(dr["MCCheckID"]);
                        objModel.CarrierName = Convert.ToString(dr["CarrierName"]);
                        objModel.MCNumber = Convert.ToInt64(dr["MCNumber"]);
                        objModel.EquipmentType = Convert.ToString(dr["EquipmentType"]);
                        objModel.CommodityType = Convert.ToString(dr["CommodityType"]);
                        objModel.CommodityValue = Convert.ToInt64(dr["CommodityValue"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.IsDeletedInd = Convert.ToBoolean(dr["IsDeletedInd"]);
                    }

                    if (ds.Tables.Count > 1)
                    {
                        System.Data.DataTable dt2= ds.Tables[1];
                        objModel.MCCheckDocsList = new List<MCCheckDocModel>();
                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt2.Rows)
                        {
                              
                                MCCheckDocModel objMCDocModel = new MCCheckDocModel();
                                objMCDocModel.MCCheckID = Convert.ToInt64(dr["MCCheckID"]);
                                objMCDocModel.MCCheckDocName = Convert.ToString(dr["MCCheckDocName"]);
                                objMCDocModel.MCCheckDocURL = Convert.ToString(dr["MCCheckDocURL"]);
                                objModel.MCCheckDocsList.Add(objMCDocModel);
                            }
                        }


                    }
                }

            }
            return objModel;
        }


        public List<MCCheckModel> getAllPendingMCCheck(long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<MCCheckModel> objList = new List<MCCheckModel>();
            string query = "GetAllPendingMCCheck";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USerID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MCCheckModel objModel = new MCCheckModel();
                        objModel.MCCheckID = Convert.ToInt64(dr["MCCheckID"]);
                        objModel.MCNumber = Convert.ToInt64(dr["MCNumber"]);
                        objModel.ApprovalStatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        var approvestatus = Convert.ToInt32(dr["ApprovalStatus"]);
                        objModel.strApprovalStatus = approvestatus == 1 ? "Pending" : (approvestatus == 2 ? "Approved" : "Not Approved");
                        objModel.CarrierName = Convert.ToString(dr["CarrierName"]);
                        objModel.CommodityValue = Convert.ToInt64(dr["CommodityValue"]);
                        objModel.CommodityType = Convert.ToString(dr["CommodityType"]);
                        objModel.EquipmentType = Convert.ToString(dr["EquipmentType"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.strCreatedDate = Convert.ToString(dr["CreatedDate"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedBy"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }
        #endregion

    }
}