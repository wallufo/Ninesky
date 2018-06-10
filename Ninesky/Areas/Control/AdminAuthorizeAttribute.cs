using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ninesky.Areas.Control
{
    /// <summary>
    /// 管理员身份验证类
    /// </summary>
    public class AdminAuthorizeAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// 重写自定义授权检查
        /// </summary>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["AdminID"] == null) return false;
            else return true;
        }
        /// <summary>
        /// 重写未授权的 HTTP 请求处理
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Control/Admin/Login");
        }
    }
}