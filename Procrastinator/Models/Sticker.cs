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
		public long UserId { get; set; }

		[ScriptIgnore]
		public string FileUrl { get; set; }

		public Sticker(long userId, string name, string fileUri)
		{
			Id = ProcrastinatorCore.GenerateID();
			Name = name;
			FileUrl = fileUri;
			UserId = userId;
		}
	}
}
