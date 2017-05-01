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
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable} WHERE eventId='{id}'";
					using (var reader = cmd.ExecuteReader())
					{
						if (!reader.HasRows)
							return null;
						reader.Read();
						long[] stickerIds = reader.GetValue(6) as long[];
						return new Event
						{
							Id = id,
							UserId=reader.GetInt64(7),
							Name=reader.GetString(0),
							Date=reader.GetDateTime(1),
							EndDate=reader.GetDateTime(5),
							AllDay=reader.GetBoolean(2),
							Description=reader.GetString(3),
							Style= (Event.EventStyle)Enum.Parse(typeof(Event.EventStyle), reader.GetString(9), true),
							Color=reader.GetString(8),
							Stickers = GetStickers(stickerIds)
						};
					}
				}
			}
		}

		internal static void CreateEvent(Event theEvent)
		{
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = $"INSERT INTO {DBCredentials.DB_eventTable} (eventname,eventDate, allday, eventdescription, eventid, eventenddate, {(theEvent.Stickers == null ? "" : "stickers,")} eventstyle, userid, color)  " +
						$" VALUES('{theEvent.Name}', '{theEvent.Date}', '{theEvent.AllDay}', '{theEvent.Description}', '{theEvent.Id}', '{theEvent.EndDate}', {(theEvent.Stickers == null ? "" : $"'{ theEvent.Stickers.ToArrayLiteral()}',")} '{theEvent.Style}', '{theEvent.UserId}', '{theEvent.Color}')";
					cmd.ExecuteNonQuery();
				}
			}
		}

		internal static Sticker[] GetStickers(long[] ids)
		{
			if (ids == null)
				return null;
			if (ids.Length == 0)
				return null;
			List<Sticker> stickers = new List<Sticker>();
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
					foreach (long id in ids)
					{
						cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_stickerTable} WHERE stickerId='{id}'";
						using (var reader = cmd.ExecuteReader())
						{
							if (!reader.HasRows)
								continue;
							reader.Read();
							stickers.Add(new Sticker
							{
								id = id,
								name =reader.GetString(3),
								fileUrl=reader.GetString(1)
							});
						}
					}
				}
			}
			return stickers.ToArray();
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
