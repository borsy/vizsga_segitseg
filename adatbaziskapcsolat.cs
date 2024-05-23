using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI20240502
{
    internal class Adatbazis_kapcsolat
    {
        static MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Port = 3306,
            UserID = "root",
            Password = "",
            Database = "tallest_buildings"
        };
        public static MySqlConnection adatbazis_kapcsolat = new MySqlConnection(mscsb.ConnectionString);
    }
}