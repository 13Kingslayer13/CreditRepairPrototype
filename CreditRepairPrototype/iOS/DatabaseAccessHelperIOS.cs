using System;
using System.IO;

namespace CreditRepairPrototype.iOS
{
	public class DatabaseAccessHelperIOS : DatabaseAccessHelper
	{
		string path;

		public override string DatabasePath {
			get
			{
				if (path == "") {
					path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "ormdemo.db3");
				}
				return path;
			}
		}
	}
}

