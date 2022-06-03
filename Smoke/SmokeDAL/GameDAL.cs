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

        public List<GameDTO> GetAllUserGames(UserDTO User)
        {
            List<GameDTO> gameDTOs = new List<GameDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Id, Name FROM game" + "SELECT UserId FROM usergame", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                            //Location = reader["location"].ToString()
                            //cmd.LastInsertedId
                        });
                    }
                }
            }
            return gameDTOs;
        }

        public void AddGame(GameDTO gameDTO, int UserId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO game(id, name) VALUES(@Id, @Name)", conn);
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO usergame(gameId, UserId) VALUES(@GameId, @UserId)", conn);

                cmd1.Parameters.AddWithValue("@Id", gameDTO.Id);
                cmd1.Parameters.AddWithValue("@Name", gameDTO.Name);
                cmd1.ExecuteNonQuery();

                int gameId = Convert.ToInt32(cmd1.LastInsertedId);

                foreach (var item in gameDTO.Users)
                {
                    cmd2.Parameters.AddWithValue("@GameId", gameId);
                    cmd2.Parameters.AddWithValue("@UserId", UserId);
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //public void AddUserGame(int GameId, int UserId)
        //{
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("INSERT INTO usergame(gameId, UserId) VALUES(@GameId, @UserId)", conn);
        //        cmd.Parameters.AddWithValue("@GameId", GameId);
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public void Update(int Id, string Name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE property SET Id = @Id, Name = @Name WHERE Id = @Id", conn);
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
                MySqlCommand cmd = new MySqlCommand("DELETE FROM game WHERE gameId = @GameId", conn);
                cmd.Parameters.AddWithValue("@GameId", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUserGame(int GameId, int UserId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM usergame WHERE gameId = @GameId, userId = @UserId", conn);
                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public PropertyDTO GetDetails(int GameId, int UserId)
        {
            PropertyDTO propertyDTO = new PropertyDTO();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT  FROM usergame WHERE gameId = @GameId, userId = @UserId", conn);
                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propertyDTO.Id = Convert.ToInt32(reader["propertyId"]);
                        propertyDTO.gameId = reader["gameId"] == DBNull.Value ? null : Convert.ToInt32(reader["gameId"]);
                        propertyDTO.parentId = reader["parentId"] == DBNull.Value ? null : Convert.ToInt32(reader["parentId"]);
                        propertyDTO.name = reader["propertyName"].ToString();
                        propertyDTO.value = reader["propertyValue"].ToString();
                        propertyDTO.type = reader["propertyType"].ToString();
                    }
                }
            }
            return propertyDTO;
        }
    }
}
