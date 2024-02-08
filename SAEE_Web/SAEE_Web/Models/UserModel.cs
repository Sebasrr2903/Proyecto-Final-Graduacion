using Newtonsoft.Json;
using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace SAEE_Web.Models
{
    public class UserModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public UserEnt Login(UserEnt user)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "Login";
                JsonContent contenido = JsonContent.Create(user);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<UserEnt>().Result;
            }
        }

        public string RegisterUser(UserEnt user)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterUser";
                JsonContent contenido = JsonContent.Create(user);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        /***********************Users List***********************/
        public List<UserEnt> UsersList()
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "UsersList";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<UserEnt>>().Result;
            }
        }

        /***********************Select List Item***********************/
        public List<SelectListItem> ListUsersTypes()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "ListUsersTypes";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> ListSpecialties()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "ListSpecialties";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }
    }
}