using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procastinator.Bootstrap
{
	public class NancyBootstrap : DefaultNancyBootstrapper
	{


		// TODO: Favicon
		/*private byte[] favicon;

		protected override byte[] FavIcon
		{
			get { return favicon ?? (favicon = LoadFavIcon()); }
		}

		private byte[] LoadFavIcon()
		{
			return File.ReadAllBytes(@"Procastinator/res/img/Procastinator.ico");
		}*/

		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			Conventions.ViewLocationConventions.Add((viewName, model, context) =>
			{
				return string.Concat("Procastinator/", viewName);
			});
		}

		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("res", $"ProcastinatorWeb/res"));
		}

#if DEBUG
		protected override IRootPathProvider RootPathProvider
		{
			get { return new RootProvider(); }
		}
#endif
	}
}
