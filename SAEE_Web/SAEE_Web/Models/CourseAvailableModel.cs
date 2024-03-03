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
    public class CourseAvailableModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public List<SelectListItem> SelectListCourses()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "SelectListCourses";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> SelectListTeacher()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "SelectListTeacher";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> SelectListSchedule()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "SelectListSchedule";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public string RegisterCourseAvailable(CourseAvailableEnt courseAvailable)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterCourseAvailable";
                JsonContent content = JsonContent.Create(courseAvailable);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }





    }

}