using System;
using SQLite;

namespace CreditRepairPrototype
{
	public class User : DatabaseItem
	{		
		public User()
		{			
		}

		[SQLite.PrimaryKey]
		public override int Id {
			get;
			set;
		}

		[MaxLength(24)]
		public string Email {
			get;
			set;
		}

		[MaxLength (4)]
		public int UserPIN {
			get;
			set;
		}

		[MaxLength(48)]
		public string SocialSecurityNumber {
			get;
			set;
		}

		#region User Names
		[MaxLength(24)]
		public string LastName {
			get;
			set;
		}

		[MaxLength (24)]
		public string MiddleName {
			get;
			set;
		}

		[MaxLength (24)]
		public string FirstName {
			get;
			set;
		}
		#endregion

		#region User Address
		[MaxLength(48)]
		public string Address {
			get;
			set;
		}
		[MaxLength(24)]
		public string City {
			get;
			set;
		}

		[MaxLength(24)]
		public string State {
			get;
			set;
		}

		[MaxLength(20)]
		public int ZipCode {
			get;
			set;
		}
		#endregion

	}
}

