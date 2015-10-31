using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Answers
    {
        public Dictionary<int, string> Ans { get; set; }

        public Answers()
        {
            Ans = new Dictionary<int, string>();
            for(int i=0; i<92; i++)
            {
                Ans.Add(i, "test");
            }

        }

    }
}