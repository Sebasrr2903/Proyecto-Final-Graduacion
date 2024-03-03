using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class ContentPerWeekEnt
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }

        //For reports
        public int activeUser { get; set; }
    }
}