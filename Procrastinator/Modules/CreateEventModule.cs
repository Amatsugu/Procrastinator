using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Modules
{
	public class CreateEventModule : NancyModule
	{
		public CreateEventModule() : base("/create")
		{
			Get["/"] = args =>
			{
				return View["createEvent", new { user = Context.CurrentUser }];
			};
		}
	}
}
