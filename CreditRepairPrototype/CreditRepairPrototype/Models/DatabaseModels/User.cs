using System;
using SQLite;

namespace CreditRepairPrototype
{
	public class User : DatabaseItem
	{		
		public User()
		{			
		}

		[MaxLength(24)]
		public string Email {
			get;
			set;
		}

		[MaxLength(24)]
		public string LastName {
			get;
			set;
		}

		[MaxLength (24)]
		public string FirstName {
			get;
			set;
		}

		[SQLite.PrimaryKey]
		public override int Id {
			get;
			set;
		}
	}
}

