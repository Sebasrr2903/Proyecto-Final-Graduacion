﻿using Newtonsoft.Json.Linq;
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
        CourseTasksModel courseTasksModel = new CourseTasksModel();
        MaterialPerWeekModel materialPerWeekModel = new MaterialPerWeekModel();

        [HttpGet]
        public ActionResult AllCourses()
        {
            Session["SelectedWeekNum"] = 0; //Para iniciar en semana introduccion
            Session["SelectedWeekId"] = 0;

            int q = (int)Session["ActiveId"];

            if ((int)Session["UserType"] == 2)
            {
                var datos = enrolledCoursesModel.EnrolledCoursesPerTeacher(q);
                return View(datos);
            }
            else
            {
                var datos = enrolledCoursesModel.EnrolledCoursesPerStudent(q);
                return View(datos);
            }
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
            var enrolledCoursesList = enrolledCoursesModel.SpecificCourse(q);

            int weekId = 0;
            if ((int)Session["SelectedWeekId"] == 0)
            {
                weekId = enrolledCoursesList[0].WeekId;
            }
            else
            {
                weekId = (int)Session["SelectedWeekId"];
            }
            var courseAssignmentsList = courseTasksModel.SpecificAssignment(weekId);
            var materialPerWeekList = materialPerWeekModel.SpecificMaterial(weekId);

            //Procesar foto del profesor
            if (enrolledCoursesList[0].TeacherPhoto != null)
            {
				byte[] bytes = enrolledCoursesList[0].TeacherPhoto;
				string base64String = Convert.ToBase64String(bytes);
				Session["TeacherProfileImageB64"] = base64String;
            }
            else
            {
                Session["TeacherProfileImageB64"] = null;

			}

			var viewModel = new ViewModelCourse_Task_Material
            {
                EnrolledCourses = enrolledCoursesList,
                CourseAssignments = courseAssignmentsList,
                WeekMaterial = materialPerWeekList
            };

            return View(viewModel);

        }
    }
}