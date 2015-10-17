namespace WebApplication2.Models
{
    public class Leader : Member
	{
        public string LeaderKey { get; set; }

        public override void Init(Member Table)
        {
            using (var activedb = new RegistrationEntities1())
            {
                activedb.leaderTableV2.Add(new leaderTableV2
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

        public string isValid(Leader Self)
        {
            if (Self.LeaderKey != "5")
                return "LeaderKey";
            else return vf.Check(Self);
        }

    }
}