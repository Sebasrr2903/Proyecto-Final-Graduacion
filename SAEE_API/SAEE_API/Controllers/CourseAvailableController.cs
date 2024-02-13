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
                reports.ErrorReport(errorDescription, 0, "SelectListCourses");

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
                    var data = (from x in context.TeacherData select x).ToList();
                    var list = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var x in data)
                    {
                        list.Add(new System.Web.Mvc.SelectListItem { Value = x.teacherId.ToString(), Text = x.specialty.ToString ()});
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 0, "SelectListTeacher");

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
                reports.ErrorReport(errorDescription, 0, "SelectListSchedule");

                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
    }
}
