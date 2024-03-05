using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_Web.Entities
{
    public class AssignmentGradingEnt
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public float Score { get; set; }
        public string PerformanceDescription { get; set; }

        //For reports
        public int activeUser { get; set; }

    }
}