
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



        CourseTasksModel CourseTaskssModel = new CourseTasksModel();
        
        [HttpGet]
        public ActionResult RegisterCourseTasks()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RegisterCourseTasks(HttpPostedFileBase FileNew, CoursesTasksEnt courseTasks)
        {
            courseTasks.activeUser = (int)Session["ActiveId"]; // Para la acción de registro

            var resp = CourseTaskssModel.RegisterCourseTasks(courseTasks);

            if (resp == "OK" && FileNew != null && FileNew.ContentLength > 0)
            {
                try
                {
                    // Leer el contenido del archivo en un byte array
                    byte[] fileData;
                    using (BinaryReader reader = new BinaryReader(FileNew.InputStream))
                    {
                        fileData = reader.ReadBytes(FileNew.ContentLength);
                    }

                    // Asignar los datos del archivo al objeto de la tarea del curso
                    courseTasks.File = fileData;

                    // Guardar la tarea del curso (incluyendo el archivo) en la base de datos
                    var result = CourseTaskssModel.RegisterCourseTasks(courseTasks);

                    if (result == "OK")
                    {
                        return RedirectToAction("SpecificCourse", "Course");
                    }
                    else
                    {
                        ViewBag.BoxMessage = "No se ha registrado el curso correctamente.";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.BoxMessage = "Error al guardar el archivo: " + ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.BoxMessage = "No se ha proporcionado ningún archivo.";
                return View();
            }
        }
       }
    }