using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procastinator.Bootstrap
{
#if DEBUG
	public class RootProvider : IRootPathProvider
	{
		public string GetRootPath()
		{
			return @"C:\Users\Karuta\Documents\Visual Studio 2015\Projects\Aoba\Aoba.Server";
		}
	}
#endif
}
