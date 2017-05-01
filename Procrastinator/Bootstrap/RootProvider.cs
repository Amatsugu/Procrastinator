using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Bootstrap
{
#if DEBUG
	public class RootProvider : IRootPathProvider
	{
		public string GetRootPath()
		{
			return @"C:\Users\Karuta\Documents\GitHub\Procrastinator\Procastinator";
		}
	}
#endif
}
