using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Modules
{
	public class RegisterModule : NancyModule 
	{
		public RegisterModule() : base("/register")
		{
			Get["/"] = _ =>
			{
				return View["register"];
			};
		}
	}
}
