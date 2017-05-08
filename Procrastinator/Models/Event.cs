using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Procrastinator.Models
{
	public struct Event
	{
		public enum EventStyle //TODO: Add more event types
		{
			Basic,
			Work,
			School

		}

		public long Id { get; set; }
		public long UserId { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public DateTime EndDate { get; set; }
		public bool AllDay { get; set; }
		public string Description { get; set; }
		public EventStyle Style { get; set; }
		public string Color { get; set; }
		public Sticker[] Stickers { get; set; }

		public Event(long userId, string eventName, DateTime eventDate)
		{
			Id = ProcrastinatorCore.GenerateID();
			UserId = userId;
			Name = eventName;
			Date = eventDate;
			Style = EventStyle.Basic;
			EndDate = eventDate;
			AllDay = false;
			Description = "";
			Style = EventStyle.Basic;
			Color = "#fff";
			Stickers = null;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (obj.GetType() != typeof(Event))
				return false;
			var e = (Event)obj;
			return Equals(e);
		}

		public bool Equals(Event e)
		{
			if (Id == e.Id
				&& UserId == e.UserId
				&& Name == e.Name
				&& Date.Equals(e.Date)
				&& EndDate.Equals(e.EndDate)
				&& AllDay == e.AllDay
				&& Description == e.Description
				&& Style == e.Style
				&& Color == e.Color
				&& Stickers == e.Stickers)
				return true;
			else
				return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Event a, Event b)
		{
			if (ReferenceEquals(a, b))
				return true;
			if ((a == null) || (b == null))
				return false;
			return a.Equals(b);
		}

		public static bool operator !=(Event a, Event b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return $"{Name}, ID:[{Id}, {UserId}], Date:[{Date}, {EndDate}], Allday:{AllDay}, Description: [{Description}], Color:{Color}, Style:[{Style}], Stickers:{JsonConvert.SerializeObject(Stickers)}";
		}
	}

	public class EventNotFoundExeception : Exception
	{
		public long EventID { get; set; }

		public EventNotFoundExeception(long eventID) : base($"Event '{eventID}' could not be found!")
		{
			EventID = eventID;
		}
	}
}
