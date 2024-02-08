using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SAEE_Web.Controllers
{
    public class UserController : Controller
    {
        UserModel userModel = new UserModel();

        /**********************LOGIN/LOGOUT**********************/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserEnt user)
        {
            //Space validations
            if (user.Email == null)
            {
                ViewBag.MessageMail = "Ingrese el correo electrónico.";
                return View();
            }else if (user.Password == null)
            {
                ViewBag.MessagePass = "Ingrese la contraseña.";
                return View();
            }
            else
            {
                var resp = userModel.Login(user);
                if (resp != null)
                {
                    Session["ActiveId"] = resp.Id;
                    Session["Name"] = resp.Name;
                    Session["Lastname"] = resp.Lastname;
                    Session["Email"] = resp.Email;
                    Session["PhoneNumber"] = resp.PhoneNumber;
                    Session["UserType"] = resp.UserType;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MessagePopUP = "Compruebe la información de sus credenciales.";
                    return View();
                }
            }  
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /**********************REGISTER**********************/
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserEnt user)
        {
            user.activeUser = (int)Session["ActiveId"];//For action register

            var resp = userModel.RegisterUser(user);

            if (resp == "OK")
            {
                return RedirectToAction("UsersTable", "User");
            }
            else if (resp == "Repeated email")
            {
                ViewBag.MessageMail = "Correo electrónico ya registrado.";
                return View();
            }else
            {
                ViewBag.MessagePopUP = "No se ha registrado el usuario.";
                return View();
            }
        }











        [HttpGet]
        public ActionResult UsersTable()
        {
            int ActiveId = (int)Session["ActiveId"];
            var datos = userModel.UsersList().Where(x => x.Id != ActiveId).ToList();
            return View(datos);
        }

        
    }
}