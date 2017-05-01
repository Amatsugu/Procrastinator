using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procrastinator;
using Procrastinator.Models;

namespace Procrastinator.Tests
{
	[TestClass]
	public class CoreTests
	{
		[TestMethod]
		public void RetreiveEvent()
		{
			try
			{
				var e = ProcrastinatorCore.GetEvent(192929);
				Assert.IsNotNull(e);
				Assert.AreEqual(e.Name, "Test Event");
			}catch
			{
				Assert.Fail();
			}
		}

		[TestMethod]
		public void CreateEvent()
		{
			try
			{
				var e = new Event();

			}catch
			{
				Assert.Fail();
			}
		}
	}
}
