using System.Collections.Generic;

namespace BLL.DTO.UnitType
{
    public class UnitTypeAggrDTO
    {
        public Dictionary<string, List<DAL.Entities.Tables.UnitType>> SubjectTypes {get; set;}
    }
}