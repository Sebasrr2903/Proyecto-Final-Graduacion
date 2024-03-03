using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Controllers
{
    public class ContentPerWeekController : Controller
    {
        ContentPerWeekModel contentPerWeek = new ContentPerWeekModel();
        static string returnUrl; //For return

        [HttpGet]
        public ActionResult UpdateContentPerWeek(int q)
        {
            returnUrl = Request.UrlReferrer?.ToString();//For return

            var data = contentPerWeek.GetContentPerWeek(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateContentPerWeek(ContentPerWeekEnt contentPerWeekEnt)
        {
            contentPerWeekEnt.activeUser = (int)Session["ActiveId"];//For action register

            var resp = contentPerWeek.UpdateContentPerWeek(contentPerWeekEnt);

            if (resp == "OK")
            {
                return Redirect(returnUrl); //For return
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar el contenido.";
                return View();
            }
        }
    }
}