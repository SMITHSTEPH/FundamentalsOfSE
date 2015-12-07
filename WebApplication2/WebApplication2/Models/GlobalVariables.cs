using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public static class GlobalVariables
	{
		public static int ProjectID_cal { get; set; }
		public static string role_cal
		{
			get
			{
				return HttpContext.Current.Application["role_cal"] as string;
			}
			set
			{
				HttpContext.Current.Application["role_cal"] = value;
			}
		}
	}
}