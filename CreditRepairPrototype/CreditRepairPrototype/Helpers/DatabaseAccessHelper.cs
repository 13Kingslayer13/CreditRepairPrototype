using System;
using System.IO;
using SQLite;

namespace CreditRepairPrototype
{
	public class DatabaseAccessHelper : Singleton<DatabaseAccessHelper>
	{
		SQLiteConnection database;
		string databasePath;

		public virtual string DatabasePath {
			get
			{
				return databasePath;
			}
		}

		public bool DatabaseCreated
		{
			get
			{
				return database != null;
			}
		}

		public void CreateDatabaseConnection(string databasePath)
		{
			if (DatabaseCreated) {
				return;
			}
			this.databasePath = databasePath;
			database = new SQLiteConnection (DatabasePath);
		}

		public int Insert<T> (T data) where T : class, new()
		{
			database.CreateTable<T> ();
			return database.Insert (data);
		}

		public void UpdateItem<T> (T data) where T : DatabaseItem, new()
		{
			database.CreateTable<T> ();
			if (database.Table<T> ().Count() != 0) {
				database.Update (data);
			}
		}

		public T GetItem<T> (int id) where T : DatabaseItem, new()
		{
			database.CreateTable<T> ();
			if (database.Table<T> ().Count() == 0) {
				return null;
			}
			return database.Get<T>(id);
		}

		public void DeleteItem<T> (int id) where T : DatabaseItem, new()
		{
			database.CreateTable<T> ();
			if (database.Table<T> ().Count() != 0) {
				Debugger.Log (database.Table<T> ().Count ());
				database.Delete<T>(id);
			}			 
		}

		public void DeleteAll<T> () where T : DatabaseItem, new()
		{
			database.CreateTable<T> ();
			if (database.Table<T> ().Count() != 0) {
				database.DeleteAll<User>();
			}			 
		}
	}
}

