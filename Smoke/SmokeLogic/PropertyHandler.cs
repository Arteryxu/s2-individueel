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

        public void Update(int Id, int? GameId, int? ParentId, string Name, string Value, string PropertyType)
        {

            _propertyDAL.Update(Id, GameId, ParentId, Name, Value, PropertyType);
        }

        public Property GetDetails(int Id)
        {
            Property property = new Property();
            PropertyDTO propertyDTO = _propertyDAL.GetDetails(Id);

            property.Id = propertyDTO.Id;
            property.gameId = propertyDTO.gameId;
            property.parentId = propertyDTO.parentId;

            return property;
        }
    }
}
