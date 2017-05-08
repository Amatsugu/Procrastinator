using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Procrastinator.Models;
using Nancy.Extensions;

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
				Console.WriteLine(auth);
				if (string.IsNullOrEmpty(auth))
					return new Response { StatusCode = HttpStatusCode.Unauthorized };
				else
					return new Response().WithCookie("Auth", auth, DateTime.Now.AddDays(5));
			};

			Get["/logout"] = _ =>
			{
				return Response.AsRedirect("/").WithCookie("Auth", null, DateTime.Now);
			};

			Post["/register"] = _ =>
			{
				var user = this.Bind<LoginCredentialsModel>();
				ProcrastinatorCore.CreateUser(user);
				var auth = ProcrastinatorCore.ValidateUser(user);
				return new Response().WithCookie("Auth", auth, DateTime.Now.AddDays(5));
			};

			Post["/checkuser"] = args =>
			{
				return (ProcrastinatorCore.CheckUserExists(Request.Body.AsString())) ? new Response { StatusCode = HttpStatusCode.NotAcceptable } : new Response { StatusCode = HttpStatusCode.OK };
			};

		}
	}
}
