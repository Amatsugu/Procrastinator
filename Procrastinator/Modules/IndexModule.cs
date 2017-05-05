using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Modules
{
	public class IndexModule : NancyModule
	{
		public IndexModule()
		{
			Get["/"] = _ =>
			{
#if !DEBUG
				//if (Context.CurrentUser == null)
				//	return View["login"];
				//else
#endif
					return View["index", new { user = Context.CurrentUser }];
			};
		}
	}
}
