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
					var tasks = new CourseTasks();
					tasks.name = courseTasks.Name;
					tasks.description = courseTasks.Description;
					tasks.file = courseTasks.File;
					tasks.fileExtension = courseTasks.FileExtension;
					tasks.deliveredOn = DateTime.Now;
					tasks.assignmentId = courseTasks.AssignmentId;
					tasks.studentId = courseTasks.StudentId;

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
        public object DeliveredCoursetasks(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {

					context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.CourseTasks
                                 join studentData in context.Users on x.studentId equals studentData.id
                                 join grading in context.AssignmentGrading on x.id equals grading.taskId into gj
								 from grading in gj.DefaultIfEmpty()
								 where x.assignmentId == q 
                                 select new
                                 {
                                     Id = x.id,
                                     Name = x.name,
                                     Description = x.description,
                                     File = x.file,
                                     FileExtension = x.fileExtension,
                                     DeliveredOn = x.deliveredOn,
                                     StudentId = x.studentId,
                                     StudentFullName = studentData.name + " " + studentData.lastname,
									 GradingId = grading != null ? grading.id : (int?)0,
									 GradingScore = grading != null ? grading.score : (double?)0
								 }).ToList();

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

        [HttpGet]
        [Route("GetTask")]
        public object GetTask(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.CourseTasks
                                 where x.id == q
                                 where x.id == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "GetTask");

                return null;
            }
        }


        [HttpPost]
        [Route("GradeAssignment")]
        public string GradeAssignment(AssignmentGradingEnt grade)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    //Validar que solo se esté calificando una vez la entrega

                    var grades = new AssignmentGrading();
                    grades.taskId = grade.TaskId;
                    grades.score = grade.Score;
                    grades.performanceDescription = grade.PerformanceDescription;

                    context.AssignmentGrading.Add(grades);
                    context.SaveChanges();

                    reports.ActionReport("GradeAssignmentDone", grade.activeUser, "GradeAssignment");

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, grade.activeUser, "GradeAssignment");

                return string.Empty;
            }

        }


		[HttpGet]
		[Route("GradingData")]
		public AssignmentGrading GradingData(int q)
		{
			try
			{
				using (var context = new SAEEEntities())
				{
					context.Configuration.LazyLoadingEnabled = false;
					var datos = (from x in context.AssignmentGrading
								 where x.id == q
								 select x).FirstOrDefault();

					return datos;
				}
			}
			catch (Exception e)
			{
				string errorDescription = e.Message.ToString();
				reports.ErrorReport(errorDescription, 1, "GradingData");

				return null;
			}
		}


		[HttpPut]
		[Route("UpdateGradeAssignment")]
		public string UpdateGradeAssignment(AssignmentGradingEnt grading)
		{
			try
			{
				using (var context = new SAEEEntities())
				{
					var data = (from x in context.AssignmentGrading
								where x.id == grading.Id
								select x).FirstOrDefault();

					if (data != null)
					{
						//De momento se hace por Linq, es mejor hacer un SP y así poder actualizar el tipo de usuario tambien
						data.score = grading.Score;
						data.performanceDescription = grading.PerformanceDescription;

						context.SaveChanges();
					}

					reports.ActionReport("UpdateGradeAssignmentDone", grading.activeUser, "UpdateGradeAssignment");
					return "OK";
				}
			}
			catch (Exception e)
			{
				string errorDescription = e.Message.ToString();
				reports.ErrorReport(errorDescription, 1, "UpdateGradeAssignment");

				return string.Empty;
			}
		}









	}
}
