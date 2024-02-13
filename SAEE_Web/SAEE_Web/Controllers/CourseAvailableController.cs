using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAEE_Web.Models;
using SAEE_Web.Entities;

namespace SAEE_Web.Controllers
{
    public class CourseAvailableController : Controller
    {
        CourseAvailableModel courseAvailableModel = new CourseAvailableModel();

        [HttpGet]
        public ActionResult RegisterCourseAvailable()
        {
            ViewBag.ListSelectCourses = courseAvailableModel.SelectListCourses();
            ViewBag.ListSelectTeacher = courseAvailableModel.SelectListTeacher();
            ViewBag.SelectSchedule = courseAvailableModel.SelectListSchedule();
            return View();
        }
    }
}