using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorizationSample.Authorization
{
    public class BaseController
      : Controller
    {
        /// <summary>  
        /// Determines whether the specified comparison type has permission.  
        /// </summary>  
        /// <param name="comparisonType">Type of the comparison.</param>  
        /// <param name="permissionRules">The permissions.</param>  
        /// <returns>True, If user has permission.</returns>  
        protected bool HasPermission(ComparisonType comparisonType = ComparisonType.All,
          params PermissionRule[] permissionRules)
        {
            return WebHelper.HasPermission(ViewBag.UserPermissionRules, permissionRules,
              HttpContext.User.Identity.Name, comparisonType);
        }
        /// <summary>  
        /// Gets the result based on authorization.  
        /// </summary>  
        /// <param name="resultOnCondition">The result on condition.</param>  
        /// <param name="comparisonType">Type of the comparison.</param>  
        /// <param name="permissionRules">The permission rules.</param>  
        /// <returns>If authorization success then the view else redirect to page not authorized.</returns>  
        protected ActionResult GetResultOnAuthorization(Func<ActionResult> resultOnCondition,
          ComparisonType comparisonType = ComparisonType.All,
          params PermissionRule[] permissionRules)
        {
            return HasPermission(comparisonType, permissionRules) ? resultOnCondition() :
                new HttpUnauthorizedResult(); // Unauthorized action redirect  
        }
        /// <summary>  
        /// Gets the result based on authorization.  
        /// </summary>  
        /// <param name="resultOnCondition">The result on condition.</param>  
        /// <param name="permissionRules">The permission rules.</param>  
        /// <returns>If authorization success then the view else redirect to page not authorized.</returns>  
        protected ActionResult GetResultOnAuthorization(Func<ActionResult> resultOnCondition, params PermissionRule[] permissionRules)
        {
            return GetResultOnAuthorization(resultOnCondition, ComparisonType.All, permissionRules);
        }
    }
}