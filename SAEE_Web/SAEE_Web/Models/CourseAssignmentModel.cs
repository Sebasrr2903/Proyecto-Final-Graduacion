using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;

namespace SAEE_Web.Models
{
    public class CourseAssignmentModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public string RegisterAssignment(CourseAssignmentsEnt course)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterAssignment";
                JsonContent content = JsonContent.Create(course);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public CourseAssignmentsEnt GetAssignment(int q, int a)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "GetAssignment?q=" + q + "&a=" + a;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<CourseAssignmentsEnt>().Result;

            }
        }

        public string ChangeStatusAssignment(CourseAssignmentsEnt assignment)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "ChangeStatusAssignment";
                JsonContent content = JsonContent.Create(assignment);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string UpdateAssignment(CourseAssignmentsEnt assignment)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "UpdateAssignment";
                JsonContent content = JsonContent.Create(assignment);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }





    }
}