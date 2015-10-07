using System;
using System.Data.Entity;

//http://www.asp.net/mvc/overview/older-versions/getting-started-with-aspnet-mvc4/adding-a-model
//http://www.codeproject.com/Articles/639709/Getting-Data-From-View-to-Controller-in-MVC
namespace WebApplication2.Models
{
    public class Member
    {
        public string FirstName { get; set; } //define properties
        public string LastName { get; set; } 
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }

        //private database instance?

        

        public Boolean isUserValid(String Username, String Password)
        {
            //if valid return true
            //else return false
            return true;
        }
        //public void Create()

    }
    public class MemberDBContext : DbContext
    {
        public DbSet<Member> members { get; set;  }
    }
}