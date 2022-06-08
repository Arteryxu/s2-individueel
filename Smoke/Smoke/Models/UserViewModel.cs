using SmokeLogic;
using System.Collections.Generic;

namespace SmokeUI.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            Email = user.Email;
            foreach(Game game in user.Games)
            {
                Games.Add(new GameViewModel()
                {
                    Id = game.Id,
                    Name = game.Name
                });
            }
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<GameViewModel> Games { get; set; }
    }
}
