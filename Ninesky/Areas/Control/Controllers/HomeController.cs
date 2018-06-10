using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ninesky.Areas.Control.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        // GET: Control/Home
        public ActionResult Index()
        {
            return View();
        }
      
    }
}