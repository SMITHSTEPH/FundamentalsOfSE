using WebApplication2.Models;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ProjectController : Controller
    { 

        // GET: Project
        [HttpGet]
        public ActionResult ExistingProjects(Account User)
        {
            Project ListofProjects = new Project();

            ListofProjects = ListofProjects.UsersProjects(User);
            //Needs to go get the Projects the user is in than display those projects
            return View("ExistingProjects", ListofProjects);
        }

        
        public ActionResult PasstoAdd(Project something)
        {

            //Needs to go to the page that allows them to add projects 
            return View("AddProjects", something);
        }

        public ActionResult SumbitProjet()
        {
            //Needs to add the project to the tables and inform the user the project was entered successfully
            return View( );
        }



    }
}