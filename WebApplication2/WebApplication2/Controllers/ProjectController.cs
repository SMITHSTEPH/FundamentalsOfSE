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
            table.AID = ListofProjects.AccountId;
            table.Role = ListofProjects.Rank;
            myModel.JunctionTableProjectAndAccount = table;

            //Needs to go to the page that allows them to add a project
            return View("AddProjects", myModel);
        }

        public ActionResult AddPeople(Project ListofProjects)
        {
            return View("AddPeople", ListofProjects);
        }

        public ActionResult ApplyProjects(int id)
        {
            dynamic myModel = new ExpandoObject();
            myModel.MemberTableAdd = possibleMembersAdd(id);
            myModel.MemberTableRemove = possibleMembersRemove(id);
            ProjectTable table = new ProjectTable();
            table.ProjectId = id;
            myModel.ProjectTable = table;

            return View("ApplyProjects", myModel);
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

        public ActionResult RemoveTable(int id, string role, int Aid)
        {
            
            int AID = Aid;
            string Role = role;
            JunctionTableProjectAndAccountV2 toRemove = new JunctionTableProjectAndAccountV2();
            toRemove.AID = Aid;

            string query = "SELECT * FROM JunctionTableProjectAndAccountV2 WHERE AID= " + Aid + " AND Role= '" + Role + "' AND PId = " + id;
            JunctionTableProjectAndAccountV2 JT = db.JunctionTableProjectAndAccountV2.SqlQuery(query).SingleOrDefault();
            
            db.JunctionTableProjectAndAccountV2.Remove(JT);
            db.SaveChanges();
            
            return View("SuccessPage");
        }



        public List<ProjectTable> possibleProjects()
        {
            List<ProjectTable> possibleProjects = db.ProjectTables.ToList();

            return possibleProjects;
        }

        public List<memberTableV2> possibleMembersAdd(int id)
        {
            List<memberTableV2> possibleMembers = db.memberTableV2.ToList();


            foreach(memberTableV2 member in db.memberTableV2)
            {
                foreach(JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2)
                {
                    if(member.Id == junc.AID && junc.Role.Contains("Member") && id == junc.PId)
                    {
                        possibleMembers.Remove(member);
                    }
                }
            }
            return possibleMembers;
        }

        public List<memberTableV2> possibleMembersRemove(int id)
        {
            List<memberTableV2> possibleMembers = db.memberTableV2.ToList();
            List<memberTableV2> membersInProject = new List<memberTableV2>();

            foreach (memberTableV2 member in db.memberTableV2)
            {
                foreach (JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2)
                {
                    if (member.Id == junc.AID && junc.Role.Contains("Member") && id == junc.PId)
                    {
                        
                        membersInProject.Add(member);
                    }
                }
            }


            return membersInProject;
        }


        private string AddAccount(int id, string Rank, int projId)
        {
            try
            {
                db.JunctionTableProjectAndAccountV2.Add(new JunctionTableProjectAndAccountV2
                {
                    AID = id,
                    Role = Rank,
                    PId = projId,
                    Responsibilities = "Member"

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