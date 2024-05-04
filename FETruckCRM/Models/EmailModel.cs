using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class EmailModel
    {


        
        public Int32 EmailTypeID { get; set; }
        public string EmailType { get; set; }
        public string strEmailTypeID { get; set; }
        public Int64 EmailID { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Email Address is required")]

        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Body is required")]

        public string Body { get; set; }
        public bool Status { get; set; }
        public string strStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool IsDeletedInd { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> EmailTypeList { get; set; }

        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }

    }

    public class EmailTypeModel
    {
        public int EmailTypeID { get; set; }
        public string EmailType { get; set; }
    }

    }