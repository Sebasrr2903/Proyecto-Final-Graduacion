
using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Activities;

namespace SAEE_Web.Controllers
{
    public class CourseTasksController : Controller
    {
        CourseTasksModel courseTasksModel = new CourseTasksModel();
        static string returnUrl; //For return

        [HttpGet]
        public ActionResult RegisterCourseTasks()
        {
            returnUrl = Request.UrlReferrer?.ToString();//For return

            return View();
        }

        [HttpPost]
        public ActionResult RegisterCourseTasks(HttpPostedFileBase FileNew, CoursesTasksEnt courseTasks)
        {
            courseTasks.activeUser = (int)Session["ActiveId"]; // Para la acción de registro

            courseTasks.AssignmentId = (int)Session["SelectedAssignmentId"];
            courseTasks.StudentId = (int)Session["ActiveId"];

            var resp = "";

            if (FileNew != null && FileNew.ContentLength > 0)
            {
                byte[] fileData;
                using (BinaryReader reader = new BinaryReader(FileNew.InputStream))
                {
                    fileData = reader.ReadBytes(FileNew.ContentLength);
                }

                // Asignar los datos del archivo al objeto de la tarea del curso
                courseTasks.File = fileData;
                courseTasks.FileExtension = FileNew.FileName;

                //Ahora sí llamamos al metodo para insertar, una vez que está completo el objeto
                resp = courseTasksModel.RegisterCourseTasks(courseTasks);
            }
            else
            {
                ViewBag.BoxMessage = "No ha agregado el archivo.";
                return View();
            }

            if (resp == "OK")
            {
                return Redirect(returnUrl); //For return
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado la tarea correctamente.";
                return View();
            }
        }


        [HttpGet]
        public ActionResult DeliveredCoursetasks()
        {
            int assignmentId = (int)Session["SelectedAssignmentId"];
            var data = courseTasksModel.DeliveredCoursetasks(assignmentId);

            return View(data);
        }

        [HttpGet]
        public ActionResult DownloadTask(int q)
        {
            CoursesTasksEnt task = courseTasksModel.GetTask(q);

            if (task != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=" + task.FileExtension);
                Response.BinaryWrite(task.File);
                Response.End();

                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GradeAssignment(int q)
        {
            Session["TempTaskId"] = q;
            returnUrl = Request.UrlReferrer?.ToString();//For return

            return View();
        }

        [HttpPost]
        public ActionResult GradeAssignment(AssignmentGradingEnt grade)
        {
            grade.activeUser = (int)Session["ActiveId"];//For action register

            grade.TaskId = (int)Session["TempTaskId"];
            var resp = courseTasksModel.GradeAssignment(grade);

            //Mostrar mensaje si ya ha sido calificada
            if (resp == "OK")
            {
                return Redirect(returnUrl); //For return
            }
            else
            {
                ViewBag.BoxMessage = "No se ha calificado la entrega.";
                return View();
            }
        }




    }
}