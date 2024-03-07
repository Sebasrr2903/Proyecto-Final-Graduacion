using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class SchedulesController : Controller
    {

        SchedulesModel scheduleModel = new SchedulesModel();


        [HttpGet]
        public ActionResult ScheduleTable()
        {
            int ActiveId = (int)Session["ActiveId"];
            var datos = scheduleModel.ScheduleList().Where(x => x.Id != ActiveId).ToList();
            return View(datos);
        }

        // GET: Schedule
        [HttpGet]
        public ActionResult RegisterSchedule()
        {

            return View();

        }

        [HttpPost]
        public ActionResult RegisterSchedule(SchedulesEnt course)
        {
            course.activeUser = (int)Session["ActiveId"];

            // Validación del formato de hora para StartTime y EndTime
            if (!ValidateTimeFormat(course.StartTime) || !ValidateTimeFormat(course.EndTime))
            {
                ViewBag.BoxMessage = "El formato de hora es incorrecto. El formato válido es HH:MM a.m. o p.m.";
                return View();
            }

            var resp = scheduleModel.RegisterSchedule(course);

            if (resp == "OK")
            {
                ViewBag.SuccessMessage = "El horario se ha registrado correctamente.";
                return View();
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado el curso.";
                return View();
            }
        }

        private bool ValidateTimeFormat(string time)
        {
            // Expresión regular para el formato de hora: HH:MM a.m. o p.m.
            string pattern = @"^(1[0-2]|0?[1-9]):[0-5][0-9] (a\.m\.|p\.m\.)$";

            // Se realiza la validación del formato de hora con la expresión regular
            Regex regex = new Regex(pattern);
            return regex.IsMatch(time);
        }


    }
}