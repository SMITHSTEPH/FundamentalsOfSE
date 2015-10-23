using System.Linq;
using System;

namespace WebApplication2.Models
{
    public class Administrator : Account
    {
        public string FirstName { get; set; } //define properties
        public string LastName { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string AdminKey { get; set; }

        public void Init(Administrator Table)
        {
                db.administrationV2.Add(new administrationV2
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

        public string isValid(Administrator Self)
        {
            if (Self.AdminKey != "5")
                return "AdminKey";
            else if (IsNullAdmin(Self))
                return "NullFields";
            else if (FindAdmin(Self.UserName))
                return "Exist";
            else return vf.PasswordCheck(Self.Password);
        }

        public Boolean IsNullAdmin(Administrator Table)
        {
            if (vf.IsNull(Table.UserName) ||
                     vf.IsNull(Table.Email) ||
                     vf.IsNull(Table.Password) ||
                     vf.IsNull(Table.Address) ||
                     vf.IsNull(Table.Birthdate) ||
                     vf.IsNull(Table.Gender) ||
                     vf.IsNull(Table.FirstName) ||
                     vf.IsNull(Table.LastName) ||
                     vf.IsNull(Table.PhoneNumber) ||
                     vf.IsNull(Table.AdminKey))
            {
                return true;
            }
            else return false;
        }

        public bool FindAdmin(string UserName)
        {
            try
            {
                string queryA = "SELECT * FROM administrationV2 WHERE UserName='" + UserName + "'";
                administrationV2 AT = db.administrationV2.SqlQuery(queryA).SingleOrDefault();

                if (AT != null)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return true;
            }
        }


    }
}