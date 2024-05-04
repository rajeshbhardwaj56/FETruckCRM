using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace FETruckCRM.Data
{
    public class SessionExpire : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpSessionStateBase session = filterContext.HttpContext.Session;

            //if (session.IsNewSession || session["UserName"] == null)
            //{
            //    FormsAuthentication.SignOut();
            //    filterContext.Result =
            //   new RedirectToRouteResult(new RouteValueDictionary
            //     {
            // { "action", "Login" },
            //{ "controller", "Account" },
            //{ "returnUrl", filterContext.HttpContext.Request.RawUrl}
            //      });

            //    return;
            //}
            if (session.IsNewSession || session["UserName"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, return result as a simple string, 
                    // and inform calling JavaScript code that a user should be redirected.
                    //JsonResult result = new JsonResult
                    //{
                    //    Data = new
                    //    {
                    //        // put whatever data you want which will be sent
                    //        // to the client
                    //        message = "sorry, but you were logged out"
                    //    },
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                    filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Account", action = "Timeout" }));
                    //filterContext.Result = result;
                }
                else
                {
                    // For round-trip requests,
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                { "Controller", "Account" },
                { "Action", "Login" }
                    });
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SessionTimeoutAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IPrincipal user = filterContext.HttpContext.User;
            base.OnAuthorization(filterContext);
            if (!user.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "AccountController", action = "Timeout" }));
            }
        }
    }
}