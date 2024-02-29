using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class CourseTasksController : ApiController
    {

        //To insert into errors and actions table
        Reports reports = new Reports();


        [HttpPost]
        [Route("RegisterCourseTasks")]
        public string RegisterCourseTasks(CoursesTasksEnt courseTasks)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var tasks = new CourseTasks();
                    tasks.name = courseTasks.Name;
                    tasks.description = courseTasks.Description;
                    tasks.file = courseTasks.File;
                    tasks.deadline = DateTime.Now; 



                   context.CourseTasks.Add(tasks);
                    context.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, courseTasks.activeUser, "RegisterCourseTasks");

                return string.Empty;
            }
        }
    }
}
