using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server
{
	public class TestDB
	{
		public String ConnectionString { get; set; }

		public TestDB(string connectionString)
		{ 
			this.ConnectionString= connectionString;
		}
		private MySqlConnection GetConnection() { 
			return new MySqlConnection(ConnectionString);
		}
	}
}
