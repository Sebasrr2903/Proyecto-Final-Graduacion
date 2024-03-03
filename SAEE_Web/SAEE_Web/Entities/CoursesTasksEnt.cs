using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_Web.Entities
{
    public class CoursesTasksEnt

    {
        public int activeUser { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] File { get; set; }
        public string FileExtension { get; set; }
        public System.DateTime DeliveredOn { get; set; }
        public int AssignmentId { get; set; }
        public int WeekId { get; set; }
        public int StudentId { get; set; }

    }
}