using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class CourseAssignmentsEnt
    {    
        public int activeUser { get; set; }

        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime AssignmentDeadline { get; set; }
        public bool AssignmentActive { get; set; }
        public int AssignmentWeek { get; set; }

		//Coursetask
		public int TaskId { get; set; }
		public string TaskName { get; set; }
		public string TaskDescription { get; set; }
		public byte[] TaskFile { get; set; }
		public string TaskFileExtension { get; set; }
		public System.DateTime TaskDeliveredOn { get; set; }

		//AssignmentGrading
		public int GradingId { get; set; }
		public float GradingScore { get; set; }
		public string GradingPerformanceDescription { get; set; }
	}
}