using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FETruckCRM.Models
{
    public class LogModel
    {

        public string LogID { get; set; }
        public string UserID { get; set; }
        public string User { get; set; }
        public string LogDate { get; set; }
        public string ModuleName { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string EditMode { get; set; }
        public long TotalRecords { get; set; }

    }

    public class LogInputModel
    {
        public List<SelectListItem> UserList { get; set; }

        public int SelectedUserId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

     
    }

}
