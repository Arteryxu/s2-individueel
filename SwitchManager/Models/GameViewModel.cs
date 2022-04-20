namespace SwitchPresentation.Models
{
    public class GameViewModel
    {
        public GameViewModel(int Id, string Name, string Location)
        {
            this.Id = Id;
            this.Name = Name;
            this.Location = Location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
