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
        static string returnUrl; //For return

        [HttpGet]
        public ActionResult RegisterAssignment()
        {
            returnUrl = Request.UrlReferrer?.ToString();//For return

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
                return Redirect(returnUrl); //For return
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado el entregable.";
                return View();
            }
        }


    }
}