using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeLogic
{
    public class UserHandler
    {
        private IUserDAL _userDAL;

        public UserHandler(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public void UpdateUser(int Id, string Name, string Email, string Password)
        {

            _userDAL.UpdateUser(Id, Name);
        }

        public User GetUserDetails(int UserId)
        {
            User user = new User();
            UserDTO userDTO = _userDAL.GetUserDetails(UserId);

            user.Id = userDTO.Id;
            user.Name = userDTO.Name;
            //user.Email = userDTO.Email;
            //user.Password = userDTO.Password;

            return user;
        }
        public User GetUserGameDetails(int UserId, int GameId)
        {
            User userGame = new User();
            UserDTO userDTO = _userDAL.GetUserGameDetails(UserId, GameId);

            userGame = DTOToUserGame(userDTO);

            return userGame;
        }

        public User DTOToUserGame(UserDTO userDTO)
        {
            User userGame = new User();
            List<Game> games = new List<Game>();

            userGame.Id = userDTO.Id;
            foreach (GameDTO gameDTO in userDTO.Games)
            {
                games.Add(new Game()
                {
                    Id = gameDTO.Id,
                    Name = gameDTO.Name
                });
            }

            return userGame;
        }
    }
}
