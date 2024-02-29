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
    public class CourseTasksModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];


        public string RegisterCourseTasks(CoursesTasksEnt courseTasks)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterCourseTasks";
                JsonContent content = JsonContent.Create(courseTasks);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<CourseAssignmentsEnt> SpecificAssignment(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "SpecificAssignment?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<CourseAssignmentsEnt>>().Result;
            }
        }

        

    }
}