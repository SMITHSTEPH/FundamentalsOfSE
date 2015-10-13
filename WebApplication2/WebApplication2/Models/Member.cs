using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

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
        public string Email { get; set; }

        private RegistrationEntities1 db = new RegistrationEntities1();
        private Verifcation vf = new Verifcation();

        public virtual void Init(Member Table)
        {
            db.memberTables.Add(new memberTable
            {
                UserName = Table.UserName,
                Email = Table.Email,
                Password = vf.Encrypt(Table.Password),
                Address = Table.Address,
                BirthDate = Table.Birthdate,
                Gender = Table.Gender,
                FirstName = Table.FirstName,
                LastName = Table.LastName,
                PhoneNumber = Table.PhoneNumber
            });
            db.SaveChanges();
        }

    }
}