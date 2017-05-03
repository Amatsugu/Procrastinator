using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastinator.Models
{
	public static class Extentions
	{
		public static string ToArrayLiteral(this Sticker[] stickers) => $"{{{string.Join(",", from Sticker s in stickers select s.Id.ToString())}}}";
			
	}
}
