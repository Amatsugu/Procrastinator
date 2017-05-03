using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Json;

namespace Procrastinator.Models
{
	public struct Sticker
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Url => $"/sticker/{Id}";

		[ScriptIgnore]
		public string FileUrl { get; set; }

		public Sticker(long id, string name, string fileUri)
		{
			Id = id;
			Name = name;
			FileUrl = fileUri;
		}
	}
}
