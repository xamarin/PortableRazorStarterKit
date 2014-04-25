using System;
using System.IO;

namespace Congress
{
	public partial class DataAccess
	{
		public DataAccess() {
            var dbName = "congress.sqlite";
			var dataPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), dbName);
			connectionString = "URI=file:" + dataPath;
		}
	}
}

