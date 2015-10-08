using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class Leader : Member
	{
        public string LeaderKey { get; set; }

        public override void Init(System.Web.Mvc.FormCollection form)
        {
            base.Init(form);
            LeaderKey = form["LeaderKey"];
        }

    }
}