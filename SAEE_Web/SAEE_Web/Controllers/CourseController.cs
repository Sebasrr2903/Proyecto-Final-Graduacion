using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class CourseController : Controller
    {
        [HttpGet]
        public ActionResult AllCourses()
        {
            return View();
        }
    }
}