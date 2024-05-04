using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class EquipmentTypeModel
    {
       
        public Int64 EquipmentTypeID { get; set; }

        [Required(ErrorMessage = "Equipment Type is required")]
        [StringLength(100)]
        public string EquipmentTypeName { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public bool StatusInd { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string strStatusInd { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 CreatedByID { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool Isdeleted { get; set; }
        public List<SelectListItem> StatusList { get; set; }


    }



}