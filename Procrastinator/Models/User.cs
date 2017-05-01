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
		public long id { get; set; }
		[ScriptIgnore]
		public string UserName { get; set; }
		public IEnumerable<string> Claims { get; set; }
		
		private string username => UserName;
	}
}
