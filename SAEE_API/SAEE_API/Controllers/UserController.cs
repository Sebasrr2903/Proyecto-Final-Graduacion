using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace SAEE_API.Controllers
{
    public class UserController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();
        //To send email
        MailService mailService = new MailService();

        /***********************Database Communication***********************/
        [HttpPost]
        [Route("RegisterUser")]
        public string RegisterUser(UserEnt user)
        {
            try
            {
                using (var context = new EntitiesSAEE())
                {
                    //Generate password
                    string pass = GeneratePassword(); 
                    //Encrypt password
                    string encryptPass = EncryptPassword(pass);

                    string resp;
                    resp = context.SP_RegisterUser(
                        user.Name, 
                        user.Lastname, 
                        user.Email, 
                        user.PhoneNumber,
                        encryptPass, 
                        user.ProfilePicture, 
                        user.UserType, 
                        user.Specialty, 
                        user.ExperienceYears
                        ).FirstOrDefault();

                    if (resp == "OK")
                    {
                        reports.ActionReport("RegisterUserDone", user.activeUser, "RegisterUser");

                        //Enviar un correo a la cuenta creada con las credenciales
                        //string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "Templates\\RecuperacionContrasena.html";
                        //string html = File.ReadAllText(urlHtml);
                        //html = html.Replace("@@Nombre", datos.nombre + " " + datos.apellidoUno + " " + datos.apellidoDos);
                        //html = html.Replace("@@Contrasena", datos.contrasena);

                        //mailService.SendEmail(datos.correo, "Recuperar Contraseña", html);

                        return resp;
                    }
                    else
                    {
                        //Only the response from the database is returned to show the error to the client
                        return resp;
                    }
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, user.activeUser, "RegisterUser");

                return string.Empty;
            }
        }

        [HttpPost]
        [Route("Login")]
        public SP_Login_Result Login(UserEnt user)
        {
            try
            {
                using (var context = new EntitiesSAEE())
                {
                    string encryptPass = EncryptPassword(user.Password);

                    return context.SP_Login(user.Email, encryptPass).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "Login");

                return null;
            }
        }

        /***********************Users List***********************/
        [HttpGet]
        [Route("UsersList")]
        public List<Users> UsersList()
        {
            try
            {
                using (var context = new EntitiesSAEE())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Users
                            select x).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "UsersList");

                return new List<Users>();
            }
        }


        /***********************Select List Item***********************/
        [HttpGet]
        [Route("ListUsersTypes")]
        public List<System.Web.Mvc.SelectListItem> ListUsersTypes()
        {
            try
            {
                using (var context = new EntitiesSAEE())
                {
                    var data = (from x in context.UsersTypes select x).ToList();
                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.id.ToString(), Text = x.description });
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "ListUsersTypes");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("ListSpecialties")]
        public List<System.Web.Mvc.SelectListItem> ListSpecialties()
        {
            try
            {
                using (var context = new EntitiesSAEE())
                {
                    var data = (from x in context.Specialties select x).ToList();
                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.id.ToString(), Text = x.description });
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "ListSpecialties");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }




        /***********************Methods***********************/
        private static string GeneratePassword()
        {
            //Crear una contraseña aleatorea que luego el usuario pueda cambiar
            // ya que la cuenta va a ser creada por un administrador
            string pass = "Hola123"; //De momento se define esta por defecto para pruebas
            return pass;
        }

        private static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }












    }
}
