using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procastinator.Bootstrap
{
	public class RazorConfig : IRazorConfiguration
	{
		public IEnumerable<string> GetAssemblyNames()
		{
			return null;
		}

		public IEnumerable<string> GetDefaultNamespaces()
		{
			yield return "Procastinator";
			//yield return "Procastinator.Models";
		}

		public bool AutoIncludeModelNamespace
		{
			get { return true; }
		}
	}
}
