namespace BLL.DTO.Classification
{
    public class ClassificationCreateDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public int UnitTypeId {get; set;}
        public int ParentId {get; set;}
    }
}