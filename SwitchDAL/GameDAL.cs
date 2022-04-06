using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SwitchDAL
{
    public class GameDAL
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection("server=localhost;user=root;database=switch;password=123456789");
        }

        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> gameDTOs = new List<GameDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM game", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Location = reader["location"].ToString()
                        });
                    }
                }
            }
            return gameDTOs;
        }

        public void AddGame(string Name, string Location)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO game(name, location) VALUES(@Name, @Location)", conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGame(int Id, string Name, string Location)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE game SET Name = @Name, Location = @Location WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteGame(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM game WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }
        public GameDTO GetDetails(int Id)
        {
            GameDTO gameDTO = new GameDTO();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM game WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTO.Id = Convert.ToInt32(reader["id"]);
                        gameDTO.Name = reader["name"].ToString();
                        gameDTO.Location = reader["location"].ToString();
                    }
                }
            }
            return gameDTO;
        }
    }
}
