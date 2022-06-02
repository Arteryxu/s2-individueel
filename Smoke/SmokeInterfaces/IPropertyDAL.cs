using SmokeDTOs;
using System.Collections.Generic;

namespace SmokeInterfaces
{
    public interface IPropertyDAL
    {
        void Add(int? GameId, int? ParentId, string Name, string Value, string propertyType);
        void Delete(int Id);
        List<PropertyDTO> GetAll();
        PropertyDTO GetDetails(int Id);
        void Update(int Id, int? GameId, int? ParentId, string name, string Value, string propertyType);
    }
}