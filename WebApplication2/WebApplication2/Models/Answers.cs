using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Answers
    {
        //public Dictionary<int, string> Ans { get; set; }
        public string[] Ans { get; set; }
        public Answers()
        {
            Ans = new string[92];
        }
    }
}