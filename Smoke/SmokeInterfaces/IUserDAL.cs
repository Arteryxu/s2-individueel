using SmokeDTOs;
using System.Collections.Generic;

namespace SmokeInterfaces
{
    public interface IUserDAL
    {
        void AddUser(UserDTO userDTO);
        void AddUserGame(int GameId, int? UserId);
        void DeleteUser(int UserId);
        void DeleteUserGame(int? GameId, int? UserId);
        List<UserDTO> GetAllUserGames();
        List<UserDTO> GetAllUsers();
        UserDTO GetUserDetails(int UserId);
        UserDTO GetUserGameDetails(int GameId, int UserId);
        List<UserDTO> GetUserGames(int UserId);
        void UpdateUser(int Id, string Name);
    }
}