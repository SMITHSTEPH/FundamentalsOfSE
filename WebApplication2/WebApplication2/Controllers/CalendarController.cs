using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using WebApplication2.Models;
using System.Data.Entity;
namespace WebApplication2.Controllers
{
    public class CalendarController : Controller
    {


		private AppointmentContext db = new AppointmentContext();
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */
            
 
           // scheduler.InitialDate = new DateTime(2012, 09, 03);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }



		public ContentResult Data()
		{

			string queryM = "SELECT * FROM Appointment WHERE projectNumber=" + GlobalVariables.ProjectID_cal;
			var apps = db.Appointments.SqlQuery(queryM).ToList();

			return new SchedulerAjaxData(apps);
		}


		public ActionResult Save(int? id, FormCollection actionValues)
		{
			var action = new DataAction(actionValues);
			var apps = db.Appointments.ToList();
			apps.ToString();

			try
			{
				var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
		
				switch (action.Type)
				{
					case DataActionTypes.Insert:
						db.Appointments.Add(new Appointment
						{	

							Description = changedEvent.Description,
							StartDate = changedEvent.StartDate,
							EndDate = changedEvent.EndDate,
							projectNumber = GlobalVariables.ProjectID_cal

						});
						//db.SaveChanges();
						break;
					case DataActionTypes.Delete:
						if (GlobalVariables.role_cal == "Leader") {
							db.Entry(changedEvent).State = EntityState.Deleted;

						}
						else
						{
							TempData["notice"] = "Only leader can delete an event.";
						}
						
						break;
					default:// "update"  
						if (GlobalVariables.role_cal == "Leader")
						{
							db.Entry(changedEvent).State = EntityState.Modified;
						}
						else
						{
							TempData["notice"] = "Only leader can modify an event.";
						}
						
						break;
				}
				db.SaveChanges();
				action.TargetId = changedEvent.Id;
			}
			catch (Exception a)
			{

				System.Console.WriteLine(a);
				
			}

			return (new AjaxSaveResponse(action));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}


    }
}

