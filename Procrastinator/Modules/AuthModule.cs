using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Procrastinator.Models;

namespace Procrastinator.Modules
{
	public class AuthModule : NancyModule
	{
		public AuthModule() : base("/auth")
		{
			Post["/login"] = args =>
			{
				var login = this.Bind<LoginCredentialsModel>();
				var auth = ProcrastinatorCore.ValidateUser(login);
				if (string.IsNullOrEmpty(auth))
					return new Response { StatusCode = HttpStatusCode.Unauthorized };
				else
					return new Response().WithCookie("Auth", auth, DateTime.Now.AddDays(5));
			};

			Get["/logout"] = _ =>
			{
				return new Response().WithCookie("Auth", null, DateTime.Now);
			};
		}
	}
}
