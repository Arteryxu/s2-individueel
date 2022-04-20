using SwitchDTO;
using System.Collections.Generic;

namespace SwitchInterface
{
    public interface IGameCollection
    {
        //List<GameModel> GetAllGames();
        void AddGame(string Name, string Location);
        void DeleteGame(int Id);
    }
}