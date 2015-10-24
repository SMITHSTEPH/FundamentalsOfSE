using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Project
    {
        public string UserName { get; set; }
        public string Rank { get; set; }
        public int AccountId { get; set; }
        public int[] ProjectId { get; set; }

        RegistrationEntities1 db = new RegistrationEntities1();

        public Project UsersProjects (Account User)
        {
            

            List<JunctionTableProjectAndAccount> projects = db.JunctionTableProjectAndAccounts.ToList();

            Project UsersProjects = new Project();
            int j = 0;
            UsersProjects.ProjectId = new int[projects.Count];

            foreach (JunctionTableProjectAndAccount project in projects)
            {

                UsersProjects.UserName = User.UserName;
                UsersProjects.Rank = User.Rank;
                UsersProjects.AccountId = User.AccountId;

                if (project.Role.Contains(User.Rank) && project.AID == User.AccountId)
                {
                    UsersProjects.ProjectId[j] = new int();
                    UsersProjects.ProjectId[j] = (int) project.PId;
                }

                j++;
            }
            
            return UsersProjects;
        }


    }
}