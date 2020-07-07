namespace BLL.DTO.Response
{
    public class UnitAddResponse: UnitBaseResponse
    {
        public int? ParentId {get; set;}
        public int UnitClassificationId {get; set;}
    }
}