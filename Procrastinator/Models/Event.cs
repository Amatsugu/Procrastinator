using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Models
{
	public class Event
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

		public Event()
		{

		}

		public Event(long id, string eventName, DateTime eventDate, long userId)
		{
			Id = id;
			Name = eventName;
			Date = eventDate;
			UserId = userId;
			Style = EventStyle.Basic;
			Color = "#fff";
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Event))
				return false;
			Event e = (Event)obj;
			if (Id == e.Id
				&& UserId == e.UserId
				&& Name == e.Name
				&& Date == e.Date
				&& EndDate == e.EndDate
				&& AllDay == e.AllDay
				&& Description == e.Description
				&& Style == e.Style
				&& Color == e.Color
				&& Stickers == e.Stickers)
				return true;
			else
				return false;
		}
	}
}
