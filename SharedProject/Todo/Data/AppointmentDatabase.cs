using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace TermineAudibene
{
	public class AppointmentDatabse
	{
		static object locker = new object();

		SQLiteConnection database;
		string path;
		string DatabasePath
		{
			get
			{
				var sqliteFilename = "Appointments2.db3";
#if __IOS__
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
				path = Path.Combine(libraryPath, sqliteFilename);
#else
#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			    path = Path.Combine(documentsPath, sqliteFilename);
#else
				// WinPhone
				path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
#endif
#endif
				return path;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public AppointmentDatabse()
		{
			database = new SQLiteConnection(DatabasePath);
			// create the tables
			database.CreateTable<Appointment>();
		}

		public IEnumerable<Appointment> GetItems()
		{
			lock (locker)
			{
				return (from i in database.Table<Appointment>() select i).ToList();
			}
		}

		public IEnumerable<Appointment> GetItemsNotDone()
		{
			lock (locker)
			{
				return database.Query<Appointment>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
			}
		}

		public Appointment GetItem(int id)
		{
			lock (locker)
			{
				return database.Table<Appointment>().FirstOrDefault(x => x.ID == id);
			}
		}


		public bool IsEmptyDB()
		{
			lock (locker)
			{
				string query = string.Format("SELECT * FROM [Appointment] ", DatabasePath);
				var x= database.Query<Appointment>(query);
				if (x == null)
					return true;
				else
					return false;
			}

		}

		public int SaveItem(Appointment item)
		{
			lock (locker)
			{
				if (item.ID != 0)
				{
					database.Update(item);
					return item.ID;
				}
				else
				{
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker)
			{
				return database.Delete<Appointment>(id);
			}
		}
	}
}

