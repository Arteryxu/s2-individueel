using MySql.Data.MySqlClient;
using SwitchDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchLogic
{
    public class GameCollection
    {
        public List<Game> GetAllGames()
        {
            GameDAL gameDAL = new GameDAL();
            List<GameDTO> gameDTOs = gameDAL.GetAllGames();
            List<Game> games = new List<Game>();
            foreach (GameDTO gameDTO in gameDTOs)
            {
                int gameId = gameDTO.Id;
                string gameName = gameDTO.Name;
                string gameLocation = gameDTO.Location;
                games.Add(new Game { Id = gameId, Name = gameName, Location = gameLocation });
            }
            return games;
        }
        public void AddGame(string Name, string Location)
        {
            GameDAL gameDAL = new GameDAL();
            gameDAL.AddGame(Name, Location);
        }
        public void UpdateGame(int Id, string Name, string Location)
        {
            GameDAL gameDAL = new GameDAL();
            gameDAL.UpdateGame(Id, Name, Location);
        }

        public void DeleteGame(int Id)
        {
            GameDAL gameDAL = new GameDAL();
            gameDAL.DeleteGame(Id);
        }
        public Game GetDetails(int Id)
        {
            GameDAL gameDAL = new GameDAL();
            Game game = new Game();
            GameDTO gameDTO = gameDAL.GetDetails(Id);

            game.Id = gameDTO.Id;
            game.Name = gameDTO.Name;
            game.Location = gameDTO.Location;

            return game;
        }
    }
}
