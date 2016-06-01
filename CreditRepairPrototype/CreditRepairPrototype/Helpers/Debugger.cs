using System;

namespace CreditRepairPrototype
{
	public static class Debugger
	{
		public static void Log(object message, Type t = null)
		{			
			string fullMessage = message.ToString ();
			if (t != null) 
			{
				fullMessage = t + ": " + message;
			}
			System.Diagnostics.Debug.WriteLine (fullMessage);
		}
	}
}

