using System;
using System.IO;

namespace CreditRepairPrototype
{
	public static class UniversalHelper
	{
		public static StreamReader GetStreamReaderFromByteArray(byte[] source)
		{			
			return new StreamReader( GetStreamFromByteArray(source) );   	
		}

		public static Stream GetStreamFromByteArray(byte[] source)
		{
			return new MemoryStream(source) as Stream;
		}
	}
}

