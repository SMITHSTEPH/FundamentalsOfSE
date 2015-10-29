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

            /*if(ListofProjects.Rank == "Admin")
            {
                myModel.administrationV2 = possibleAdmins(ListofProjects.AccountId);

            }
            else
            {
                myModel.memberTableV2 = possibleMembers(ListofProjects.AccountId);
            }*/

            //ViewData["Rank"] = ListofProjects.Rank;

            JunctionTableProjectAndAccount table = new JunctionTableProjectAndAccount();
            List<JunctionTableProjectAndAccount> output = new List<JunctionTableProjectAndAccount>();
            table.AID = ListofProjects.AccountId;
            table.Role = ListofProjects.Rank;
            output.Add(table);
            myModel.JunctionTableProjectAndAccount = table;

            //Needs to go to the page that allows them to add a project
            return View("AddProjects", myModel);
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


        public List<administrationV2> possibleAdmins(int AId)
        {

            List<administrationV2> possibleAdmins = db.administrationV2.ToList();
            List<administrationV2> outputProjects = new List<administrationV2>();

            foreach(administrationV2 poss in possibleAdmins)
            {
                
                    if (poss.Id == AId)
                    {
                        administrationV2 input = new administrationV2();
                        input = poss;
                        outputProjects.Add(input);
                    }
                
                
            }

            return possibleAdmins;

        }

        public List<memberTableV2> possibleMembers(int AId)
        {
            List<memberTableV2> possibleMembers = db.memberTableV2.ToList();
            List<memberTableV2> outputProjects = new List<memberTableV2>();

            foreach (memberTableV2 poss in possibleMembers)
            {

                if (poss.Id == AId)
                {
                    memberTableV2 input = new memberTableV2();
                    input = poss;
                    outputProjects.Add(input);
                }


            }

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