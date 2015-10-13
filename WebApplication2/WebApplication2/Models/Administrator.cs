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
            db.administrations.Add(new administration
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

        public string isValid(Administrator Self)
        {
            if (Self.AdminKey != "5")
            {
                return "AdminKey";
            }
            else
            {
                return vf.Check(Self);
            }
        }
    }
}