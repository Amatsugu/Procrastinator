using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Procrastinator.Modules
{
	public class UserModule : NancyModule
	{
		public UserModule() : base("/user")
		{
			Get["/profilepic"] = _ =>
			{
				return Response.AsRedirect("/res/img/defaultProfile.png");
			};
		}
	}
}
