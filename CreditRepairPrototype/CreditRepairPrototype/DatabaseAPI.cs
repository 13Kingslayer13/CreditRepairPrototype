using System;

namespace CreditRepairPrototype
{
	public class DatabaseAPI<T> : Singleton<T> where T:class, new()
	{
		public virtual string DatabasePath {
			get;
		}

		public DatabaseAPI()
		{
			DatabaseAccessHelper.Instance.CreateDatabaseConnection (DatabasePath);
		}

		public User GetUser()
		{
			//DatabaseAccessHelper.Instance.DeleteAll<User> ();
			User user = DatabaseAccessHelper.Instance.GetItem<User> (0);
			if (user == null) {
				user = new User ();
				user.Id = 0;
				DatabaseAccessHelper.Instance.Insert<User> (user);
				user = DatabaseAccessHelper.Instance.GetItem<User> (0);
			}
			return user;
		}

		public void UpdateUser(User user)
		{
			DatabaseAccessHelper.Instance.UpdateItem<User> (user);
		}
	}
}

