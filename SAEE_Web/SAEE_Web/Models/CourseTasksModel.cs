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
    public class CourseTasksModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public CoursesTasksEnt TasksData(long q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "TasksData?q=" + q;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<CoursesTasksEnt>().Result;
            }
        }

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

        public List<CoursesTasksEnt> DeliveredCoursetasks(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "DeliveredCoursetasks?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<CoursesTasksEnt>>().Result;
            }
        }

        public CoursesTasksEnt GetTask(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "GetTask?q=" + q;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<CoursesTasksEnt>().Result;

            }
        }

        public string GradeAssignment(AssignmentGradingEnt grade)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "GradeAssignment";
                JsonContent content = JsonContent.Create(grade);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }


		public AssignmentGradingEnt GradingData(int q)
		{
			using (var client = new HttpClient())
			{
				var url = urlAPI + "GradingData?q=" + q;
				var resp = client.GetAsync(url).Result;
				return resp.Content.ReadFromJsonAsync<AssignmentGradingEnt>().Result;
			}
		}

		public string UpdateGradeAssignment(AssignmentGradingEnt grading)
		{
			using (var client = new HttpClient())
			{
				string url = urlAPI + "UpdateGradeAssignment";
				JsonContent content = JsonContent.Create(grading);
				var resp = client.PutAsync(url, content).Result;
				return resp.Content.ReadFromJsonAsync<string>().Result;
			}
		}

		public CoursesTasksEnt Task(int q)
		{
			using (var client = new HttpClient())
			{
				var url = urlAPI + "Task?q=" + q;
				var resp = client.GetAsync(url).Result;
				return resp.Content.ReadFromJsonAsync<CoursesTasksEnt>().Result;
			}
		}
		public string UpdateCoursesTasks(CoursesTasksEnt courseTasks)
		{
			using (var client = new HttpClient())
			{
				string url = urlAPI + "UpdateCoursesTasks";
				JsonContent content = JsonContent.Create(courseTasks);
				var resp = client.PutAsync(url, content).Result;
				return resp.Content.ReadFromJsonAsync<string>().Result;
			}
		}

           public void DeleteTasks(int q)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "DeleteTasks?q=" + q;
                var resp = client.DeleteAsync(url).Result;
            }
        }


	}
}