using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAEE_API.Entities
{
    public class Reports
    {
        //FILL IN ERROR TABLE AFTER FALLING INTO A CATCH
        public void ErrorReport(string errorDescription, int activeUser, string origin)
        {
            using (var context = new SAEEEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var error = new ErrorReport();

                error.date = DateTime.Now;
                error.userId = activeUser; //Use 0 when there is no active user
                error.errorDescription = errorDescription;
                error.origin = origin;

                context.ErrorReport.Add(error);
                context.SaveChanges();
            }
        }

        //FILL IN ACTION TABLE AFTER AFTER YOU MAKE ANY CHANGES
        public void ActionReport(string actionDescription, int activeUser, string origin)
        {
            using (var context = new SAEEEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;

                var action = new ActionReport();
                action.date = DateTime.Now;
                action.userId = activeUser; //Use 0 when there is no active user
                action.actionDescription = actionDescription;
                action.origin = origin;

                context.ActionReport.Add(action);
                context.SaveChanges();
            }
        }
    }
}