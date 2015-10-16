using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Administrator : Member
    {
        public string AdminKey { get; set; }

        public override void Init(Member Table)
        {
            using (var activedb = new RegistrationEntities1())
            {
                activedb.administrationV2.Add(new administrationV2
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
                activedb.SaveChanges();

            }
        }

        public string isValid(Administrator Self)
        {
            if (Self.AdminKey != "5")
                return "AdminKey";
            else return vf.Check(Self);
        }
    }
}