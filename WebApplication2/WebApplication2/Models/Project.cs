using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Models
{
    public class Project
    {
        public string UserName { get; set; }
        public string Rank { get; set; }
        public int AccountId { get; set; }
        public int[] ProjectId { get; set; }
        public string[] ProjectProcess { get; set; }
        public string[] Responsibiltes { get; set; }

        private RegistrationEntities1 db = new RegistrationEntities1();

        public Project UsersProjects (Account User)
        {
            

            List<JunctionTableProjectAndAccountV2> projects = db.JunctionTableProjectAndAccountV2.ToList();
            List<ProjectTable> projectsProess = db.ProjectTables.ToList();

            Project UsersProjects = new Project();
            int j = 0;
            UsersProjects.ProjectId = new int[projects.Count];
            UsersProjects.UserName = User.UserName;
            UsersProjects.Rank = User.Rank;
            UsersProjects.AccountId = User.AccountId;

            foreach (JunctionTableProjectAndAccountV2 project in projects)
            {
                try
                {
                    if (project.Role.Contains(User.Rank) && project.AID == User.AccountId)
                    {
                        UsersProjects.ProjectId[j] = new int();
                        UsersProjects.ProjectId[j] = (int)project.PId;
                        j++;
                    }
                }
                catch (ArgumentNullException){}

            }

            int numberOfIds = UsersProjects.ProjectId.Length;

            if (numberOfIds > 0)
            {
                UsersProjects.ProjectProcess = new string[numberOfIds];
                int k = 0;
                foreach (int id in UsersProjects.ProjectId)
                {
                    foreach (ProjectTable process in projectsProess)
                    {

                        if (id == process.ProjectId)
                        {
                            UsersProjects.ProjectProcess[k] = process.ProcessModelChosen;
                        }

                    }

                    k++;
                }

                k = 0;
                UsersProjects.Responsibiltes = new string[numberOfIds];
                foreach (JunctionTableProjectAndAccountV2 project in projects)
                {
                    
                    if (project.Role.Contains(User.Rank) && project.AID == User.AccountId)
                    {
                        
                        UsersProjects.Responsibiltes[k] = project.Responsibilities;
                        k++;
                    }


                }

            }

            return UsersProjects;
        }


    }
}