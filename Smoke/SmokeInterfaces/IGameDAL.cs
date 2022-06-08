using SmokeDTOs;
using System.Collections.Generic;

namespace SmokeInterfaces
{
    public interface IGameDAL
    {
        void AddGame(int Id, string Name);
        void DeleteGame(int Id);
        List<GameDTO> GetAllGames();
        GameDTO GetGameDetails(int Id);
        void UpdateGame(int Id, string Name);
    }
}