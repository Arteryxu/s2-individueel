using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmokeLogic
{
    public class PropertyCollection
    {
        private IPropertyDAL _propertyDAL;

        public PropertyCollection(IPropertyDAL propertyDAL)
        {
            _propertyDAL = propertyDAL;
        }

        public List<Property> GetAll()
        {
            List<PropertyDTO> propertyDTOs = _propertyDAL.GetAll();
            List<Property> properties = new List<Property>();
            foreach (PropertyDTO propertyDTO in propertyDTOs)
            {
                int Id = propertyDTO.Id;
                int? gameId = propertyDTO.gameId;
                int? parentId = propertyDTO.parentId;
                string name = propertyDTO.name;
                string value = propertyDTO.value;
                string type = propertyDTO.type;
                properties.Add(new Property { Id = Id, gameId = gameId, parentId = parentId, name = name, value = value, type = type });
            }
            return properties;
        }

        public void Add(int? GameId, int? ParentId, string Name, string Value, string PropertyType)
        {
            _propertyDAL.Add(GameId, ParentId, Name, Value, PropertyType);
        }

        public void Delete(int Id)
        {
            _propertyDAL.Delete(Id);
        }
    }
}
