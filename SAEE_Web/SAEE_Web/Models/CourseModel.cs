using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace SAEE_Web.Models
{
    public class CourseModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public List<CoursesEnt> CoursesList()
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "CoursesList";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<CoursesEnt>>().Result;
            }
        }

        public CoursesEnt CourseData(long q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "CourseData?q=" + q;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<CoursesEnt>().Result;
            }
        }

        public string UpdateCourse(CoursesEnt course)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "UpdateCourse";
                JsonContent content = JsonContent.Create(course);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ChangeStatusCourse(CoursesEnt course)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "ChangeStatusCourses";
                JsonContent content = JsonContent.Create(course);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string RegisterCourse(CoursesEnt course)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterCourse";
                JsonContent content = JsonContent.Create(course);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}