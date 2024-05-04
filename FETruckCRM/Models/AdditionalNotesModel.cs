using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class AdditionalNotesModel
    {
        public Int64 LoadAdditionalNotesID { get; set; }
        public Int64 LoadID { get; set; }
        public string LoadNo { get; set; }
        public string DriverPayNotes { get; set; }
        public string DPNAppearOnReport { get; set; }
        public string InvoiceNotes { get; set; }
        public string INAppearOnInvoice { get; set; }
        public string InvoiceDescription { get; set; }
        public string DeletedRefusalNotes { get; set; }
        public string RecInvoiceNo { get; set; }
        public string RecInvoiceDate { get; set; }
        public decimal RecAmount { get; set; }
        public string RecCustomer { get; set; }
        public string RecContact { get; set; }

        public string RecTelephone { get; set; }
        public string RecTelephoneExt { get; set; }
        public string RecTollFree { get; set; }
        public string RecFax { get; set; }
        public string RecNotes { get; set; }
        public string InternalNotes { get; set; }
        public string ConDispatcherName { get; set; }
        public string ConDispatcherPhone { get; set; }
        public string conDispatcherEmail { get; set; }
        public string conDriverName { get; set; }
        public string ConDriverPhone { get; set; }
        public string conDriverEmail { get; set; }
        public string ConTruck { get; set; }
        public string ConTrailer { get; set; }
        public string ConNotes { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }


    public class LoadFilesModel
    {
        public Int64 LoadFilesID { get; set; }
        public Int64 LoadID { get; set; }
        
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileURL { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}