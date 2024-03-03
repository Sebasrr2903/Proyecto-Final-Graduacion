using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;
using System.Web.Mvc;

namespace SAEE_Web.Models
{
    public class EnrolledCoursesModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public List<SelectListItem> SelectListCoursesAvailable()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "SelectListCoursesAvailable";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<SelectListItem> SelectListStudents()
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "SelectListStudents";
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public string EnrolledStudent(EnrolledCoursesEnt enrolledCourses)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "EnrolledStudent";
                JsonContent content = JsonContent.Create(enrolledCourses);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        
        public List<EnrolledCoursesEnt> EnrolledCoursesPerStudent(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "EnrolledCoursesPerStudent?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<EnrolledCoursesEnt>>().Result;
            }
        }
        
        public List<EnrolledCoursesEnt> EnrolledCoursesPerTeacher(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "EnrolledCoursesPerTeacher?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<EnrolledCoursesEnt>>().Result;
            }
        }
        
        public List<EnrolledCoursesEnt> SpecificCourse(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "SpecificCourse?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<EnrolledCoursesEnt>>().Result;
            }
        }
    }
}