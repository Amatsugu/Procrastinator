using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Procrastinator.Credentials;
using Procrastinator.Models;

namespace Procrastinator
{
	public static class ProcrastinatorCore
	{
		public const string HOST = "karuta.luminousvector.com";

		private static NpgsqlConnection GetConnection()
		{
			var con = new NpgsqlConnection(DBCredentials.CONNECTION_STRING);
			con.Open();
			return con;
		}

		internal static dynamic GetEventsFrom(int year, int month)
		{
			throw new NotImplementedException();
		}

		internal static dynamic GetEventsFrom(int year, int month, int day)
		{
			throw new NotImplementedException();
		}

		internal static dynamic GetEventsFrom(long id)
		{
			throw new NotImplementedException();
		}

		internal static void RemoveEvent(long id)
		{
			throw new NotImplementedException();
		}

		internal static dynamic GetStricker(long id)
		{
			throw new NotImplementedException();
		}

		internal static void RemoveSticker(long id)
		{
			throw new NotImplementedException();
		}

		internal static Event[] GetAllEvents() //TODO: Retrieive All Events
		{
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
					return null;
				}
			}
		}

		internal static dynamic GetUser(long id)
		{
			throw new NotImplementedException();
		}

		internal static void RemoveUser(long id)
		{
			throw new NotImplementedException();
		}

		public static bool Init()
		{
			try //Check DB Connection
			{
				using (var con = GetConnection())
				{
					using (var cmd = con.CreateCommand())
					{
						cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable}";
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
