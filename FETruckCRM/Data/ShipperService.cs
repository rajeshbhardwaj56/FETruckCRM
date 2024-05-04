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
    public class ShipperService
    {
        SqlConnection con;
        public ShipperService()
        {
            string constring = ConfigurationManager.ConnectionStrings["conn"].ToString();
            con = new SqlConnection(constring);
        }
        public Int64 RegisterShipper(ShipperModel objModel)
        {
            Int64 retVal = 0;

            var olddata = getShipperByShipperIDdt(objModel.ShipperID);
            var editMode = objModel.ShipperID > 0 ? "Edit" : "Add";
            string query = "insupdShipper_new";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperType", objModel.ShipperType);
                cmd.Parameters.AddWithValue("@ShipperID", objModel.ShipperID);
                cmd.Parameters.AddWithValue("@ShipperName", objModel.ShipperName);
                cmd.Parameters.AddWithValue("@StatusInd", objModel.strStatusInd == "1");
                cmd.Parameters.AddWithValue("@LoggedUserID", objModel.CreatedByID);
                cmd.Parameters.AddWithValue("@AddressL1", string.IsNullOrEmpty(objModel.AddressL1)?"": objModel.AddressL1);
                cmd.Parameters.AddWithValue("@AddressL2", string.IsNullOrEmpty(objModel.AddressL2) ? "" : objModel.AddressL2);
                cmd.Parameters.AddWithValue("@AddressL3", string.IsNullOrEmpty(objModel.AddressL3) ? "" : objModel.AddressL3);
                cmd.Parameters.AddWithValue("@CountryID", objModel.strCountryID);
                cmd.Parameters.AddWithValue("@StateID", objModel.strStateID);
                cmd.Parameters.AddWithValue("@City", string.IsNullOrEmpty(objModel.City) ? "" : objModel.City);
                cmd.Parameters.AddWithValue("@ZipCode", string.IsNullOrEmpty(objModel.ZipCode) ? "" : objModel.ZipCode);
                cmd.Parameters.AddWithValue("@ContactName", string.IsNullOrEmpty(objModel.ContactName) ? "" : objModel.ContactName);
                cmd.Parameters.AddWithValue("@ContactEmail", string.IsNullOrEmpty(objModel.ContactEmail) ? "" : objModel.ContactEmail);
                cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrEmpty(objModel.Telephone) ? "" : objModel.Telephone);
                cmd.Parameters.AddWithValue("@TelephoneExt", string.IsNullOrEmpty(objModel.TelephoneExt) ? "" : objModel.TelephoneExt);
                cmd.Parameters.AddWithValue("@TollFree", string.IsNullOrEmpty(objModel.TollFree) ? "" : objModel.TollFree);
                cmd.Parameters.AddWithValue("@Fax", string.IsNullOrEmpty(objModel.Fax) ? "" : objModel.Fax);
                cmd.Parameters.AddWithValue("@DuplicateInfo", objModel.DuplicateInfo);
                cmd.Parameters.AddWithValue("@ShippingHours", objModel.strShippingHours);
                cmd.Parameters.AddWithValue("@Appointments", (string.IsNullOrEmpty(objModel.strAppointments)?(int?) null: Convert.ToInt32(objModel.strAppointments)));
                cmd.Parameters.AddWithValue("@MajorInspectionDirections", string.IsNullOrEmpty(objModel.MajorInspectionDirections) ? "" : objModel.MajorInspectionDirections);
                cmd.Parameters.AddWithValue("@Notes", objModel.Notes);
                cmd.Parameters.AddWithValue("@InternalNotes", objModel.InternalNotes);
                cmd.Parameters.AddWithValue("@CustomerID", objModel.CustomerID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0 && Convert.ToInt64(dt.Rows[0][0]) > 0)
                {
                    retVal = Convert.ToInt64(dt.Rows[0][0]);

                    #region Log
                    var newData = getShipperByShipperIDdt(retVal);
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
                                cmd1.Parameters.AddWithValue("@ModuleName", objModel.ShipperType==1?"Shipper":"Consignee");
                                cmd1.Parameters.AddWithValue("@TableName", "tbShipper");
                                cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.ShipperID);
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
                                    cmd1.Parameters.AddWithValue("@ModuleName", objModel.ShipperType == 1 ? "Shipper" : "Consignee");
                                    cmd1.Parameters.AddWithValue("@TableName", "tbShippert");
                                    cmd1.Parameters.AddWithValue("@PrimaryKey", objModel.ShipperID);
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

        public List<ShipperModel> getAllShippers(string id,long UserID)
        {
            List<ShipperModel> objList = new List<ShipperModel>();
            string query = "getallShipperList";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperType", id);
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
                        ShipperModel objModel = new ShipperModel();
                        //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                        objModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                        objModel.ShipperType = Convert.ToInt32(dr["ShipperType"]);
                        objModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                        objModel.StatusInd = Convert.ToInt32(dr["StatusInd"]);
                       var status = Convert.ToInt32(dr["StatusInd"]);
                        objModel.strStatusInd = status == 1 ? "Active" :"Inactive";
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.AddressL1 = Convert.ToString(dr["AddressL1"]);
                        objModel.Address = Convert.ToString(dr["Address"]);
                        objModel.AddressL2 = Convert.ToString(dr["AddressL2"]);
                        objModel.AddressL3 = Convert.ToString(dr["LastModifiedByID"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.ContactName = Convert.ToString(dr["ContactName"]);
                        objModel.ContactEmail = Convert.ToString(dr["ContactEmail"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                        objModel.Appointments = Convert.ToInt64(dr["Appointments"]);
                        objModel.MajorInspectionDirections = Convert.ToString(dr["MajorInspectionDirections"]);
                        objModel.DuplicateInfo = Convert.ToBoolean(dr["DuplicateInfo"]);
                        objModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                        objModel.strShippingHours = Convert.ToString(dr["ShippingHours"]).ToString();
                        objModel.ShippingHours = Convert.ToString(dr["LastModifiedByID"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                        objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                        objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                       // objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                        objModel.CustomerID = dr["CustomerID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dr["CustomerID"]);






                        objList.Add(objModel);
                    }

                }

            }
            return objList;
        }


        public List<ShipperModel> getAllShippersByPaging(string type, long UserID, int DisplayStart, int DisplayLength, string Search, string SortCol, string Sortdir)
        {
            List<ShipperModel> objList = new List<ShipperModel>();
            string query = "getAllShipperListByPaging";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperType", type);
                cmd.Parameters.AddWithValue("@USerID", UserID);
                cmd.Parameters.AddWithValue("@DisplayStart", DisplayStart);
                cmd.Parameters.AddWithValue("@DisplayLength", DisplayLength);
                cmd.Parameters.AddWithValue("@Search", Search);
                cmd.Parameters.AddWithValue("@SortCol", SortCol);
                cmd.Parameters.AddWithValue("@Sortdir", Sortdir);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
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
                            ShipperModel objModel = new ShipperModel();
                            //objModel.RowNo = Convert.ToInt64(dr["RowNo"]);
                            objModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                            objModel.ShipperType = Convert.ToInt32(dr["ShipperType"]);
                            objModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                            objModel.StatusInd = Convert.ToInt32(dr["StatusInd"]);
                            var status = Convert.ToInt32(dr["StatusInd"]);
                            objModel.strStatusInd = status == 1 ? "Active" : "Inactive";
                            objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            objModel.strCreatedDate = Convert.ToString(dr["strCreatedDate"]);
                            objModel.LastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"]);
                            objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                            objModel.AddressL1 = Convert.ToString(dr["AddressL1"]);
                            objModel.Address = Convert.ToString(dr["Address"]);
                            objModel.AddressL2 = Convert.ToString(dr["AddressL2"]);
                            objModel.AddressL3 = Convert.ToString(dr["LastModifiedByID"]);
                            objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                            objModel.StateID = Convert.ToInt64(dr["StateID"]);
                            objModel.City = Convert.ToString(dr["City"]);
                            objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                            objModel.ContactName = Convert.ToString(dr["ContactName"]);
                            objModel.ContactEmail = Convert.ToString(dr["ContactEmail"]);
                            objModel.Telephone = Convert.ToString(dr["Telephone"]);
                            objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                            objModel.TollFree = Convert.ToString(dr["TollFree"]);
                            objModel.Fax = Convert.ToString(dr["Fax"]);
                            objModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                            objModel.Appointments = Convert.ToInt64(dr["Appointments"]);
                            objModel.MajorInspectionDirections = Convert.ToString(dr["MajorInspectionDirections"]);
                            objModel.DuplicateInfo = Convert.ToBoolean(dr["DuplicateInfo"]);
                            objModel.ShippingHours = Convert.ToString(dr["ShippingHours"]);
                            objModel.strShippingHours = Convert.ToString(dr["ShippingHours"]).ToString();
                            objModel.ShippingHours = Convert.ToString(dr["LastModifiedByID"]);
                            objModel.Notes = Convert.ToString(dr["Notes"]);
                            objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                            objModel.AddedByUser = Convert.ToString(dr["AddedByUser"]);
                            objModel.TeamLead = Convert.ToString(dr["TeamLead"]);
                            objModel.TeamManager = Convert.ToString(dr["TeamManager"]);
                            // objModel.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                            objModel.CustomerID = dr["CustomerID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dr["CustomerID"]);

                            objModel.TotalRecords = Convert.ToInt64(ds.Tables[1].Rows[0][0]);




                            objList.Add(objModel);
                        }

                    }
                }

            }
            return objList;
        }


        public ShipperModel getShipperByShipperID(Int64? ShipperID)
        {
            ShipperModel objModel = new ShipperModel();
            string query = "getShipperByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);

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
                        objModel.ShipperID = Convert.ToInt64(dr["ShipperID"]);
                        objModel.ShipperType = Convert.ToInt32(dr["ShipperType"]);
                        objModel.ShipperName = Convert.ToString(dr["ShipperName"]);
                        objModel.StatusInd = Convert.ToInt32(dr["StatusInd"]);
                        var status = Convert.ToInt32(dr["StatusInd"]);

                        objModel.strStatusInd = Convert.ToString(dr["StatusInd"]);// status == 1 ? "Pending Approval" : (status == 2 ? "Approved" : "Not Approved");

                        objModel.CreatedByID = Convert.ToInt64(dr["CreatedByID"]);
                        objModel.LastModifiedByID = Convert.ToInt64(dr["LastModifiedByID"]);
                        objModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objModel.AddressL1 = Convert.ToString(dr["AddressL1"]);
                        objModel.AddressL2 = Convert.ToString(dr["AddressL2"]);
                        objModel.AddressL3 = Convert.ToString(dr["AddressL3"]);
                        objModel.StateID = Convert.ToInt64(dr["StateID"]);
                        objModel.strStateID = Convert.ToString(dr["StateID"]);
                        objModel.strCountryID = Convert.ToString(dr["CountryID"]);
                        objModel.CountryID = Convert.ToInt64(dr["CountryID"]);
                        objModel.City = Convert.ToString(dr["City"]);
                        objModel.ZipCode = Convert.ToString(dr["ZipCode"]);
                        objModel.ContactName = Convert.ToString(dr["ContactName"]);
                        objModel.ContactEmail = Convert.ToString(dr["ContactEmail"]);
                        objModel.Telephone = Convert.ToString(dr["Telephone"]);
                        objModel.TelephoneExt = Convert.ToString(dr["TelephoneExt"]);
                        objModel.TollFree = Convert.ToString(dr["TollFree"]);
                        objModel.Fax = Convert.ToString(dr["Fax"]);
                        objModel.ShippingHours =  Convert.ToString(dr["ShippingHours"]);
                        objModel.strShippingHours = Convert.ToString(dr["ShippingHours"]);
                        objModel.Appointments = dr["Appointments"]!= DBNull.Value? Convert.ToInt64(dr["Appointments"]):0;
                        objModel.strAppointments = Convert.ToString(dr["Appointments"]);
                        objModel.MajorInspectionDirections = Convert.ToString(dr["MajorInspectionDirections"]);
                        objModel.DuplicateInfo = Convert.ToBoolean(dr["DuplicateInfo"]);
                        objModel.Notes = Convert.ToString(dr["Notes"]);
                        objModel.InternalNotes = Convert.ToString(dr["InternalNotes"]);
                        objModel.CustomerID = dr["CustomerID"]==DBNull.Value?Convert.ToInt64(0): Convert.ToInt64(dr["CustomerID"]);


                    }
                }

            }
            return objModel;
        }


        public DataTable getShipperByShipperIDdt(Int64? ShipperID)
        {
            DataTable dt = new DataTable();
            string query = "getShipperByID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);

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
        public bool CheckShipperName(string ShipperName, long ShipperID)
        {
            bool isvalid = false;
            string query = "proc_CheckShipperByName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperName", ShipperName);
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
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

        public long deleteShipper(Int64 ShipperID, Int64 LoggedinUserId = 0)
        {
            long isSuccess = 0;

            using (SqlCommand cmd = new SqlCommand("deleteShipper", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    isSuccess = Convert.ToInt64(dt.Rows[0][0]);
                    #region Log
                    var newData = getShipperByShipperID(ShipperID);
                    string query1 = "insupdLog";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Connection = con;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@UserID", LoggedinUserId);
                    cmd1.Parameters.AddWithValue("@ModuleName", newData.ShipperType==1? "Shipper":"Consignee");
                    cmd1.Parameters.AddWithValue("@TableName", "tbShipper");
                    cmd1.Parameters.AddWithValue("@PrimaryKey", ShipperID);
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
    }
}