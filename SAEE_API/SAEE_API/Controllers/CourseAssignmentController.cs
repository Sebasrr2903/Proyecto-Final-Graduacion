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
                reports.ErrorReport(errorDescription, 1, "GetAssignment");

                return null;
            }
        }

        [HttpPut]
        [Route("ChangeStatusAssignment")]
        public string ChangeStatusAssignment(CourseAssignmentsEnt assignment)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.CourseAssignments
                                where x.id == assignment.AssignmentId
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.active = (data.active == true ? false : true);
                        context.SaveChanges();
                    }

                    reports.ActionReport("ChangeStatusAssignmentDone", assignment.activeUser, "ChangeStatusAssignment");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, assignment.activeUser, "ChangeStatusAssignment");

                return string.Empty;
            }
        }

        [HttpPut]
        [Route("UpdateAssignment")]
        public string UpdateAssignment(CourseAssignmentsEnt assignment)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.CourseAssignments
                                where x.id == assignment.AssignmentId
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.name = assignment.AssignmentName;
                        data.indications = assignment.AssignmentDescription;
                        data.deadline = assignment.AssignmentDeadline;

                        context.SaveChanges();
                    }

                    reports.ActionReport("UpdateAssignmentDone", assignment.activeUser, "UpdateAssignment");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, assignment.activeUser, "UpdateAssignment");

                return string.Empty;
            }
        }




        
        



    }
}
