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


        [HttpGet]
        public ActionResult ViewCourseAssignment()
        {
            int q = (int)Session["SelectedAssignmentId"];

            var data = courseAssignmentModel.GetAssignment(q);
            return View(data);

        }

        [HttpGet]
        public ActionResult ChangeStatusAssignment()
        {
            returnUrl = Request.UrlReferrer?.ToString();


            int q = (int)Session["SelectedAssignmentId"];


            var assignment = new CourseAssignmentsEnt();
            assignment.AssignmentId = q;

            assignment.activeUser = (int)Session["ActiveId"];//For action register

            var resp = courseAssignmentModel.ChangeStatusAssignment(assignment);

            if (resp == "OK")
            {
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo modificar el estado de la tarea.";
                return Redirect(returnUrl);
            }
        }

        [HttpGet]
        public ActionResult UpdateAssignment()
        {
            returnUrl = Request.UrlReferrer?.ToString();

            int q = (int)Session["SelectedAssignmentId"];
            var data = courseAssignmentModel.GetAssignment(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateAssignment(CourseAssignmentsEnt assignment)
        {
            assignment.activeUser = (int)Session["ActiveId"];//For action register

            var resp = courseAssignmentModel.UpdateAssignment(assignment);

            if (resp == "OK")
            {
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar la tarea.";
                return View();
            }
        }

        

    }
}