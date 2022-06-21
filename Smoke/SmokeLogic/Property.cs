using SmokeDTOs;
using System;

namespace SmokeLogic
{
    public class Property
    {
        public Property()
        {

        }

        public Property(PropertyDTO propertyDTO)
        {
            Id = propertyDTO.Id;
            gameId = propertyDTO.gameId;
            userId = propertyDTO.userId;
            parentId = propertyDTO.parentId;
            name = propertyDTO.name;
            value = propertyDTO.value;
            type = propertyDTO.type;
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
