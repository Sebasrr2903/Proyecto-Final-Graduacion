﻿using SAEE_Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;

namespace SAEE_Web.Models
{
    public class ScheduleModel
    {
        public string urlAPI = ConfigurationManager.AppSettings["urlAPI"];
        //MODELSHEDULE
        public List<ScheduleEnt> ScheduleList()
        {
            using (var client = new HttpClient())
            {
                var url = urlAPI + "ScheduleList";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<ScheduleEnt>>().Result;
            }
        }


        public string RegisterSchedule(ScheduleEnt course)
        {
            using (var client = new HttpClient())
            {
                string url = urlAPI + "RegisterSchedule";
                JsonContent content = JsonContent.Create(course);
                var resp = client.PostAsync(url, content).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}