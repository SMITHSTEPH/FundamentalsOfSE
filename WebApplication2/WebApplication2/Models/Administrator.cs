using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Administrator : Member
    {
        public string AdminKey { get; set; }

        public override void Init(System.Web.Mvc.FormCollection form)
        {
            base.Init(form);
            AdminKey = form["AdminKey"];
        }
    }
}