using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class MaterialPerWeekController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();

        [HttpGet]
        [Route("SpecificMaterial")]
        public object SpecificMaterial(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;

                    return (from week in context.Weeks
                            join materialPerWeek in context.MaterialPerWeek on week.id equals materialPerWeek.weekId
                            where week.id == q
                            select new
                            {
                                MaterialId = materialPerWeek.id,
                                MaterialName = materialPerWeek.name,
                                MaterialFile = materialPerWeek.file,
                                MaterialExtension = materialPerWeek.fileExtension,
                                WeekId = materialPerWeek.weekId
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "SpecificMaterial");

                return new List<EnrolledCoursesEnt>();
            }
        }

        [HttpPost]
        [Route("RegisterWeekMaterial")]
        public string RegisterWeekMaterial(MaterialPerWeekEnt material)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var materials = new MaterialPerWeek();
                    materials.name = material.MaterialName;
                    materials.file = material.MaterialFile;
                    materials.fileExtension = material.MaterialExtension;
                    materials.weekId = material.WeekId;

                    context.MaterialPerWeek.Add(materials);
                    context.SaveChanges();

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, material.activeUser, "RegisterWeekMaterial");

                return string.Empty;
            }
        }

        [HttpGet]
        [Route("GetMaterial")]
        public object GetMaterial(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.MaterialPerWeek
                                 where x.id == q
                                select new
                                {
                                    MaterialId = x.id,
                                    MaterialName = x.name,
                                    MaterialFile = x.file,
                                    MaterialExtension = x.fileExtension,
                                    WeekId = x.weekId
                                }).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "GetMaterial");

                return null;
            }
        }

        [HttpDelete]
        [Route("DeleteMaterial")]
        public void DeleteMaterial(long q)
        {
            using (var context = new SAEEEntities())
            {
                var datos = (from x in context.MaterialPerWeek
                             where x.id == q
                             select x).FirstOrDefault();

                context.MaterialPerWeek.Remove(datos);
                context.SaveChanges();
            }
        }
        

    }
}
