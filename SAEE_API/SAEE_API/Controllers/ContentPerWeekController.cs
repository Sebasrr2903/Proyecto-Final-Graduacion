using SAEE_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAEE_API.Controllers
{
    public class ContentPerWeekController : ApiController
    {
        //To insert into errors and actions table
        Reports reports = new Reports();

        [HttpGet]
        [Route("GetContentPerWeek")]
        public ContentPerWeek GetContentPerWeek(int q)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.ContentPerWeek
                                 where x.id == q
                                 select x).FirstOrDefault();

                    return datos;
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "GetContentPerWeek");

                return null;
            }
        }

        [HttpPut]
        [Route("UpdateContentPerWeek")]
        public string UpdateContentPerWeek(ContentPerWeekEnt contentPerWeek)
        {
            try
            {
                using (var context = new SAEEEntities())
                {
                    var data = (from x in context.ContentPerWeek
                                where x.id == contentPerWeek.Id
                                select x).FirstOrDefault();

                    if (data != null)
                    {
                        data.header = contentPerWeek.Header;
                        data.content = contentPerWeek.Content;

                        context.SaveChanges();
                    }

                    reports.ActionReport("UpdateContentPerWeekDone", contentPerWeek.activeUser, "UpdateContentPerWeek");

                    return "OK";
                }
            }
            catch (Exception e)
            {
                string errorDescription = e.Message.ToString();
                reports.ErrorReport(errorDescription, 1, "UpdateContentPerWeek");

                return string.Empty;
            }
        }



        




    }
}
