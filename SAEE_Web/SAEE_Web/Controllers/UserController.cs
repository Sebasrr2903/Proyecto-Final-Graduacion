using SAEE_Web.Entities;
using SAEE_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Drawing;


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
			var resp = userModel.Login(user);

			if (resp != null && resp.Active)
			{
				Session["ActiveId"] = resp.Id;
				Session["Name"] = resp.Name;
				Session["Lastname"] = resp.Lastname;
				Session["Email"] = resp.Email;
				Session["PhoneNumber"] = resp.PhoneNumber;
				Session["UserType"] = resp.UserType;
				Session["Active"] = resp.Active;
				Session["SelectedWeekNum"] = 0;

				if (resp.ProfilePicture != null)
				{
					byte[] bytes = resp.ProfilePicture;
					string base64String = Convert.ToBase64String(bytes);
					Session["ProfileImageB64"] = base64String;
				}

				return RedirectToAction("Index", "Home");
			}
			else if (resp != null && !resp.Active)
			{
				ViewBag.BoxMessage = "Usuario inactivo. Por favor, contacte al administrador.";
				return View();
			}
			else
			{
				ViewBag.BoxMessage = "Compruebe la información de sus credenciales.";
				return View();
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
        public ActionResult UpdateUser( UserEnt user)
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
			// Llamar al método para recuperar la contraseña del modelo de usuario
			var resp = userModel.RecoverPassword(user);

			if (resp == "OK")
			{
				// Si la recuperación de la contraseña fue exitosa, establecer un mensaje de éxito en ViewBag
				ViewBag.BoxSuccess = "Se ha enviado un correo electrónico para la recuperación de su cuenta.";
			}
			else
			{
				// Si la recuperación de la contraseña falló, establecer un mensaje de error en ViewBag
				ViewBag.BoxMessage = "No se ha podido recuperar la cuenta, verifique que los datos sean correctos.";
			}

			// Redirigir a la vista actual para mostrar el mensaje
			return View();
		}



		[HttpGet]
        public ActionResult ProfileData()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileUser(long q)
        {
            var data = userModel.UserData(q);
            return View(data);
        }

        [HttpPost]
        public ActionResult ProfileUser(HttpPostedFileBase ProfileImg, UserEnt user)
        {
            user.activeUser = (int)Session["ActiveId"];//For action register

            if (ProfileImg != null && ProfileImg.ContentLength > 0)
            {
                byte[] imageBytes;
                using (BinaryReader reader = new BinaryReader(ProfileImg.InputStream))
                {
                    imageBytes = reader.ReadBytes(ProfileImg.ContentLength);
                }

                user.ProfilePicture = imageBytes;

                string base64String = Convert.ToBase64String(imageBytes);
                Session["ProfileImageB64"] = base64String;
            }

            var resp = userModel.UpdateUser(user);

            Session["Name"] = user.Name;
            Session["Lastname"] = user.Lastname;
            Session["PhoneNumber"] = user.PhoneNumber;

            if (resp == "OK")
            {
                return RedirectToAction("ProfileData", "User");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo actualizar el usuario.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteProfilePicture(long q)
        {
            var user = new UserEnt();
            user.Id = (int)q;

            var resp = userModel.DeleteProfilePicture(user);
            Session["ProfileImageB64"] = null;

            if (resp == "OK") //Error al entrar a esta respuesta
            {
                Session["ProfileImageB64"] = null;
                return RedirectToAction("ProfileData", "User");
            }
            else
            {
                ViewBag.BoxMessage = "No se pudo eliminar la foto de perfil.";
                return RedirectToAction("ProfileData", "User");
            }
        }


        


    }
}