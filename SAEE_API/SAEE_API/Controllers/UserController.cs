using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.IO;

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
                using (var context = new SAEEEntities())
                {
                    //Generate password
                    string pass = GeneratePassword(); 
                    //Encrypt password
                    string encryptPass = EncryptPassword(pass);

                    string resp;
                    resp = context.SP_RegisterUser(
                        user.Name, 
                        user.Lastname,
                        user.Birthdate,
                        user.Email,
                        user.PhoneNumber,
                        encryptPass, 
                        user.UserType, 
                        user.Specialty, 
                        user.ExperienceYears
                        ).FirstOrDefault();

                    if (resp == "OK")
                    {
                        //Add report of the action performed
                        reports.ActionReport("RegisterUserDone", user.activeUser, "RegisterUser");

                        //Send an email to the account created with the credentials
                        string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "Templates\\TemporaryCredentials.html";
                        string html = File.ReadAllText(urlHtml);
                        html = html.Replace("@@Name", user.Name + " " + user.Lastname);
                        html = html.Replace("@@Email", user.Email);
                        html = html.Replace("@@Password", pass);

                        mailService.SendEmail(user.Email, "Credenciales SAEE-ELEC", html);

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
                using (var context = new SAEEEntities())
                {
                    string encryptPass = EncryptPassword(user.Password);

                    return context.SP_Login(user.Email, encryptPass).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "Login");

                return null;
            }
        }

        [HttpPut]
        [Route("ChangeStatusUser")]
        public string ChangeStatusUser(UserEnt user)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Users
                                 where x.id == user.Id
                                 select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.active = (data.active == true ? false : true);
                        context.SaveChanges();
                    }

                    reports.ActionReport("ChangeStatusUserDone", user.activeUser, "ChangeStatusUser");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "ChangeStatusUser");

                return string.Empty;
            }
        }

        [HttpPut]
        [Route("DeleteProfilePicture")]
        public string DeleteProfilePicture(UserEnt user)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Users
                                where x.id == user.Id
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.profilePicture = null;
                        context.SaveChanges();
                    }

                    reports.ActionReport("DeleteProfilePictureDone", user.activeUser, "DeleteProfilePicture");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "DeleteProfilePicture");

                return string.Empty;
            }
        }

        [HttpGet]
        [Route("UserData")]
        public Users UserData(long q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.Users
                                 where x.id == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "UserData");

                return null;
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public string UpdateUser(UserEnt user)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Users
                                 where x.id == user.Id
                                 select x).FirstOrDefault();

                    if (data != null)
                    {
                        //De momento se hace por Linq, es mejor hacer un SP y así poder actualizar el tipo de usuario tambien
                        data.name = user.Name;
                        data.lastname = user.Lastname;
                        data.email = user.Email;
                        data.phoneNumber = user.PhoneNumber;

                        if (user.ProfilePicture != null)
                        {
                            data.profilePicture = user.ProfilePicture;
                        }


                        context.SaveChanges();
                    }

                    reports.ActionReport("UpdateUserDone", user.activeUser, "UpdateUser");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "UpdateUser");

                return string.Empty;
            }
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public string RecoverPassword(UserEnt user)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Users
                                 where x.email == user.Email
                                 && x.phoneNumber == user.PhoneNumber
                                 && x.birthdate == user.Birthdate
                                 select x).FirstOrDefault();


                    //Generate password
                    string pass = GeneratePassword();
                    //Encrypt password
                    string encryptPass = EncryptPassword(pass);

                    data.password = encryptPass;
                    context.SaveChanges();

                    if (data != null)
                    {
                        //Send an email to recover the password
                        string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "Templates\\RecoverPassword.html";
                        string html = File.ReadAllText(urlHtml);
                        html = html.Replace("@@Name", data.name + " " + data.lastname);
                        html = html.Replace("@@Password", pass);

                        mailService.SendEmail(user.Email, "Recuperación de contraseña SAEE-ELEC", html);

                        return "OK";
                    }

                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "RecoverPassword");

                return string.Empty;
            }
        }


        /***********************Users List***********************/
        [HttpGet]
        [Route("UsersList")]
        public List<Users> UsersList()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Users
                            select x).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "UsersList");

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
                using (var context = new SAEEEntities())
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
                reports.ErrorReport(errorDescription, 1, "ListUsersTypes");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("ListSpecialties")]
        public List<System.Web.Mvc.SelectListItem> ListSpecialties()
        {
            try
            {
                using (var context = new SAEEEntities())
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
                reports.ErrorReport(errorDescription, 1, "ListSpecialties");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }



   
        /***********************Methods***********************/
        private static string GeneratePassword()
        {
            //Create a random password that the user can then change since the account will be created by an administrator
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder pass = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(characters.Length);
                pass.Append(characters[index]);
            }

            return pass.ToString();
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
