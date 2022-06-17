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
                properties.Add(new Property(propertyDTO));
            }
            return properties;
        }

        public void Add(Property property)
        {
            PropertyDTO propertyDTO = new PropertyDTO();
            
            propertyDTO.gameId = property.gameId;
            propertyDTO.userId = property.userId;
            propertyDTO.parentId = property.parentId;
            propertyDTO.name = property.name;
            propertyDTO.value = property.value;
            propertyDTO.type = property.type;

            _propertyDAL.Add(propertyDTO);
        }

        public void Delete(int Id)
        {
            _propertyDAL.Delete(Id);
        }
    }
}
