using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class Leader : Member
	{
        public string LeaderKey { get; set; }

        public override void Init(Member Table)
        {
            db.leaderTables.Add(new leaderTable
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

        public string isValid(Leader Self)
        {
            if (Self.LeaderKey != "5")
                return "LeaderKey";
            else return vf.Check(Self);
        }

    }
}