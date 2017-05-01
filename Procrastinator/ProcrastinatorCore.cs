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

		internal static Event[] GetEventsFrom(int year, int month)
		{
			throw new NotImplementedException();
		}

		internal static Event[] GetEventsFrom(int year, int month, int day)
		{
			throw new NotImplementedException();
		}

		internal static Event GetEvent(long id)
		{
			throw new NotImplementedException();
		}

		internal static void RemoveEvent(long id)
		{
			throw new NotImplementedException();
		}

		internal static Sticker GetStricker(long id)
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
					cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable}";
					using (var reader = cmd.ExecuteReader())
					{	
						while(reader.Read())
						{
							var data = reader.GetString(0);
						}
					}
				}
			}
			return null;
		}

		internal static User GetUser(long id)
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
