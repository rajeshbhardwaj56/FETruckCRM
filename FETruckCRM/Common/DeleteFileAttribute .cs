using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FETruckCRM.Common
{
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Delete file 
            filterContext.HttpContext.Response.Flush();
            var filePathResult = filterContext.Result as FilePathResult;
            if (filePathResult != null)
            {
                System.IO.File.Delete(filePathResult.FileName);
            }
        }
    }
}
