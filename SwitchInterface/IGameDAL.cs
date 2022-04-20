using SwitchDTO;
using System.Collections.Generic;

namespace SwitchInterface
{
    public interface IGameDAL
    {
        void AddGame(string Name, string Location);
        void DeleteGame(int Id);
        List<GameDTO> GetAllGames();
        GameDTO GetDetails(int Id);
        void UpdateGame(int Id, string Name, string Location);
    }
}
