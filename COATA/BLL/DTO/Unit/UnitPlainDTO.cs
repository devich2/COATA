using DAL.Entities.Tables;

namespace BLL.DTO.Unit
{
    public class UnitPlainDTO
    {
        public int Id {get; set;}
        public int? ParentId {get; set;}
        public string Name {get; set;}
        public UnitClassification Classification {get; set;}
    }
}