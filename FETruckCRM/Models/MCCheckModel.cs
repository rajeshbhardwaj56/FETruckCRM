using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class MCCheckModel
    {
        public long? MCCheckID { get; set; }
        public string AddedByUser { get; set; }
        public string TeamLead { get; set; }
        public string TeamManager { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Isdeleted { get; set; }
        [Required(ErrorMessage = "MC Number is required")]
        public long MCNumber { get; set; }
        [Required(ErrorMessage = "Carrier name is required")]
        public string CarrierName { get; set; }

        [Required(ErrorMessage = "Commodity value is required")]
        public long? CommodityValue { get; set; }

        [Required(ErrorMessage = "Commodity type is required")]
        public string CommodityType { get; set; }

        [Required(ErrorMessage = "Equipment type is required")]
        public string EquipmentType { get; set; }

       
        public string CommodityProofURL { get; set; }
        public List<SelectListItem> EquipmentTypeListing { get; set; }
        //
        public int ApprovalStatus { get; set; }
        public string strApprovalStatus { get; set; }
        public long AppropvedBy { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }

        public long CreatedByID { get; set; }
        public long LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeletedInd { get; set; }

        //[Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

        public List<MCCheckDocModel> MCCheckDocsList { get; set; }
    }

    public class MCCheckDocModel
    {

        public long MCCheckDocID { get; set; }
        public long MCCheckID { get; set; }
        public string MCCheckDocName { get; set; }
        public string MCCheckDocURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }

    }
}