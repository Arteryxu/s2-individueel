using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeDAL
{
    public class GameDAL : IGameDAL
    {
        public IConfiguration _configuration;

        public GameDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            string ConnString = _configuration.GetConnectionString("Default");
            return new MySqlConnection(ConnString);
        }

        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> gameDTOs = new List<GameDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, name FROM game", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = Convert.ToInt32(reader["propertyId"]),
                            Name = reader["propertyName"].ToString()
                            //Location = reader["location"].ToString()
                        });
                    }
                }
            }
            return gameDTOs;
        }

        public void AddGame(int Id, string Name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO game(Id, Name) VALUES(@Id, @Name)", conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGame(int Id, string Name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE game SET Name = @Name WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
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
        public GameDTO GetGameDetails(int Id)
        {
            GameDTO gameDTO = new GameDTO();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT Id, Name FROM game WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", Id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTO.Id = Convert.ToInt32(reader["propertyId"]);
                        gameDTO.Name = reader["propertyName"].ToString();
                    }
                }
            }
            return gameDTO;
        }
    }
}

