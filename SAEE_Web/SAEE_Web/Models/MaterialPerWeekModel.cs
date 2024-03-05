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
    public class MaterialPerWeekModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public List<MaterialPerWeekEnt> SpecificMaterial(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "SpecificMaterial?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<MaterialPerWeekEnt>>().Result;
            }
        }

        public string RegisterWeekMaterial(MaterialPerWeekEnt material)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterWeekMaterial";
                JsonContent content = JsonContent.Create(material);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public MaterialPerWeekEnt GetMaterial(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "GetMaterial?q=" + q;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<MaterialPerWeekEnt>().Result;
                
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

        public void DeleteMaterial(int q)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "DeleteMaterial?q=" + q;
                var resp = client.DeleteAsync(url).Result;
            }
        }







    }
}