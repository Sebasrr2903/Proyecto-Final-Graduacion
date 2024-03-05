using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class CourseMaterialController : Controller
    {
        MaterialPerWeekModel materialPerWeekModel = new MaterialPerWeekModel();
        static string returnUrl; //For return

        [HttpGet]
        public ActionResult RegisterMaterial()
        {
            returnUrl = Request.UrlReferrer?.ToString();//For return

            return View();
        }

        [HttpPost]
        public ActionResult RegisterMaterial(HttpPostedFileBase FileNew, MaterialPerWeekEnt material)
        {
            material.activeUser = (int)Session["ActiveId"]; // Para la acción de registro

            material.WeekId = (int)Session["SelectedWeekId"];

            var resp = "";

            if (FileNew != null && FileNew.ContentLength > 0)
            {
                byte[] fileData;
                using (BinaryReader reader = new BinaryReader(FileNew.InputStream))
                {
                    fileData = reader.ReadBytes(FileNew.ContentLength);
                }

                // Asignar los datos del archivo al objeto de la tarea del curso
                material.MaterialFile = fileData;
                material.MaterialExtension = FileNew.FileName;

                //Ahora sí llamamos al metodo para insertar, una vez que está completo el objeto
                resp = materialPerWeekModel.RegisterWeekMaterial(material);
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
                ViewBag.BoxMessage = "No se ha registrado el material.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DownloadMaterial(int q)
        {
            MaterialPerWeekEnt material = materialPerWeekModel.GetMaterial(q);

            if (material != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=" + material.MaterialExtension);
                Response.BinaryWrite(material.MaterialFile);
                Response.End();

                return View();
            }
            else
            {
                return View();
            }      
        }
    }
}