using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procrastinator;
using Procrastinator.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace Procrastinator.Tests
{
	[TestClass]
	public class CoreTests
	{
		public static Random Rand
		{
			get
			{
				if (_rand == null)
					_rand = new Random();
				return _rand;
			}
		}
		private static Random _rand;

		[TestMethod]
		public void RetreiveEvent()
		{
			var e = ProcrastinatorCore.GetEvent(192929);
			Assert.IsNotNull(e);
			Assert.AreEqual(e.Name, "Test Event");
		}

		[TestMethod]
		public void CreateAndRemoveEvent()
		{

			var e = new Event(Rand.Next(int.MaxValue), Guid.NewGuid().ToString(), DateTime.Now)
			{
				Description = "This is a test",
				Color = "#fff"
			};
			e = ProcrastinatorCore.CreateEvent(e);
			ProcrastinatorCore.RemoveEvent(e.Id);
		}

		[TestMethod]
		public void RetreiveSticker()
		{
			var s = ProcrastinatorCore.GetSticker(123456);
			Assert.AreEqual("url/to/image",s.FileUrl, "Url Mismatch");
			Assert.AreEqual(54321, s.UserId, "User mismatch");
			Assert.AreEqual("Test Sticker", s.Name);
		}

		[TestMethod]
		public void CreateAndRemoveSticker()
		{
			Sticker s = new Sticker(Rand.Next(int.MaxValue), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			s = ProcrastinatorCore.CreateSticker(s);
			ProcrastinatorCore.RemoveSticker(s.Id);
		}

		[TestMethod]
		public void CreateAndRemoveUser()
		{
			LoginCredentialsModel u = new LoginCredentialsModel
			{
				Username = $"Test User {Rand.Next(int.MaxValue)}",
				Password = "Test Password"
			};
			var user = ProcrastinatorCore.CreateUser(u);
			Assert.IsNotNull(ProcrastinatorCore.ValidateUser(u));
			ProcrastinatorCore.RemoveUser(user.Id);
		}

		[TestMethod]
		public void GetEventsFromDate()
		{
			DateTime now = DateTime.Now;
			List<Event> events = new List<Event>();
			var user = Rand.Next(int.MaxValue);
			for (int i = 0; i < 10; i++)
			{
				events.Add(ProcrastinatorCore.CreateEvent(new Event(user, Guid.NewGuid().ToString(), now)
				{
					Description = "This is a test",
					Color = "#fff"
				}));
			}
			var rEvents = ProcrastinatorCore.GetEventsFromDay(now.Year, now.Month, now.Day, user);
			Debug.Write(events);
			Assert.IsNotNull(rEvents);
			Assert.IsTrue(events.Count == rEvents.Length, "Lengths Mismatch");
			foreach (Event e in events)
				ProcrastinatorCore.RemoveEvent(e.Id);
		}

		[TestMethod]
		public void GetEventsFromMonth()
		{
			DateTime now = new DateTime(2017, 2, 1);
			List<Event> events = new List<Event>();
			var user = Rand.Next(int.MaxValue);
			for (int i = 0; i < 10; i++)
			{
				events.Add(ProcrastinatorCore.CreateEvent(new Event(user, Guid.NewGuid().ToString(), now = now.AddDays(1))
				{
					Description = "This is a test",
					Color = "#fff"
				}));
			}
			var rEvents = ProcrastinatorCore.GetEventsFromMonth(now.Year, now.Month, user);
			Assert.IsNotNull(rEvents);
			Assert.AreEqual(events.Count, rEvents.Length, "Lengths Mismatch");
			foreach (Event e in events)
				ProcrastinatorCore.RemoveEvent(e.Id);
		}
	}
}
