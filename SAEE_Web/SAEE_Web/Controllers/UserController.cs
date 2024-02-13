using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            }
            else if (user.Password == null)
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
                    ViewBag.BoxMessage = "Compruebe la información de sus credenciales.";
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
            ViewBag.ListUsersTypes = userModel.ListUsersTypes();
            ViewBag.ListSpecialties = userModel.ListSpecialties();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserEnt user)
        {
            user.activeUser = (int)Session["ActiveId"];//For action register

            ViewBag.ListUsersTypes = userModel.ListUsersTypes();
            ViewBag.ListSpecialties = userModel.ListSpecialties();
            var resp = userModel.RegisterUser(user);

            if (resp == "OK")
            {
                return RedirectToAction("UsersTable", "User");
            }
            else if (resp == "Repeated email")
            {
                ViewBag.MessageMail = "Correo electrónico ya registrado.";
                ViewBag.ListUsersTypes = userModel.ListUsersTypes();
                ViewBag.ListSpecialties = userModel.ListSpecialties();
                return View();
            }
            else
            {
                ViewBag.BoxMessage = "No se ha registrado el usuario.";
                ViewBag.ListUsersTypes = userModel.ListUsersTypes();
                ViewBag.ListSpecialties = userModel.ListSpecialties();
                return View();
            }
        }

        /**********************TABLE/STATUS/UPDATE/RECOVER/PROFILE**********************/
        [HttpGet]
        public ActionResult UsersTable()
        {
            int ActiveId = (int)Session["ActiveId"];
            var datos = userModel.UsersList().Where(x => x.Id != ActiveId).ToList();
            return View(datos);
        }

        [HttpGet]
        public ActionResult ChangeStatusUser(long q)
        {
            var user = new UserEnt();
            user.Id = (int)q;

            user.activeUser = (int)Session["ActiveId"];//For action register

            var resp = userModel.ChangeStatusUser(user);

            if (resp == "OK")
            {
                return RedirectToAction("UsersTable", "User");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo modificar el estado del usuario.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateUser(long q)
        {
            var data = userModel.UserData(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserEnt user)
        {
            user.activeUser = (int)Session["ActiveId"];//For action register

            var resp = userModel.UpdateUser(user);

            if (resp == "OK")
            {
                return RedirectToAction("UsersTable", "User");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar el usuario.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoverPassword(UserEnt user)
        {
            var resp = userModel.RecoverPassword(user);

            if (resp == "OK")
            {
                ViewBag.BoxMessage = "Se ha enviado un correo electrónico para la recuperación de su cuenta.";
                return View();
                //Hacer que se redirija al login luego de mostrar el mensaje
            }
            else
            {
                ViewBag.BoxMessage = "No se ha podido recuperar la cuenta, verifique que los datos sean correctos.";
                return View();
            }
        }
        [HttpGet]
        public ActionResult ProfileUser(long q)
        {
            var data = userModel.UserData(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult ProfileUser(UserEnt user)
        {
            user.activeUser = (int)Session["ActiveId"];//For action register

            var resp = userModel.UpdateUser(user);

            if (resp == "OK")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar el usuario.";
                return View();
            }
        }

    }
}