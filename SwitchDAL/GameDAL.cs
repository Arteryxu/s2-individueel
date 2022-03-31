using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace SwitchDAL
{
    public class GameDAL
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection("server=localhost;user=root;database=switch;password=123456789");
        }
    }
}
