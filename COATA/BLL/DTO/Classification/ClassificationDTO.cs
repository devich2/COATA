using DAL.Entities.Tables;

namespace BLL.DTO.Classification
{
    public class ClassificationDTO
    {
        public string CustomName {get; set;}
        public DAL.Entities.Tables.UnitType UnitType {get; set;}
    }
}