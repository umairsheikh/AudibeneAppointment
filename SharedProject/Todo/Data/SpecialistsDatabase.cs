using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SQLite;

namespace TermineAudibene
{
	public class SpecialistDatabase
	{
		static object locker = new object();

		SQLiteConnection database;

		string DatabasePath
		{
			get
			{
				var sqliteFilename = "Doctors2.db3";
#if __IOS__
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
#else
#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
#else
				// WinPhone
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
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
		public SpecialistDatabase()
		{
			database = new SQLiteConnection(DatabasePath);
			// create the tables
			database.CreateTable<DoctorDetails>();
		}

		public IEnumerable<DoctorDetails> GetItems()
		{
			lock (locker)
			{
				return (from i in database.Table<DoctorDetails>() select i).ToList();
			}
		}

		public IEnumerable<DoctorDetails> GetItemsNotDone()
		{
			lock (locker)
			{
				return database.Query<DoctorDetails>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
			}
		}

		public DoctorDetails GetItem(String name)
		{
			lock (locker)				
			{
				return database.Table<DoctorDetails>().FirstOrDefault(x => x.Name == name);
														
			}
		}

		public bool IsEmptyDB()
		{ 
		lock (locker)
			{
				var x = database.Query<DoctorDetails>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
				if (x != null)
					return false;
				else
					return true;
			}
		
		}




		public int SaveItem(DoctorDetails item)
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
				return database.Delete<DoctorDetails>(id);
			}
		}
	}
}

