using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_Web.Entities
{
    public class CourseAvailableEnt
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public int ScheduleId { get; set; }
        public int EnrolledStudents { get; set; }
        public int DurationInWeeks { get; set; }
        public bool Active { get; set; }
    }
}