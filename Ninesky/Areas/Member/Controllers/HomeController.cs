using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ninesky.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主控制器
        /// </summary>
        // GET: Member/Home
        public ActionResult Index()
        {
            /// <summary>
            /// 主页面
            /// </summary>
            /// <returns></returns>
            return View();
        }
    }
}