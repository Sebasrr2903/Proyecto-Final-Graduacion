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
    }
}