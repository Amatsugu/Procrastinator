using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procrastinator;
using Procrastinator.Models;

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
			
			var e = new Event(Rand.Next(int.MaxValue), Rand.Next(int.MaxValue), Guid.NewGuid().ToString(), DateTime.Now);
			e = ProcrastinatorCore.CreateEvent(e);
			Assert.AreEqual(e, ProcrastinatorCore.GetEvent(e.Id));
			ProcrastinatorCore.RemoveEvent(e.Id);
		}

		[TestMethod]
		public void GetStickers()
		{
			Assert.Fail("Test not Written");
		}

		[TestMethod]
		public void CreateAndRemoveSticker()
		{
			Sticker s = new Sticker(Rand.Next(int.MaxValue), Rand.Next(int.MaxValue), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			s = ProcrastinatorCore.CreateSticker(s);
			Assert.AreEqual(s, ProcrastinatorCore.GetSticker(s.Id));
			ProcrastinatorCore.RemoveSticker(s.Id);
		}

		[TestMethod]
		public void CreateAndRemoveUser()
		{

		}
	}
}
