using BLL.DTO.Classification;

namespace BLL.DTO.Unit
{
    public class UnitCreateOrUpdateDTO
    {
        public int Id {get; set;}
        public int ParentId {get; set;}
        public string Name {get; set;}
        public int UnitClassificationId {get; set;}
    }
}