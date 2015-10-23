using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult ExistingProjects()
        {
            
            //Needs to go get the Projects the user is in than display those projects
            return View("ExistingProjects");
        }

        public ActionResult AddProjects()
        {

            //Needs to go to the page that allows them to add a project
            return View("ExisingProjects");
        }

        public ActionResult SumbitProjet()
        {
            //Needs to add the project to the tables and inform the user the project was entered successfully
            return View( );
        }
    }
}