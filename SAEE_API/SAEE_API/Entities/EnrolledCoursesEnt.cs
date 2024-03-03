using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class EnrolledCoursesEnt
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        //Enrolled Courses Per Student 
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string TechearName { get; set; }
        public int CourseOriginId { get; set; }

        public int AssignmentId { get; set; }
        public int WeekId { get; set; }
        public int WeekNum { get; set; }
        public int ContentPerWeekId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string CourseSchedule { get; set; }

        public int TeacherExperience { get; set; }
        public string TeacherSpecialty { get; set; }

        
            




    }
}