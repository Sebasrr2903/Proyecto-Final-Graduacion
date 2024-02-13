using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_Web.Entities
{
    public class ScheduleEnt
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}