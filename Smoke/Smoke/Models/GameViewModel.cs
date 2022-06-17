using SmokeLogic;

namespace SmokeUI.Models
{
    public class GameViewModel
    {
        public GameViewModel(Game game)
        {
            Id = game.Id;
            Name = game.Name;
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
