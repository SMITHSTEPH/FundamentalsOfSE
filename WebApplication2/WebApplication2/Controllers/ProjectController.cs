using WebApplication2.Models;
using System.Web.Mvc;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Data.Entity;

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

        public ActionResult EditPeople(Project ListofProjects)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Members = db.memberTableV2.ToList();
            myModel.Leaders = db.leaderTableV2.ToList();
            JunctionTableProjectAndAccountV2 table = new JunctionTableProjectAndAccountV2();
            table.AID = ListofProjects.AccountId;
            myModel.JunctionTable = table;
            return View("EditPeople", myModel);

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
            myModel.JunctionTableEdit = possibleMembersEdit(id);
            ProjectTable table = new ProjectTable();
            table.ProjectId = id;
            myModel.ProjectTable = table;

            return View("ApplyProjects", myModel);
        }

        public ActionResult EditTable(string DropChoice)
        {
            string result = DropChoice;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string[] strArray;
            strArray = r.Split(DropChoice);
            string resp = strArray[0];
            int id = Int32.Parse(strArray[1]);
            int AID = Int32.Parse(strArray[3]);
            string Role = strArray[2];

            string query = "SELECT * FROM JunctionTableProjectAndAccountV2 WHERE AID= " + AID + " AND Role= '" + Role + "' AND PId = " + id;
            JunctionTableProjectAndAccountV2 JT = db.JunctionTableProjectAndAccountV2.SqlQuery(query).SingleOrDefault();

            JT.Responsibilities = resp;
            db.Entry(JT).State = EntityState.Modified;
            db.SaveChanges();

            return View("SuccessPage");
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

        public ActionResult ChangeRank(string role, int Aid, int UId)
        {
            if(role == "Member")
            {
                memberTableV2 mem = db.memberTableV2.Find(Aid);
                leaderTableV2 leader = new leaderTableV2();

                db.leaderTableV2.Add(new leaderTableV2
                {
                    UserName = mem.UserName,
                    Email = mem.Email,
                    ConfirmEmail = mem.ConfirmEmail,
                    OptionalPhoneNumber = mem.OptionalPhoneNumber,
                    Password = mem.Password,
                    Address = mem.Address,
                    BirthDate = mem.BirthDate,
                    Gender = mem.Gender,
                    FirstName = mem.FirstName,
                    MiddleName = mem.MiddleName,
                    LastName = mem.LastName,
                    PhoneNumber = mem.PhoneNumber
                });
                db.memberTableV2.Remove(mem);
                db.SaveChanges();

                List<leaderTableV2> leadList = db.leaderTableV2.ToList();
                leaderTableV2 newLeader = leadList.Last();
                UpdateJunction(Aid, "Member", newLeader.Id, "Leader", UId);

            }
            else
            {
                memberTableV2 mem = new memberTableV2();
                leaderTableV2 leader = db.leaderTableV2.Find(Aid);

                db.memberTableV2.Add(new memberTableV2
                {
                    UserName = leader.UserName,
                    Email = leader.Email,
                    ConfirmEmail = leader.ConfirmEmail,
                    OptionalPhoneNumber = leader.OptionalPhoneNumber,
                    Password = leader.Password,
                    Address = leader.Address,
                    BirthDate = leader.BirthDate,
                    Gender = leader.Gender,
                    FirstName = leader.FirstName,
                    MiddleName = leader.MiddleName,
                    LastName = leader.LastName,
                    PhoneNumber = leader.PhoneNumber
                });
                db.leaderTableV2.Remove(leader);
                db.SaveChanges();

                List<memberTableV2> memList = db.memberTableV2.ToList();
                memberTableV2 newMem = memList.Last();
                UpdateJunction(Aid, "Leader", newMem.Id, "Member", UId);

            }
            administrationV2 admin = db.administrationV2.Find(UId);
            Account User = new Account();
            User.AccountId = admin.Id;
            User.Rank = "Admin";
            User.UserName = admin.UserName;


            return RedirectToAction("ExistingProjects", "Project", User);
        }

        public ActionResult RemoveTable(int id, string role, int Aid)
        {
            
            int AID = Aid;
            string Role = role;

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


        public List<JunctionTableProjectAndAccountV2> possibleMembersEdit(int id)
        {
            
            List<JunctionTableProjectAndAccountV2> respInProject = new List<JunctionTableProjectAndAccountV2>();


            foreach (memberTableV2 member in db.memberTableV2)
            {
                foreach (JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2)
                {
                    if (member.Id == junc.AID && junc.Role.Contains("Member") && id == junc.PId)
                    {

                        respInProject.Add(junc);
                    }
                }
            }


            return respInProject;
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
                    Responsibilities = Rank

                });
                db.SaveChanges();
                return "Success";
            }
            catch
            {
                return "Fail";
            }
        }

        private void UpdateJunction(int fromId, string fromRole, int toId, string toRole, int UId)
        {
            int[] projectIds = new int[db.JunctionTableProjectAndAccountV2.Count()];
            int j = 0;
            //bool isSomeone = false;

            foreach (JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2.ToList())
            {
                if (fromId == junc.AID && junc.Role.Contains(fromRole))
                {

                    junc.AID = toId;
                    junc.Role = toRole;

                    projectIds[j] = (int)junc.PId;
                    j++;

                    if (toRole == "Leader")
                    {
                        junc.Responsibilities = "Leader";
                    }
                    else
                    {
                        junc.Responsibilities = "Member";
                    }


                    db.Entry(junc).State = EntityState.Modified;
                    db.SaveChanges();
                    
                   
                    
                }
            }

            //int k;
            //Check if there are two leaders for a given project
            if (toRole == "Leader")
            {
                foreach (JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2.ToList())
                {

                    foreach (int id in projectIds)
                    {
                        if (junc.PId == id && junc.Role.Contains("Leader") && junc.AID != toId)
                        {
                            junc.Responsibilities = "2ndCommand";
                            db.Entry(junc).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                }

            }
            //Add a new leader for the project
            else
            {
                bool backUp = false;
                //bool lastOption = false;
                foreach (JunctionTableProjectAndAccountV2 junc in db.JunctionTableProjectAndAccountV2.ToList())
                {
                    foreach (int id in projectIds)
                    {
                        if (junc.PId == id && junc.Role.Contains("Leader") && junc.Responsibilities.Contains("2ndCommand"))
                        {
                            junc.Responsibilities = "Leader";
                            db.Entry(junc).State = EntityState.Modified;
                            db.SaveChanges();
                            backUp = true;
                            break;
                        }

                        if (junc.PId == id && junc.Role.Contains("Member") && junc.Responsibilities.Contains("2ndCommand"))
                        {
                            junc.Responsibilities = "Leader";
                            db.Entry(junc).State = EntityState.Modified;
                            db.SaveChanges();
                            backUp = true;
                            break;
                        }
                        else if (backUp == false)
                        {
                            if (junc.PId == id && junc.Role.Contains("Member"))
                            {
                                junc.Responsibilities = "2ndCommand";
                                db.Entry(junc).State = EntityState.Modified;
                                db.SaveChanges();
                                break;
                            }
                        }
                        
                    }

                }

            }

        }

    }
}