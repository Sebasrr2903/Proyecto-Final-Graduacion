using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class EnrolledCoursesController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();
        //To send email
        MailService mailService = new MailService();

        [HttpGet]
        [Route("SelectListCoursesAvailable")]
        public List<System.Web.Mvc.SelectListItem> SelectListCoursesAvailable()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from course in context.CourseAvailable
                                join courseN in context.Courses on course.courseId equals courseN.id
                                join schedule in context.Schedule on course.scheduleId equals schedule.id
                                select new
                                {
                                    CourseName = courseN.name,
                                    CourseId = course.id,
                                    CourseSchedule = schedule.day + " " + schedule.startTime + " " + schedule.endTime
                                }).ToList();

                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.CourseId.ToString(), Text = x.CourseName + " " + x.CourseSchedule});
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "SelectListCoursesAvailable");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("SelectListStudents")]
        public List<System.Web.Mvc.SelectListItem> SelectListStudents()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Users
                                where x.userType != 1 
                                && x.userType != 2
                                select x).ToList();

                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.id.ToString(), Text = x.name + " " + x.lastname});
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "SelectListStudents");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }


        [HttpPost]
        [Route("EnrolledStudent")]
        public string EnrolledStudent(EnrolledCoursesEnt enrolledCourse)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var enrolledCourses = new EnrolledCourses();
                    enrolledCourses.courseId = enrolledCourse.CourseId;
                    enrolledCourses.studentId = enrolledCourse.StudentId;
                    

                    context.EnrolledCourses.Add(enrolledCourses);
                    context.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "EnrolledStudent");

                return string.Empty;
            }

        }

    }
}
