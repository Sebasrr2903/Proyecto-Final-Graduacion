using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class ScheduleController : ApiController
    {
        Reports reports = new Reports();

        [HttpGet]
        [Route("ScheduleList")]
        public List<Schedule> ScheduleList()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Schedule
                            select x).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "ScheduleList");

                return new List<Schedule>();
            }
        }

        [HttpPost]
        [Route("RegisterSchedule")]
        public string RegisterSchedule(ScheduleEnt course)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var courses = new Schedule();
                    courses.day = course.Day;
                    courses.startTime = course.StartTime;
                    courses.endTime = course.EndTime;

                    context.Schedule.Add(courses);
                    context.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, course.activeUser, "RegisterCourse");

                return string.Empty;
            }

        }

    }
}
