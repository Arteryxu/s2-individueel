

namespace SmokeUI.Models
{
    public class PropertyViewModel
    {
        public PropertyViewModel(int Id, int? gameId, int? parentId, string name, string value, string type)
        {
            this.Id = Id;
            this.gameId = gameId;
            this.parentId = parentId;
            this.name = name;
            this.value = value;
            this.type = type;
        }

        public int Id { get; set; }
        public int? gameId { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
}
