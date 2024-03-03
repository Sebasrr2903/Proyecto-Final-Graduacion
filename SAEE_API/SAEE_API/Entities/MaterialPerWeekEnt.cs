using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class MaterialPerWeekEnt
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public byte[] MaterialFile { get; set; }
        public string MaterialExtension { get; set; }
        public int WeekId { get; set; }

        //For reports
        public int activeUser { get; set; }
    }
}