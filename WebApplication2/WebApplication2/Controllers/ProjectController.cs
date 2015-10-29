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
            ViewData["AccountId"] = ListofProjects.AccountId;
            ViewData["Rank"] = ListofProjects.Rank;


            //Needs to go to the page that allows them to add a project
            return View("AddProjects", myModel);
        }

        public ActionResult SumbitProjet()
        {

            //Needs to add the project to the tables and inform the user the project was entered successfully
            return View( );
        }

        public ActionResult AddtoTable(int id)
        {
            string result;
            int AID = ViewBag.AccountId;
            string Role = ViewBag.Rank;
            result = AddAccount(AID, Role, id);
            ViewData["Output"] = result;
            return View("SuccessPage");
        }


        public List<administrationV2> possibleAdmins()
        {

            List<administrationV2> possibleAdmins = db.administrationV2.ToList();

            return possibleAdmins;

        }

        public List<memberTableV2> possibleMembers()
        {
            List<memberTableV2> possibleMembers = db.memberTableV2.ToList();

            return possibleMembers;
        }

        public List<ProjectTable> possibleProjects()
        {
            List<ProjectTable> possibleProjects = db.ProjectTables.ToList();
            //List<ProjectTable> outputProjects = new List<ProjectTable>();

            /*foreach(ProjectTable poss in possibleProjects)
            {
                for(int k = 0; k < Search.ProjectId.Length; k++)
                {
                    if (poss.ProjectId == Search.ProjectId[k])
                    {
                        ProjectTable input = new ProjectTable();
                        input.ProjectId = poss.ProjectId;
                        input.ProcessModelChosen = poss.ProcessModelChosen;
                        outputProjects.Add(input);
                    }
                }
                
            }*/

            return possibleProjects;
        }


        private string AddAccount(int id, string Rank, int projId)
        {
            try
            {
                db.JunctionTableProjectAndAccounts.Add(new JunctionTableProjectAndAccount
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