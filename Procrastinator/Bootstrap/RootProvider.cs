using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
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
			return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
		}
	}
#endif
}
