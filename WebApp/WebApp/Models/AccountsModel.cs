using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class AccountsModel
    {
        [Key]
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public String Gender { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String BirthDate { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class AccountsDBContext : DbContext
    {
        public DbSet<AccountsModel> Account { get; set; }
    }
}