namespace DAL.Entities.Tables
{
    public class UnitClassification
    {
        public int Id {get; set;}
        public int UnitTypeId {get; set;}
        public UnitType UnitType {get; set;}
        public string Name {get; set;}
    }
}