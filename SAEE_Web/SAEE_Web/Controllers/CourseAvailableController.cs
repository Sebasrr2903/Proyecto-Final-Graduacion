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
            ViewBag.SelectListCourses = courseAvailableModel.SelectListCourses();
            ViewBag.SelectListTeacher = courseAvailableModel.SelectListTeacher();
            ViewBag.SelectListSchedule = courseAvailableModel.SelectListSchedule();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterCourseAvailable(CourseAvailableEnt courseAvailable)
        {
            //user.activeUser = (int)Session["ActiveId"];//For action register

            ViewBag.SelectListCourses = courseAvailableModel.SelectListCourses();
            ViewBag.SelectListTeacher = courseAvailableModel.SelectListTeacher();
            ViewBag.SelectListSchedule = courseAvailableModel.SelectListSchedule();

            var resp = courseAvailableModel.RegisterCourseAvailable(courseAvailable);

            if (resp == "OK")
            {
                ViewBag.BoxMessage = "Curso creado.";
                return View();
            }
            else
            {
                ViewBag.BoxMessage = "No se ha creado el curso.";
                ViewBag.SelectListCourses = courseAvailableModel.SelectListCourses();
                ViewBag.SelectListTeacher = courseAvailableModel.SelectListTeacher();
                ViewBag.SelectListSchedule = courseAvailableModel.SelectListSchedule();
                return View();
            }
        }




    }
}