using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;

namespace SAEE_Web.Models
{
    public class ContentPerWeekModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];

        public ContentPerWeekEnt GetContentPerWeek(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "GetContentPerWeek?q=" + q;
                var resp = client.GetAsync(url).Result;
                return resp.Content.ReadFromJsonAsync<ContentPerWeekEnt>().Result;

            }
        }

        public string UpdateContentPerWeek(ContentPerWeekEnt contentPerWeek)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "UpdateContentPerWeek";
                JsonContent content = JsonContent.Create(contentPerWeek);
                var resp = client.PutAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}