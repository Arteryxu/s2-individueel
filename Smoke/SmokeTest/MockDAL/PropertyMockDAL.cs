using SmokeDTOs;
using SmokeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTest.MockDAL
{
    public class PropertyMockDAL : IPropertyDAL
    {
        public static List<PropertyDTO> properties = new List<PropertyDTO>();
        public List<PropertyDTO> GetAll()
        {
            return properties;
        }
        public void Add(PropertyDTO propertyDTO)
        {
            properties.Add(propertyDTO);
        }
        public void Update(int Id, int GameId, string Name, string Value, string propertyType)
        {
            PropertyDTO updatedProperty = new PropertyDTO();
            updatedProperty.name = Name;
            properties[Id - 1] = updatedProperty;
        }
        public void Delete(int Id)
        {
            List<PropertyDTO> propertyList = new List<PropertyDTO>();
            foreach (PropertyDTO property in properties)
            {
                propertyList.Add(property);
            }
            foreach (PropertyDTO property in propertyList)
            {
                properties.Remove(property);
            }
        }
        public List<PropertyDTO> GetDetails(int PropertyId, int? ParentId)
        {
            List<PropertyDTO> propertyDTOs = new List<PropertyDTO>();
            foreach(PropertyDTO property in properties)
            {
                if(property.Id == PropertyId)
                {
                    propertyDTOs.Add(property);
                }
                else if(property.parentId == PropertyId)
                {
                    propertyDTOs.Add(property);
                }
            }
            return propertyDTOs;
        }
    }
}
