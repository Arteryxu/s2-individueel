using SmokeLogic;

namespace SmokeUI.Models
{
    public class PropertyViewModel
    {
        public PropertyViewModel(Property property)
        {
            Id = property.Id;
            gameId = property.gameId;
            userId = property.userId;
            parentId = property.parentId;
            name = property.name;
            value = property.value;
            type = property.type;
        }

        public int Id { get; set; }
        public int gameId { get; set; }
        public int userId { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
}
