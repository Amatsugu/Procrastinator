using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procrastinator;

namespace Procrastinator.Credentials
{
	public static class DBCredentials
	{
		private const string DB_User = "TechSupport";
		private const string DB_Pass = "";
		private const string DB_Name = "ProcrastinatorDB";

		public const string DB_eventTable = "event";
		public const string DB_stickerTable = "sticker";
		public const string DB_userTable = "userdata";
		
		public static string CONNECTION_STRING => $"Host={ProcrastinatorCore.HOST};Username={DB_User};Password={DB_Pass};Database={DB_Name};Pooling=true";

	}

}