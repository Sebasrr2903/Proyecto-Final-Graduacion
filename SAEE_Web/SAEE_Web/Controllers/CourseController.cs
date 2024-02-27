using Newtonsoft.Json.Linq;
using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class CourseController : Controller
    {

        CourseModel coursesModel =  new CourseModel();
        EnrolledCoursesModel enrolledCoursesModel =  new EnrolledCoursesModel();

        [HttpGet]
        public ActionResult AllCourses()
        {
            Session["SelectedWeekNum"] = 0; //Para iniciar en semana introduccion
            int q = (int)Session["ActiveId"];
            var datos = enrolledCoursesModel.EnrolledCoursesPerStudent(q);
            return View(datos);
        }

        /**********************TABLE/STATUS/UPDATE/RECOVER**********************/
        [HttpGet]
        public ActionResult CoursesTable()
        {
            int ActiveId = (int)Session["ActiveId"];
            var datos = coursesModel.CoursesList().Where(x => x.Id != ActiveId).ToList();
            return View(datos);
        }

        [HttpGet]
        public ActionResult ChangeStatusCourse(long q)
        {
            var course = new  CoursesEnt();
            course.Id = (int)q;

            course.activeUser = (int)Session["ActiveId"];//For action register

            var resp = coursesModel.ChangeStatusCourse(course);

            if (resp == "OK")
            {
                return RedirectToAction("CoursesTable", "Course");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo modificar el estado del curso.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RegisterCourse()
        {
      
            return View();
        }

        [HttpPost]
        public ActionResult RegisterCourse(CoursesEnt course)
        {
            course.activeUser = (int)Session["ActiveId"];//For action register

            ViewBag.CourseName = course.Name;

            var resp = coursesModel.RegisterCourse(course);

            if (resp == "OK")
            {
                return RedirectToAction("CoursesTable", "Course");
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado el curso.";
               
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateCourse(long q)
        {
            var data = coursesModel.CourseData(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateCourse(CoursesEnt course)
        {
            course.activeUser = (int)Session["ActiveId"];//For action register

            var resp = coursesModel.UpdateCourse(course);

            if (resp == "OK")
            {
                return RedirectToAction("CoursesTable", "Course");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar el usuario.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult SpecificCourse(int q)
        {
            var datos = enrolledCoursesModel.SpecificCourse(q);
            return View(datos);
        }
    }
}