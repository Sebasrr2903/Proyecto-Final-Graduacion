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
                    var data = (from courseAvailable in context.CourseAvailable
                                join courses in context.Courses on courseAvailable.courseId equals courses.id
                                join schedule in context.Schedule on courseAvailable.scheduleId equals schedule.id
                                join teacherData in context.Users on courseAvailable.teacherId equals teacherData.id
                                select new
                                {
                                    CourseName = courses.name,
                                    CourseId = courseAvailable.id,
                                    CourseSchedule = schedule.day + " " + schedule.startTime + " " + schedule.endTime,
                                    TechearName = teacherData.name + " " + teacherData.lastname,
                                    AvailableQuota = courses.availableQuota - courseAvailable.enrolledStudents
                                }).ToList();

                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.CourseId.ToString(), Text = x.CourseName + " || " + x.CourseSchedule + " || " + x.TechearName + " || " + x.AvailableQuota });
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
                    var actualCount = (from x in context.CourseAvailable
                                      where x.id == enrolledCourse.CourseId
                                      select x).FirstOrDefault();

                    var totalCount = (from x in context.Courses
                                       where x.id == actualCount.courseId
                                       select x).FirstOrDefault();

                    var availableQuota = totalCount.availableQuota - actualCount.enrolledStudents;

                    if (availableQuota > 0)
                    {
                        var enrolledCourses = new EnrolledCourses();
                        enrolledCourses.courseId = enrolledCourse.CourseId;
                        enrolledCourses.studentId = enrolledCourse.StudentId;


                        context.EnrolledCourses.Add(enrolledCourses);
                        context.SaveChanges();
                        
                        //Update EnrolledStudents in CourseAvailable
                        var CourseAvailable = (from x in context.CourseAvailable
                                           where x.id == enrolledCourse.CourseId
                                           select x).FirstOrDefault();

                        if (CourseAvailable != null)
                        {
                            CourseAvailable.enrolledStudents = CourseAvailable.enrolledStudents + 1;
                            context.SaveChanges();
                        }

                        return "OK";
                    }
                    else
                    {
                        return "El curso no tiene cupo disponible.";
                    }                  
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "EnrolledStudent");

                return string.Empty;
            }

        }

        [HttpGet]
        [Route("EnrolledCoursesPerStudent")]
        public object EnrolledCoursesPerStudent(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    return (from enrolledCourses in context.EnrolledCourses
                                join courseAvailable in context.CourseAvailable on enrolledCourses.courseId equals courseAvailable.id
                                join courses in context.Courses on courseAvailable.courseId equals courses.id
                                join teacherData in context.Users on courseAvailable.teacherId equals teacherData.id
                                where enrolledCourses.studentId == q
                                select new
                                {
                                    CourseId = enrolledCourses.courseId, //Course Available
                                    CourseOriginId = courseAvailable.courseId, //Courses
                                    CourseName = courses.name,
                                    CourseDescription = courses.description,
                                    TechearName = teacherData.name + " " + teacherData.lastname
                                }).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "EnrolledCoursesPerStudent");

                return new List<EnrolledCoursesEnt>();
            }
        }

        [HttpGet]
        [Route("SpecificCourse")]
        public object SpecificCourse(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    return (from courseAvailable in context.CourseAvailable
                            join week in context.Weeks on courseAvailable.id equals week.courseAvailableId
                            join contentPerWeek in context.ContentPerWeek on week.id equals contentPerWeek.weekId
                            join courses in context.Courses on courseAvailable.courseId equals courses.id
                            join teacher in context.Users on courseAvailable.teacherId equals teacher.id
                            join teacherData in context.TeacherData on courseAvailable.teacherId equals teacherData.teacherId
                            join specialties in context.Specialties on teacherData.specialty equals specialties.id
                            join schedule in context.Schedule on courseAvailable.scheduleId equals schedule.id
                            where courseAvailable.id == q
                            select new
                            {
                                CourseName = courses.name,
                                CourseDescription = courses.description,
                                TechearName = teacher.name + " " + teacher.lastname,
                                WeekNum = week.weekNum,
                                Header = contentPerWeek.header,
                                Content = contentPerWeek.content,
                                CourseSchedule = schedule.day + " " + schedule.startTime + " " + schedule.endTime,
                                TeacherExperience = teacherData.experienceYears,
                                TeacherSpecialty = specialties.description
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SpecificCourse");

                return new List<EnrolledCoursesEnt>();
            }
        }

    }
}
