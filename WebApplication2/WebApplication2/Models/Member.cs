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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }

        public virtual void Init(System.Web.Mvc.FormCollection form)
        {
            Address = form["Address"];
            Birthdate = form["Birthdate"];
            FirstName = form["FirstName"];
            LastName = form["LastName"];
            UserName = form["UserName"];
            Password = form["Password"];
            PhoneNumber = form["PhoneNumber"];
            Gender = form["Gender"];
        }
    }
}