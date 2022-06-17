using SmokeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeLogic
{
    public class User
    {
        public User()
        {

        }
        public User(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Name = userDTO.Name;
            Email = userDTO.Email;
            Password = userDTO.Password;
            foreach (GameDTO gameDTO in userDTO.Games)
            {
                Games.Add(new Game()
                {
                    Id = gameDTO.Id,
                    Name = gameDTO.Name
                });
            }
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }  
        public List<Game> Games = new List<Game>();
    }
}
