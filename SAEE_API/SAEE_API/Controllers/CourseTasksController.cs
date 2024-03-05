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

        [HttpGet]
        [Route("TasksData")]
        public CourseTasks TasksData(long q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.CourseTasks
                                 where x.id == q
                                 select x).FirstOrDefault();
                                             
                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "TasksData");

                return null;
            }
        }

        [HttpPost]
        [Route("RegisterCourseTasks")]
        public string RegisterCourseTasks(CoursesTasksEnt courseTasks)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    //Agregar validacioines para que no se pueda subir 2 veces la tarea sino más bien se edite o elimine
                    var tasks = new CourseTasks();
                    tasks.name = courseTasks.Name;
                    tasks.description = courseTasks.Description;
                    tasks.file = courseTasks.File;
                    tasks.fileExtension = courseTasks.FileExtension;
                    tasks.deliveredOn = DateTime.Now;
                    tasks.assignmentId = courseTasks.AssignmentId;
                    tasks.studentId = courseTasks.activeUser; 

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

        [HttpGet]
        [Route("SpecificAssignment")]
        public object SpecificAssignment(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    return (from week in context.Weeks
                            join courseAssignments in context.CourseAssignments on week.id equals courseAssignments.weekId
                            where week.id == q
                            select new
                            {
                                AssignmentId = courseAssignments.id,
                                AssignmentName = courseAssignments.name,
                                AssignmentDescription = courseAssignments.indications,
                                AssignmentDeadline = courseAssignments.deadline,
                                AssignmentActive = courseAssignments.active,
                                AssignmentWeek = courseAssignments.weekId
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SpecificAssignment");

                return new List<EnrolledCoursesEnt>();
            }
        }

        [HttpGet]
        [Route("DeliveredCoursetasks")]
        public List<CourseTasks> DeliveredCoursetasks(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.CourseTasks
                                 where x.assignmentId == q
                                 select x).ToList();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "DeliveredCoursetasks");

                return new List<CourseTasks>();
            }
        }


    }
}
