using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class CourseAssignmentController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();

        [HttpPost]
        [Route("RegisterAssignment")]
        public string RegisterAssignment(CourseAssignmentsEnt course)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var courses = new CourseAssignments();
                    courses.name = course.AssignmentName;
                    courses.active = course.AssignmentActive;
                    courses.deadline = course.AssignmentDeadline;
                    courses.indications = course.AssignmentDescription;
                    courses.weekId = course.AssignmentWeek;

                    context.CourseAssignments.Add(courses);
                    context.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, course.activeUser, "RegisterAssignment");

                return string.Empty;
            }
        }

        [HttpGet]
        [Route("GetAssignment")]
        public object GetAssignment(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.CourseAssignments
                                 where x.id == q
                                 select new
                                 {
                                     AssignmentId = x.id,
                                     AssignmentName = x.name,
                                     AssignmentDescription = x.indications,
                                     AssignmentDeadline = x.deadline,
                                     AssignmentActive = x.active,
                                     AssignmentWeek = x.weekId
                                 }).FirstOrDefault();
                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "GetContentPerWeek");

                return null;
            }
        }



        
    }
}
