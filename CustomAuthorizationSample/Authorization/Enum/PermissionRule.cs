using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuthorizationSample.Authorization
{
    // TODO: Need to derive from database.
    public enum PermissionRule
    {
        CanAddBlog,
        CanEditBlog,
        CanViewBlog,
    }
}