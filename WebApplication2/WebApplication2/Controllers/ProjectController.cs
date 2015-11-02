using WebApplication2.Models;
using System.Web.Mvc;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class ProjectController : Controller
    {
        private RegistrationEntities1 db = new RegistrationEntities1();


        // GET: Project
        public ActionResult ExistingProjects(Account User)
        {
            Project ListofProjects = new Project();

            ListofProjects = ListofProjects.UsersProjects(User);
            //Needs to go get the Projects the user is in than display those projects
            return View("ExistingProjects", ListofProjects);
        }

        public ActionResult AddProjects(Project ListofProjects)
        {
            dynamic myModel = new ExpandoObject();
            myModel.ProjectTable = possibleProjects();

            JunctionTableProjectAndAccountV2 table = new JunctionTableProjectAndAccountV2();
            List<JunctionTableProjectAndAccountV2> output = new List<JunctionTableProjectAndAccountV2>();
            table.AID = ListofProjects.AccountId;
            table.Role = ListofProjects.Rank;
            output.Add(table);
            myModel.JunctionTableProjectAndAccount = table;

            //Needs to go to the page that allows them to add a project
            return View("AddProjects", myModel);
        }

        public ActionResult AddPeople(Project ListofProjects)
        {

        }

        public ActionResult SumbitProjet()
        {

            //Needs to add the project to the tables and inform the user the project was entered successfully
            return View( );
        }

        public ActionResult AddtoTable(int id, string role, int Aid)
        {
            string result;
            int AID = Aid;
            string Role = role;
            result = AddAccount(AID, Role, id);
            ViewData["Output"] = result;
            return View("SuccessPage");
        }

        public List<ProjectTable> possibleProjects()
        {
            List<ProjectTable> possibleProjects = db.ProjectTables.ToList();

            return possibleProjects;
        }


        private string AddAccount(int id, string Rank, int projId)
        {
            try
            {
                db.JunctionTableProjectAndAccountV2.Add(new JunctionTableProjectAndAccountV2
                {
                    AID = id,
                    Role = Rank,
                    PId = projId

                });
                db.SaveChanges();
                return "Success";
            }
            catch
            {
                return "Fail";
            }
        }

    }
}