using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class SettingsModel
    {
        public int SettingsID { get; set; }
        public bool IsLoginWithOTP { get; set; }
        public long LoggedInUserID { get; set; }

    }

}