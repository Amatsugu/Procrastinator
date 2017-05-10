using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Authentication.Stateless;
using Nancy.Security;

namespace Procrastinator.Modules
{
	public class IndexModule : NancyModule
	{
		private static Random Rand = new Random();

		public IndexModule()
		{
			StatelessAuthentication.Enable(this, ProcrastinatorCore.StatelessConfig);
			this.RequiresAuthentication();
			Get["/"] = _ =>
			{
				Console.WriteLine(Context?.CurrentUser?.UserName);
				return View["index", new { user = Context.CurrentUser, bgid = Rand.Next(7) }];
			};
		}
	}
}
