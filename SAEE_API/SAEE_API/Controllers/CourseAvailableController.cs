using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class CourseAvailableController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();
        //To send email
        MailService mailService = new MailService();


        [HttpGet]
        [Route("SelectListCourses")]
        public List<System.Web.Mvc.SelectListItem> SelectListCourses()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Courses select x).ToList();
                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.id.ToString(), Text = x.name });
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SelectListCourses");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }


        [HttpGet]
        [Route("SelectListTeacher")]
        public List<System.Web.Mvc.SelectListItem> SelectListTeacher()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from teacher in context.TeacherData
                                join users in context.Users on teacher.teacherId equals users.id
                                select new
                                {
                                    TeacherId = teacher.teacherId,
                                    TeacherName = users.name + " " + users.lastname
                                }).ToList();

                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.TeacherId.ToString(), Text = x.TeacherName});
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SelectListTeacher");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("SelectListSchedule")]
        public List<System.Web.Mvc.SelectListItem> SelectListSchedule()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Schedule select x).ToList();
                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.id.ToString(), Text = x.day + " " + x.startTime + " " + x.endTime});
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SelectListSchedule");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }


        [HttpPost]
        [Route("RegisterCourseAvailable")]
        public string RegisterCourseAvailable(CourseAvailableEnt courseAvailable)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var course = new CourseAvailable();
                    course.teacherId = courseAvailable.TeacherId;
                    course.courseId = courseAvailable.CourseId;
                    course.scheduleId = courseAvailable.ScheduleId;
                    course.enrolledStudents = 0; //Newly created (has no students yet)
                    course.durationInWeeks = courseAvailable.DurationInWeeks;
                    course.active = true;

                    context.CourseAvailable.Add(course);
                    context.SaveChanges();

                    int courseAvailableId = course.id;
                    //Create default course weeks
                    for (global::System.Int32 i = 0; i < courseAvailable.DurationInWeeks + 1; i++)
                    {
                        //Week 0 corresponds to the introduction and the rest will have content as appropriate
                        var week = new Weeks();
                        week.weekNum = i;
                        week.courseAvailableId = courseAvailableId;

                        context.Weeks.Add(week);
                        context.SaveChanges();
                    }

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "RegisterCourseAvailable");

                return string.Empty;
            }

        }
    }
}
