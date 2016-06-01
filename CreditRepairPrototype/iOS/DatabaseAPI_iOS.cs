using System;
using System.IO;

namespace CreditRepairPrototype.iOS
{
	public class DatabaseAPI_iOS : DatabaseAPI<DatabaseAPI_iOS>
	{
		public override string DatabasePath {
			get {
				return Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal),"database.db3");
			}
		}
	}
}

