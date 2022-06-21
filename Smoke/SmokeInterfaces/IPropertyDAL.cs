using SmokeDTOs;
using System.Collections.Generic;

namespace SmokeInterfaces
{
    public interface IPropertyDAL
    {
        void Add(PropertyDTO propertyDTO);
        void Delete(int Id);
        List<PropertyDTO> GetAll();
        List<PropertyDTO> GetGameProperties();
        List<PropertyDTO> GetDetails(int PropertyId, int? ParentId);
        void Update(int Id, int GameId, string name, string Value, string propertyType);
    }
}