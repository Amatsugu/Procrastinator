using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Bootstrap
{
	public class RazorConfig : IRazorConfiguration
	{
		public IEnumerable<string> GetAssemblyNames()
		{
			return null;
		}

		public IEnumerable<string> GetDefaultNamespaces()
		{
			yield return "Procrastinator";
			yield return "Procrastinator.Models";
		}

		public bool AutoIncludeModelNamespace
		{
			get { return true; }
		}
	}
}
