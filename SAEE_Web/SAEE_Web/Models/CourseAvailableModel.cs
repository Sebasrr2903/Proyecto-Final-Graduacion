using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }

}