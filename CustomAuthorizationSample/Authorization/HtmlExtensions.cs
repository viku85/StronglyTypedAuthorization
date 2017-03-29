using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorizationSample.Authorization
{
    /// <summary>  
    /// Native HTML Helper extension  
    /// </summary>  
    public static class HtmlExtensions
    {
        /// <summary>  
        /// Determines whether user is having permission.  
        /// </summary>  
        /// <param name="htmlHelper">The HTML helper.</param>  
        /// <param name="comparisonType">Type of the comparison.</param>  
        /// <param name="permissions">The permission checks.</param>  
        /// <returns>  
        /// True, if  
        /// </returns>  
        /// <exception cref="System.Exception">The controller used to render this view doesn't inherit from BaseController</exception>  
        public static bool IsUserHavingPermission(this HtmlHelper htmlHelper,
          ComparisonType comparisonType = ComparisonType.All, params PermissionRule[] permissions)
        {
            IEnumerable<PermissionRule> userPermissionRules = htmlHelper.ViewBag.UserPermissionRules;
            if (userPermissionRules == null)
            {
                return false;
                //throw new NotImplementedException("User permissions not set.");
            }
            return WebHelper.HasPermission(userPermissionRules, permissions,
              htmlHelper.ViewContext.RequestContext.HttpContext.User.Identity.Name
              , comparisonType);
        }
        /// <summary>  
        /// Determines whether user is having specified permissions.  
        /// </summary>  
        /// <param name="htmlHelper">The HTML helper.</param>  
        /// <param name="permissions">The permissions.</param>  
        /// <returns></returns>  
        public static bool IsUserHavingPermission(this HtmlHelper htmlHelper, params PermissionRule[] permissions)
        {
            return IsUserHavingPermission(htmlHelper, ComparisonType.All, permissions);
        }
    }
}