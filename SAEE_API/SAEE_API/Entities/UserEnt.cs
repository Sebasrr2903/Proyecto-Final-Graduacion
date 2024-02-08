using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class UserEnt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string ProfilePicture { get; set; }
        public int UserType { get; set; }

        //For Teacher
        public int Specialty { get; set; }
        public int ExperienceYears { get; set; }

        //For reports
        public int activeUser { get; set; }
    }
}