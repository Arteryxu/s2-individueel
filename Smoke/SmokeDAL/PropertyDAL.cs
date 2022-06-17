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
    public class PropertyDAL : IPropertyDAL
    {
        public IConfiguration _configuration;

        public PropertyDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            string ConnString = _configuration.GetConnectionString("Default");
            return new MySqlConnection(ConnString);
        }

        public List<PropertyDTO> GetAll()
        {
            List<PropertyDTO> propertyDTOs = new List<PropertyDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT propertyId, gameId, userId, parentId, propertyName, propertyValue FROM property", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propertyDTOs.Add(new PropertyDTO()
                        {
                            Id = Convert.ToInt32(reader["propertyId"]),
                            gameId = Convert.ToInt32(reader["gameId"]),
                            userId = Convert.ToInt32(reader["userId"]),
                            parentId = reader["parentId"] == DBNull.Value ? null : Convert.ToInt32(reader["parentId"]),
                            name = reader["propertyName"].ToString(),
                            value = reader["propertyValue"].ToString()
                            //Location = reader["location"].ToString()
                        });
                    }
                }
            }
            return propertyDTOs;
        }

        public List<PropertyDTO> GetGameProperties()
        {
            List<PropertyDTO> propertyDTOs = new List<PropertyDTO>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT propertyId, gameId, userId, parentId, propertyName, propertyValue FROM property", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propertyDTOs.Add(new PropertyDTO()
                        {
                            Id = Convert.ToInt32(reader["propertyId"]),
                            gameId = Convert.ToInt32(reader["gameId"]),
                            userId = Convert.ToInt32(reader["userId"]),
                            parentId = reader["parentId"] == DBNull.Value ? null : Convert.ToInt32(reader["parentId"]),
                            name = reader["propertyName"].ToString(),
                            value = reader["propertyValue"].ToString()
                            //Location = reader["location"].ToString()
                        });
                    }
                }
            }
            return propertyDTOs;
        }

        public void Add(PropertyDTO propertyDTO)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO property(propertyId, gameId, userId, parentId, propertyName, propertyValue, propertyType) " +
                    "VALUES(0, @GameId, @UserId, @ParentId, @Name, @Value, @Type)", conn);
                cmd.Parameters.AddWithValue("@GameId", propertyDTO.gameId);
                cmd.Parameters.AddWithValue("@UserId", propertyDTO.userId);
                cmd.Parameters.AddWithValue("@ParentId", propertyDTO.parentId);
                cmd.Parameters.AddWithValue("@Name", propertyDTO.name);
                cmd.Parameters.AddWithValue("@Value", propertyDTO.value);
                cmd.Parameters.AddWithValue("@Type", propertyDTO.type);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int Id, int GameId, int? ParentId, string Name, string Value, string propertyType)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE property SET gameId = @GameId, parentId = @ParentId, propertyName = @Name, propertyValue = @Value, propertyType = @Type " +
                    "WHERE propertyId = @PropertyId", conn);
                cmd.Parameters.AddWithValue("@PropertyId", Id);
                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@ParentId", ParentId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Value", Value);
                cmd.Parameters.AddWithValue("@Type", propertyType);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM property WHERE propertyId = @PropertyId", conn);
                cmd.Parameters.AddWithValue("@PropertyId", Id);
                cmd.ExecuteNonQuery();
            }
        }
        public List<PropertyDTO> GetDetails(int PropertyId, int? ParentId)
        {
            List<PropertyDTO> propertyDTOs = new List<PropertyDTO>();
            

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT propertyId, gameId, userId, parentId, propertyName, propertyValue, propertyType FROM property WHERE PropertyId = @PropertyId", conn);
                MySqlCommand cmd2 = new MySqlCommand("SELECT propertyId, gameId, userId, parentId, propertyName, propertyValue, propertyType FROM property WHERE ParentId = @PropertyId", conn);

                cmd1.Parameters.AddWithValue("@PropertyId", PropertyId);
                cmd2.Parameters.AddWithValue("@PropertyId", PropertyId);

                using (MySqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PropertyDTO propertyDTO = new PropertyDTO();
                        propertyDTO.Id = Convert.ToInt32(reader["propertyId"]);
                        propertyDTO.gameId = Convert.ToInt32(reader["gameId"]);
                        propertyDTO.userId = Convert.ToInt32(reader["userId"]);
                        propertyDTO.parentId = reader["parentId"] == DBNull.Value ? null : Convert.ToInt32(reader["parentId"]);
                        propertyDTO.name = reader["propertyName"].ToString();
                        propertyDTO.value = reader["propertyValue"].ToString();
                        propertyDTO.type = reader["propertyType"].ToString();
                        propertyDTOs.Add(propertyDTO);
                    }
                }

                using (MySqlDataReader reader = cmd2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PropertyDTO propertyDTO = new PropertyDTO();
                            propertyDTO.Id = Convert.ToInt32(reader["propertyId"]);
                            propertyDTO.gameId = Convert.ToInt32(reader["gameId"]);
                            propertyDTO.userId = Convert.ToInt32(reader["userId"]);
                            propertyDTO.parentId = reader["parentId"] == DBNull.Value ? null : Convert.ToInt32(reader["parentId"]);
                            propertyDTO.name = reader["propertyName"].ToString();
                            propertyDTO.value = reader["propertyValue"].ToString();
                            propertyDTO.type = reader["propertyType"].ToString();
                            propertyDTOs.Add(propertyDTO);
                        }
                    }
                }
            return propertyDTOs;
        }
    }
}
