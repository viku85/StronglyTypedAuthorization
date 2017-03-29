using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuthorizationSample.Authorization
{
    /// <summary>  
    /// Comparison request for multiple authorization.
    /// </summary>  
    public enum ComparisonType
    {
        /// <summary>  
        /// Comparison based on all fields  
        /// </summary>  
        All,
        /// <summary>  
        /// Comparison based on any available fields  
        /// </summary>  
        Any,
    }
}