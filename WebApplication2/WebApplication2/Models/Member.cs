using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;

//http://www.asp.net/mvc/overview/older-versions/getting-started-with-aspnet-mvc4/adding-a-model
//http://www.codeproject.com/Articles/639709/Getting-Data-From-View-to-Controller-in-MVC
namespace WebApplication2.Models
{
    public class Member
    {
        public string FirstName { get; set; } //define properties
        public string LastName { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public bool ConfirmEmail { get; set; }
        public string MiddleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }

        protected RegistrationEntities1 db = new RegistrationEntities1();
        protected Verifcation vf = new Verifcation();

        public virtual void Init(Member Table)
        {

            db.memberTableV2.Add(new memberTableV2
            {
                UserName = Table.UserName,
                Email = Table.Email,
                ConfirmEmail = Table.ConfirmEmail,
                OptionalPhoneNumber = Table.OptionalPhoneNumber,
                Password = vf.Encrypt(Table.Password),
                Address = Table.Address,
                BirthDate = Table.Birthdate,
                Gender = Table.Gender,
                FirstName = Table.FirstName,
                MiddleName = Table.MiddleName,
                LastName = Table.LastName,
                PhoneNumber = Table.PhoneNumber
            });
            db.SaveChanges();
        }

        public string isValid(Member Self)
        {
            return vf.Check(Self);
        }
    }
}