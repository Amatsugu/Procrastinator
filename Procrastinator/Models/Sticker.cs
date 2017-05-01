using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Json;

namespace Procrastinator.Models
{
	public class Sticker
	{
		public long id { get; set; }
		public string name { get; set; }
		public string url { get; set; }

		[ScriptIgnore]
		public string fileUrl { get; set; }
	}
}
