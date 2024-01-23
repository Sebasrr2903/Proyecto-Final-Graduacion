using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public ActionResult GeneralReport()
        {
            return View();
        }
    }
}