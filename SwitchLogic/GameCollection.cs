using MySql.Data.MySqlClient;
using SwitchDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchLogic
{
    public class GameCollection
    {
        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();

            using(MySqlConnection conn = GameDAL.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from game", conn);
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Location = reader["location"].ToString()
                        });
                    }
                }
            }
            return games;
        }
        public void AddGame(string Name, string Location)
        {
            using (MySqlConnection conn = GameDAL.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO game VALUES(1,", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
