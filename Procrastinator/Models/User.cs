using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Json;
using Nancy.Security;

namespace Procrastinator.Models
{
	public class User : IUserIdentity
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public IEnumerable<string> Claims { get; set; }

		public User(string username, string[] claims = null)
		{
			Id = ProcrastinatorCore.GenerateID();
			UserName = username;
			Claims = claims;
		}
	}
}
