using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Security;
using Nancy.ModelBinding;
using Procrastinator.Models;

namespace Procrastinator.Modules
{
	public class APIModule : NancyModule
	{
		public APIModule() : base("/api")
		{

			//TODO: Implementation
			StatelessAuthentication.Enable(this, ProcrastinatorCore.StatelessConfig);
			this.RequiresAuthentication();
			//Event
			Get["/event/all"] = _ =>
			{
				return ProcrastinatorCore.GetAllEvents();
			};

			Get["/event/{year}/{month}"] = date =>
			{
				return ProcrastinatorCore.GetEventsFrom((int)date.year, (int)date.month);
			};

			Get["/event/{year}/{month}/{day}"] = date =>
			{
				return ProcrastinatorCore.GetEventsFrom((int)date.year, (int)date.month, (int)date.day);
			};

			Get["/event/{id}"] = e =>
			{
				return ProcrastinatorCore.GetEvent((long)e.id);
			};

			Delete["/event/{id}"] = e =>
			{
				ProcrastinatorCore.RemoveEvent((long)e.id);
				return new Response
				{
					StatusCode = HttpStatusCode.OK
				};
			};

			Post["/event"] = e =>
			{
				var theEvent = this.Bind<Event>();
				ProcrastinatorCore.CreateEvent(theEvent);
				return new Response
				{
					StatusCode = HttpStatusCode.OK
				};
			};

			//Sticker
			Get["/sticker/{id}"] = s =>
			{
				return ProcrastinatorCore.GetStricker((long)s.id);
			};

			Delete["/sticker/{id}"] = s =>
			{
				ProcrastinatorCore.RemoveSticker((long)s.id);
				return new Response
				{
					StatusCode = HttpStatusCode.OK
				};
			};

			//User
			Get["/user/{id}"] = u =>
			{
				return ProcrastinatorCore.GetUser((long)u.id);
			};

			Delete["/user/{id}"] = u =>
			{
				ProcrastinatorCore.RemoveUser((long)u.id);
				return new Response
				{
					StatusCode = HttpStatusCode.OK
				};
			};
		}
	}
}
