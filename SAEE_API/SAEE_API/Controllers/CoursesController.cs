using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SAEE_API;
using SAEE_API.Entities;

namespace SAEE_API.Controllers
{
    public class CoursesController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();

        /***********************Cuorses List***********************/
        [HttpGet]
        [Route("CoursesList")]
        public List<Courses> CoursesList()
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Courses
                            select x).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "CoursesList");

                return new List<Courses>();
            }
        }



        [HttpPut]
        [Route("ChangeStatusCourses")]
        public string ChangeStatusCourse(CoursesEnt course)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Courses
                                where x.id == course.Id
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.active = (data.active == true ? false : true);
                        context.SaveChanges();
                    }

                    reports.ActionReport("ChangeStatusCoursesDone",course.activeUser, "ChangeStatusCourses");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "ChangeStatusCourses");

                return string.Empty;
            }
        }




        [HttpGet]
        [Route("CourseData")]
        public Courses CourseData(long q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.Courses
                                 where x.id == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "CourseData");

                return null;
            }
        }
        [HttpPut]
        [Route("UpdateCourse")]
        public string UpdateCourse(CoursesEnt course)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.Courses
                                where x.id == course.Id
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        //De momento se hace por Linq
                        data.name = course.Name;
                        data.description = course.Description;
                        data.availableQuota = course.AvailableQuota;
                        data.active = course.Active;
                  

                        context.SaveChanges();
                    }

                    reports.ActionReport("UpdateCourseDone", course.activeUser, "UpdateCourse");
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "UpdateCourse");

                return string.Empty;
            }
        }


        [HttpPost]
        [Route("RegisterCourse")]
        public string RegisterCourse(CoursesEnt course)
        {
              try
            {
                using (var context = new SAEEEntities())
                {
                    var courses = new Courses();
                    courses.description = course.Description;
                    courses.active = course.Active;
                    courses.name = course.Name; 
                    courses.availableQuota = course.AvailableQuota;

                    context.Courses.Add(courses);
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