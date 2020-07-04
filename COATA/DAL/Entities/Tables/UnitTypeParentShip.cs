namespace DAL.Entities.Tables
{
    public class UnitTypeParentShip
    {
        public int UnitTypeId {get; set;}
        public UnitType UnitType {get; set;}
        public int ParentUnitTypeId {get; set;}
        public UnitType ParentUnitType {get; set;}
    }
}