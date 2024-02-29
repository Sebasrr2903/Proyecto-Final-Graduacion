
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
        
        [HttpGet]
        public ActionResult RegisterCourseTasks()
        {
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
                ViewBag.BoxMessageDone = "Tarea enviada correctamente.";
                return View();
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado la tarea correctamente.";
                return View();
            }
        }
    }
    }