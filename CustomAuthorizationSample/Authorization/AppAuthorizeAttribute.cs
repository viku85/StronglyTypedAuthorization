using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorizationSample.Authorization
{
    /// <summary>
    /// Custom authorization implementation for checking permission rules.
    /// </summary>
    public class AppAuthorizeAttribute
      : System.Web.Mvc.AuthorizeAttribute, System.Web.Mvc.IActionFilter
    {
        /// <summary>
        /// Gets or sets the requested permissions.
        /// </summary>
        /// <value>
        /// The requested permissions.
        /// </value>
        public IList<PermissionRule> RequestedPermissions { get; set; }

        /// <summary>
        /// Gets or sets the type of the comparison.
        /// </summary>
        /// <value>
        /// The type of the comparison.
        /// </value>
        public ComparisonType ComparisonType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        public AppAuthorizeAttribute(params PermissionRule[] permissions)
        {
            RequestedPermissions = permissions.ToList();
        }

        #region " IActionFilter implementation for saving permission status "

        /// <summary>
        /// Called before an action method executes.
        /// Set users permission into ViewBag
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                // Set users permission to ViewBag
                var usrPermissions = GetUserPermission(filterContext.HttpContext);
                filterContext.Controller.ViewBag.UserPermissionRules = usrPermissions;
            }
        }

        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Not required
        }

        #endregion " IActionFilter implementation for saving permission status "

        /// <summary>

        /// An entry point for custom authorization checks. This will check user roles and
        /// permission.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific
        /// information about an individual HTTP request.</param>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }
            var userName = httpContext.User.Identity.Name;
            if (RequestedPermissions != null && RequestedPermissions.Any())
            {
                var usrPermissions = GetUserPermission(httpContext);
                if (!WebHelper.HasPermission(usrPermissions, RequestedPermissions, userName, ComparisonType))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Redirect);
            filterContext.HttpContext.Response.AddHeader("custom-authorize", "false");
            filterContext.HttpContext.Response.Redirect("~/Home/LoginFailed", false);
            // TODO: Page redirect if having any otherwise no need to override function.
        }

        /// <summary>
        /// Gets the user permission.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>Permissions related to user</returns>
        protected IList<PermissionRule> GetUserPermission(HttpContextBase httpContext)
        {
            var userName = httpContext.User.Identity.Name;
            // TODO: Get all permission list for user.
            // TODO: Add caching logic to avoid DB hits
            return new List<PermissionRule> { PermissionRule.CanViewBlog };
        }
    }
}