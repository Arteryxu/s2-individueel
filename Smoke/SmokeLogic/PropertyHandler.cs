using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeLogic
{
    public class PropertyHandler
    {
        private IPropertyDAL _propertyDAL;

        public PropertyHandler(IPropertyDAL propertyDAL)
        {
            _propertyDAL = propertyDAL;
        }

        public PropertyHandler()
        {
        }

        public void Update(int Id, int GameId, string Name, string Value, string PropertyType)
        {

            _propertyDAL.Update(Id, GameId, Name, Value, PropertyType);
        }

        public List<Property> GetDetails(int PropertyId, int? ParentId)
        {
            List<Property> properties = new List<Property>();
            List<PropertyDTO> propertyDTOs = _propertyDAL.GetDetails(PropertyId, ParentId);

            foreach(PropertyDTO propertyDTO in propertyDTOs)
            {
                properties.Add(new Property(propertyDTO));
                
            }

            return properties;
        }
    }
}
