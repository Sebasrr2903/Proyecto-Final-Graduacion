using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_Web.Entities
{
    public class ViewModelCourse_Task : IEnumerable
    {
        public List<EnrolledCoursesEnt> EnrolledCourses { get; set; }
        public List<CourseAssignmentsEnt> CourseAssignments { get; set; }

        public IEnumerator GetEnumerator()
        {
            return EnrolledCourses.GetEnumerator();
        }

        public IEnumerator<CourseAssignmentsEnt> GetCourseAssignmentsEnumerator()
        {
            return CourseAssignments.GetEnumerator();
        }
    }
}