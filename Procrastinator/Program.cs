using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator
{
	class Program
	{
		static void Main(string[] args)
		{
			var host = new NancyHost(new HostConfiguration() { UrlReservations = new UrlReservations() { CreateAutomatically = true } }, new Uri("http://localhost:2410"));
			host.Start();
			Console.WriteLine("Hosting...");
			if (ProcrastinatorCore.Init())
				Console.WriteLine("DB Connected!");
			else
				Console.WriteLine("DB Connection Failed!");

			Console.ReadLine();
		}
	}
}
