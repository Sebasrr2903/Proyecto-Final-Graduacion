using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class HomeController : Controller
    {
        //Controller used only for the index informational window
        public ActionResult Index()
        {
            return View();
        }
    }
}