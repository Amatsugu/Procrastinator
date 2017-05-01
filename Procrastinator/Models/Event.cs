using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Models
{
	public class Event
	{
		public enum EventType //TODO: Add more event types
		{
			Basic,
			Work,
			School

		}

		public long id { get; private set; }
		public long userId { get; private set; }
		public string eventName { get; private set; }
		public DateTime eventDate { get; private set; }
		public DateTime eventEndDate { get; private set; }
		public bool allDay { get; private set; }
		public string eventDescription { get; private set; }
		public EventType eventType { get; private set; }
		public string eventColor { get; private set; }
		public long[] stickers { get; private set; } 
	}
}
