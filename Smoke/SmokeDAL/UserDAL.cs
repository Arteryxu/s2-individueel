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
    public class UserDAL : IUserDAL
    {
        public IConfiguration _configuration;

        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            string ConnString = _configuration.GetConnectionString("Default");
            return new MySqlConnection(ConnString);
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Id, Name, Email, Password FROM user", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        users.Add(new UserDTO()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public List<UserDTO> GetAllUserGames()
        {

            List<UserDTO> userDTOs = new List<UserDTO>();


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand
                    ("SELECT user.Id, user.Name as userName, usergame.GameId, game.Name as gameName FROM user LEFT JOIN usergame ON user.Id = usergame.UserId " +
                    "LEFT JOIN game ON usergame.GameId = game.Id", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<GameDTO> gameDTOs = new List<GameDTO>();
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = reader["GameId"] == DBNull.Value ? null : Convert.ToInt32(reader["GameId"]),
                            Name = reader["gameName"].ToString()
                        });
                        userDTOs.Add(new UserDTO()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["userName"].ToString(),
                            Games = gameDTOs
                        });
                    }
                }
            }
            return userDTOs;
        }

        public List<UserDTO> GetUserGames(int UserId)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand
                    ("SELECT user.Id as UserId, usergame.GameId as GameId, game.Name FROM user LEFT JOIN usergame ON user.Id = usergame.UserId LEFT JOIN game ON usergame.GameId = game.Id WHERE UserId = @UserId", conn);

                cmd.Parameters.AddWithValue("@UserId", UserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<GameDTO> gameDTOs = new List<GameDTO>();
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = Convert.ToInt32(reader["GameId"]),
                            Name = reader["Name"].ToString()
                        });
                        userDTOs.Add(new UserDTO()
                        {
                            Id = Convert.ToInt32(reader["UserId"]),
                            Games = gameDTOs
                        });
                    }
                }
            }
            return userDTOs;
        }

        public void AddUser(UserDTO userDTO)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO user(name, email, password) VALUES(@Name, @Email, @Password)", conn);

                cmd.Parameters.AddWithValue("@Name", userDTO.Name);
                cmd.Parameters.AddWithValue("@Email", userDTO.Email);
                cmd.Parameters.AddWithValue("@Password", userDTO.Password);
                cmd.ExecuteNonQuery();

                //int userId = Convert.ToInt32(cmd1.LastInsertedId);

                //foreach (var item in userDTO.Games)
                //{
                //    cmd2.Parameters.AddWithValue("@GameId", gameId);
                //    cmd2.Parameters.AddWithValue("@UserId", userId);
                //    cmd2.ExecuteNonQuery();
                //}
            }
        }

        public void AddUserGame(int GameId, int? UserId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO usergame(gameId, userId) VALUES(@GameId, @UserId)", conn);

                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(int Id, string Name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE game SET Id = @Id, Name = @Name WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int UserId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand("DELETE FROM user WHERE Id = @Userid", conn);
                MySqlCommand cmd2 = new MySqlCommand("DELETE FROM usergame WHERE userId = @UserId", conn);

                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.ExecuteNonQuery();

                cmd2.Parameters.AddWithValue("@UserId", UserId);
                cmd2.ExecuteNonQuery();
            }
        }

        public void DeleteUserGame(int? GameId, int? UserId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM usergame WHERE gameId = @GameId AND userId = @UserId", conn);

                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public UserDTO GetUserDetails(int UserId)
        {
            UserDTO userDTO = new UserDTO();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT Id, Name, Email, Password FROM user WHERE Id = @UserId", conn);

                cmd.Parameters.AddWithValue("@UserId", UserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDTO.Id = Convert.ToInt32(reader["uId"]);
                        userDTO.Name = reader["Name"].ToString();
                    }
                }
            }
            return userDTO;
        }

        public UserDTO GetUserGameDetails(int GameId, int UserId)
        {
            UserDTO userDTO = new UserDTO();
            List<GameDTO> gameDTOs = new List<GameDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT gameId, userId, game.Name FROM usergame LEFT JOIN game ON usergame.GameId = game.Id WHERE gameId = @GameId AND userId = @UserId", conn);

                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameDTOs.Add(new GameDTO()
                        {
                            Id = Convert.ToInt32(reader["gameId"]),
                            Name = reader["Name"].ToString()
                        });
                        userDTO.Id = Convert.ToInt32(reader["userId"]);
                        userDTO.Games = gameDTOs;
                    }

                }
            }
            return userDTO;
        }
    }
}
