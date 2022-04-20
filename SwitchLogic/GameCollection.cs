using SwitchDTO;
using SwitchInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchLogic
{
    public class GameCollection
    {
        IGameDAL _gameDAL;

        public GameCollection(IGameDAL gameDAL)
        {
            _gameDAL = gameDAL;
        }

        public List<GameModel> GetAllGames()
        {
            List<GameDTO> gameDTOs = _gameDAL.GetAllGames();
            List<GameModel> games = new List<GameModel>();
            foreach (GameDTO gameDTO in gameDTOs)
            {
                int gameId = gameDTO.Id;
                string gameName = gameDTO.Name;
                string gameLocation = gameDTO.Location;
                games.Add(new GameModel { Id = gameId, Name = gameName, Location = gameLocation });
            }
            return games;
        }

        public void AddGame(string Name, string Location)
        {
            _gameDAL.AddGame(Name, Location);
        }

        public void DeleteGame(int Id)
        {
            _gameDAL.DeleteGame(Id);
        }

        public void SortSwitchFirst()
        {

        }
    }
}
