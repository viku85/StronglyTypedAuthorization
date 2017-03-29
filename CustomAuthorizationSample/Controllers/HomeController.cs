using CustomAuthorizationSample.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorizationSample.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ContentResult LoginFailed()
        {
            return Content("Login Failed.");
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageBlog()
        {
            return GetResultOnAuthorization(() =>
            {
                // Codes
                return Content("Can manage Blog.");
            }, PermissionRule.CanEditBlog);
            // If having any other condition  
        }

        [AppAuthorize(PermissionRule.CanViewBlog)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AppAuthorize(PermissionRule.CanEditBlog)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}