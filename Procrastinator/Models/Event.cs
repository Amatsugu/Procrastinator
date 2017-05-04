using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public Event(long id, long userId, string eventName, DateTime eventDate)
		{
			Id = id;
			UserId = userId;
			Name = eventName;
			Date = eventDate;
			Style = EventStyle.Basic;
			EndDate = eventDate;
			AllDay = false;
			Description = null;
			Style = EventStyle.Basic;
			Color = null;
			Stickers = null;
		}

		/*public bool AreSameEvent(Event e)
		{
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
		}*/
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
