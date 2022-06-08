using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeLogic
{
    public class UserCollection
    {
        private IUserDAL _userDAL;

        public UserCollection(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public List<User> GetAllUsers()
        {
            List<UserDTO> userDTOs = _userDAL.GetAllUsers();
            List<User> users = new List<User>();
            foreach (UserDTO userDTO in userDTOs)
            {
                int Id = userDTO.Id;
                string Name = userDTO.Name;
                string Email = userDTO.Email;
                string Password = userDTO.Password;

                users.Add(new User { Id = Id, Name = Name, Email = Email, Password = Password });
            }
            return users;
        }

        public List<User> GetAllUserGames()
        {
            List<UserDTO> userDTOs = _userDAL.GetAllUserGames();
            List<User> users = new List<User>();
            foreach (UserDTO userDTO in userDTOs)
            {
                int Id = userDTO.Id;
                List<Game> games = new List<Game>();
                foreach(GameDTO gameDTO in userDTO.Games)
                {
                    games.Add(new Game { Id = gameDTO.Id, Name = gameDTO.Name });
                }

                users.Add(new User { Id = Id, Games = games });
            }
            return users;
        }

        public List<User> GetUserGames(int UserId)
        {
            List<UserDTO> userDTOs = _userDAL.GetUserGames(UserId);
            List<User> users = new List<User>();
            foreach (UserDTO userDTO in userDTOs)
            {
                int Id = userDTO.Id;
                List<Game> games = new List<Game>();
                foreach (GameDTO gameDTO in userDTO.Games)
                {
                    games.Add(new Game { Id = gameDTO.Id, Name = gameDTO.Name });
                }

                users.Add(new User { Id = Id, Games = games });
            }
            return users;
        }

        public void AddUser(User user, int gameId)
        {
            UserDTO userDTO = new UserDTO();

            userDTO = UserToDTO(user);

            _userDAL.AddUser(userDTO, gameId);
        }

        public void AddUserGame(int GameId, int UserId)
        {
            _userDAL.AddUserGame(GameId, UserId);
        }

        public void DeleteUser(int GameId, int UserId, User user)
        {
            UserDTO userDTO = new UserDTO();

            userDTO = UserToDTO(user);

            _userDAL.DeleteUser(GameId, UserId, userDTO);
        }

        public void DeleteUserGame(int GameId, int UserId)
        {
            _userDAL.DeleteUserGame(GameId, UserId);
        }

        public UserDTO UserToDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            List<GameDTO> gameDTOs = new List<GameDTO>();

            foreach(Game game in user.Games)
            {
                gameDTOs.Add(new GameDTO()
                {
                    Id = game.Id,
                    Name = game.Name
                });
            }

            userDTO.Id = user.Id;
            userDTO.Name = user.Name;
            userDTO.Email = user.Email;
            userDTO.Password = user.Password;
            userDTO.Games = gameDTOs;

            return userDTO;
        }
    }
}
