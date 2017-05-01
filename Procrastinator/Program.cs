using Nancy.Hosting.Self;
using Procrastinator.Models;
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
			try
			{
				var e = new Event(192929, "Test Event", DateTime.Now, 107426);
				//ProcrastinatorCore.CreateEvent(e);
			}catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			var e1 = ProcrastinatorCore.GetEvent(192929);
			Console.ReadLine();
		}
	}
}
