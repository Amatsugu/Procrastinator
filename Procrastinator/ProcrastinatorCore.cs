using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Procrastinator.Credentials;
using Procrastinator.Models;
using Nancy.Authentication.Stateless;
using Nancy.Security;
using System.Security.Cryptography;

namespace Procrastinator
{
	public static class ProcrastinatorCore
	{
		public const string HOST = "karuta.luminousvector.com";


		private static Dictionary<string, IUserIdentity> loggedInUsers = new Dictionary<string, IUserIdentity>();

		internal static StatelessAuthenticationConfiguration StatelessConfig { get; private set; } = new StatelessAuthenticationConfiguration(nancyContext =>
		{
			try
			{
				string ApiKey = nancyContext.Request.Cookies.First(c => c.Key == "Auth").Value;
				Console.WriteLine($"AuthKey KEY: {ApiKey}");
				return GetUserFromAuthKey(ApiKey);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		});

		public static bool Init()
		{
			return true;
		}

		private static string HashPassword(string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			return Convert.ToBase64String(hashBytes);
		}

		private static bool VerifyPassword(string password, string passwordHash)
		{
			if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
				return false;
			/* Extract the bytes */
			byte[] hashBytes = Convert.FromBase64String(passwordHash);
			/* Get the salt */
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			/* Compute the hash on the password the user entered */
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			/* Compare the results */
			for (int i = 0; i < 20; i++)
				if (hashBytes[i + 16] != hash[i])
					return false;
			return true;
		}

		internal static string ValidateUser(LoginCredentialsModel user)   /* Switch the empty string to authorization thingie :) */
		{
			if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
				return null;
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
                    cmd.CommandText = $"SELECT * FROM{DBCredentials.DB_userTable} WHERE username = {username}";

                    using (var reader = cmd.ExecuteReader())
                        reader.Read();

                    LoginCredentialsModel crosscheck = new LoginCredentialsModel
                    {
                        Username = username,
                        Password = password
                    };
					
                    if(VerifyPassword(user.Password, crosscheck.Password)) 
                            return "";
                    else
                        return null;
				}
			}
		}

		private static IUserIdentity GetUserFromAuthKey(string apiKey) => loggedInUsers[apiKey];

		public static NpgsqlConnection GetConnection()
		{
			var con = new NpgsqlConnection(DBCredentials.CONNECTION_STRING);
			con.Open();
			return con;
		}


		/// <summary>
		/// Retrieves a list of events for a specified month
		/// </summary>
		/// <param name="year">Year of the event</param>
		/// <param name="month">Month of the event</param>
		/// <returns></returns>
		public static Event[] GetEventsFrom(int year, int month)
		{
            String s = "" + year + "," + month;
            DateTime searchable = Convert.ToDateTime(s);
            List<Event> Events = new List<Event>();

            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable} WHERE eventDate='{searchable}'";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;
                        reader.Read();
                        long[] stickerIds = reader.GetValue(6) as long[];
                        Events.Add(new Event
                        {


                            Id = reader.GetInt64(4),
                            UserId = reader.GetInt64(7),
                            Name = reader.GetString(0),
                            Date = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(5),
                            AllDay = reader.GetBoolean(2),
                            Description = reader.GetString(3),
                            Style = (Event.EventStyle)Enum.Parse(typeof(Event.EventStyle), reader.GetString(9), true),
                            Color = reader.GetString(8),
                            Stickers = GetStickers(stickerIds)
                        });
                        return Events.ToArray();
                    }
                }
            }

		}

		/// <summary>
		/// Get a list of events for a specified day
		/// </summary>
		/// <param name="year">Year fo the event</param>
		/// <param name="month">Month of the event</param>
		/// <param name="day">Day of the event</param>
		/// <returns></returns>
		public static Event[] GetEventsFrom(int year, int month, int day)
		{
            String s = "" + year + "," + month + "," +day;
            DateTime searchable = Convert.ToDateTime(s);
            List<Event> Events = new List<Event>();

            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable} WHERE eventDate='{searchable}'";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                            return null;
                        reader.Read();
                        long[] stickerIds = reader.GetValue(6) as long[];
                        Events.Add(new Event
                        {


                            Id = reader.GetInt64(4),
                            UserId = reader.GetInt64(7),
                            Name = reader.GetString(0),
                            Date = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(5),
                            AllDay = reader.GetBoolean(2),
                            Description = reader.GetString(3),
                            Style = (Event.EventStyle)Enum.Parse(typeof(Event.EventStyle), reader.GetString(9), true),
                            Color = reader.GetString(8),
                            Stickers = GetStickers(stickerIds)
                        });
                        return Events.ToArray();
                    }
                }
            }
        }

		public static Event GetEvent(long id)
		{

			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_eventTable} WHERE eventId='{id}'";
					using (var reader = cmd.ExecuteReader())
					{
                 
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

		public static void CreateEvent(Event theEvent)
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

		public static void RemoveEvent(long id)
		{
			using (var con = GetConnection())
			{
				using (var cmd = con.CreateCommand())
				{
                    cmd.CommandText = $"DELETE FROM {DBCredentials.DB_eventTable} WHERE eventid = {id}";
                    cmd.ExecuteNonQuery();
				}
			}
		}

		public static Sticker[] GetStickers(long[] ids)
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
								Id = id,
								Name =reader.GetString(3),
								FileUrl=reader.GetString(1)
							});
						}
					}
				}
			}
			return stickers.ToArray();
		}

		public static Sticker GetSticker(long id)
		{

            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_stickerTable} WHERE stickerId='{id}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                       
                        reader.Read();

                        return new Sticker
                        {

                            Id = id,
                            Name = reader.GetString(3),
                            FileUrl = reader.GetString(1)
                        };

                    }
                }
            }
		}


		public static void CreateSticker(Sticker sticker)
		{
            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {DBCredentials.DB_stickerTable} VALUES {sticker}";
                    cmd.ExecuteNonQuery();
                }
            }
		}


		public static void RemoveSticker(long id)
		{
            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {DBCredentials.DB_stickerTable} WHERE stickerid = {id}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

		public static Event[] GetAllEvents() //TODO: Retrieive All Events
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

		public static User GetUser(long id)
		{
            using(var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DBCredentials.DB_userTable} WHERE userId='{id}'";
                    using (var reader = cmd.ExecuteReader())
                    {

                        reader.Read();

                        return new User
                        {

                            UserId = id,
                            UserName = reader.GetString(1),
                            password= reader.GetString(2)
                        };

                    }
                }
            }
        }

        public static void createUser(LoginCredentialsModel user)
        {
            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    User userx = new User {
                        
                        UserName = username,
                    password=HashPassword(password)
                    };
                

                
                    cmd.CommandText = $"INSERT INTO {DBCredentials.DB_userTable} VALUES {userx}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

		public static void RemoveUser(long id)
		{
            using (var con = GetConnection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {DBCredentials.DB_userTable} WHERE userid = {id}";
                    cmd.ExecuteNonQuery();
                }
            }
        }
	}
}
