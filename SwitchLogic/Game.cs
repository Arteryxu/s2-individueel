using SwitchDTO;
using SwitchInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchLogic
{
    public class Game
    {
        IGameDAL _gameDAL;

        public Game(IGameDAL gameDAL)
        {
            _gameDAL = gameDAL;
        }

        public void UpdateGame(int Id, string Name, string Location)
        {

            _gameDAL.UpdateGame(Id, Name, Location);
        }

        public GameModel GetDetails(int Id)
        {
            GameModel gameModel = new GameModel();
            GameDTO gameDTO = _gameDAL.GetDetails(Id);

            gameModel.Id = gameDTO.Id;
            gameModel.Name = gameDTO.Name;
            gameModel.Location = gameDTO.Location;

            return gameModel;
        }
    }
}
