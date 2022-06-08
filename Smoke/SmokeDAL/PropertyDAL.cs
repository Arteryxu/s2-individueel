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
                MySqlCommand cmd = new MySqlCommand("SELECT propertyId, gameId, parentId, propertyName, propertyValue FROM property", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propertyDTOs.Add(new PropertyDTO()
                        {
                            Id = Convert.ToInt32(reader["propertyId"]),
                            gameId = reader["gameId"] == DBNull.Value ? null : Convert.ToInt32(reader["gameId"]),
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

        public void Add(int? GameId, int? ParentId, string Name, string Value, string propertyType)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO property(propertyId, gameId, parentId, propertyName, propertyValue, propertyType) VALUES(0, @GameId, @ParentId, @Name, @Value, @Type)", conn);
                cmd.Parameters.AddWithValue("@GameId", GameId);
                cmd.Parameters.AddWithValue("@ParentId", ParentId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Value", Value);
                cmd.Parameters.AddWithValue("@Type", propertyType);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int Id, int? GameId, int? ParentId, string Name, string Value, string propertyType)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE property SET gameId = @GameId, parentId = @ParentId, propertyName = @Name, propertyValue = @Value, propertyType = @Type WHERE propertyId = @PropertyId", conn);
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
        public PropertyDTO GetDetails(int Id)
        {
            PropertyDTO propertyDTO = new PropertyDTO();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT propertyId, gameId, parentId, propertyName, propertyValue, propertyType FROM property WHERE PropertyId = @PropertyId", conn);
                cmd.Parameters.AddWithValue("@PropertyId", Id);

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
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return propertyDTO;
        }
    }
}
