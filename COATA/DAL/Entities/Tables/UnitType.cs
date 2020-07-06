namespace DAL.Entities.Tables
{
    public class UnitType
    {
        public int Id {get; set;}
        public string Name {get; set;}
    
        public override bool Equals(object? obj)
        {
            if (obj is UnitType compositeKey)
            {
                return Id == compositeKey.Id &&
                       Name == compositeKey.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode();
        }
    }
}