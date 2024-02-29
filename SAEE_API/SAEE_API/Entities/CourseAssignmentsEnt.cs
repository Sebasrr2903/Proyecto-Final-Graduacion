using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class CourseAssignmentsEnt
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime AssignmentDeadline { get; set; }
        public bool AssignmentActive { get; set; }
        public int AssignmentWeek { get; set; }



    }
}