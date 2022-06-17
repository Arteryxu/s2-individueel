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
            return new User(_userDAL.GetUserDetails(UserId));
        }

        public User GetUserGameDetails(int UserId, int GameId)
        {
            return new User(_userDAL.GetUserGameDetails(UserId, GameId));
        }
    }
}
