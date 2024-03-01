using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class CourseAssignmentController : Controller
    {

        CourseAssignmentModel courseAssignmentModel = new CourseAssignmentModel();

        [HttpGet]
        public ActionResult RegisterAssignment()
        {

            return View();
        }


        [HttpPost]
        public ActionResult RegisterAssignment(CourseAssignmentsEnt course)
        {
            course.activeUser = (int)Session["ActiveId"];//For action register

        
            course.AssignmentWeek = (int)Session["SelectedWeekId"];

            var resp = courseAssignmentModel.RegisterAssignment(course);

            if (resp == "OK")
            {
                ViewBag.BoxMessageDone = "Entregable se registrado correctamente.";
                return View();
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado el entregable.";

                return View();
            }
        }

    }
}