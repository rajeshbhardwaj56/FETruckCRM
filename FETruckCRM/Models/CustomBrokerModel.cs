using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class CustomBrokerModel
    {
        public Int64 CustomBrokerID { get; set; }
        [Required(ErrorMessage = "Broker Name is required")]
        [StringLength(100)]
        public string BrokerName { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        [Required(ErrorMessage = "Crossing is required")]
        [StringLength(100)]
        public string Crossing { get; set; }
        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(100)]
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        public string TollFree { get; set; }
        public string Fax { get; set; }
        public Int64 CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int StatusInd { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        [Required(ErrorMessage = "Please select status")]
        public string strStatusInd { get; set; }
    }



}