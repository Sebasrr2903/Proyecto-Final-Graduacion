using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class CoursesEnt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuota { get; set; }

        public bool Active { get; set; }

        //For reports
        public int activeUser { get; set; }
    }
}