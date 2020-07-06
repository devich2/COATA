using System;
using System.Security.Cryptography;
using DAL.Entities.Tables;

namespace BLL.DTO.Classification
{
    public class ClassificationDTO
    {
        public int Id {get; set;}
        public string CustomName {get; set;}
        public DAL.Entities.Tables.UnitType UnitType {get; set;}

        public override bool Equals(object? obj)
        {    
            if(ReferenceEquals(this, obj))
                return true;

            // obj is either null or not a RatiosAVG
            if (!(obj is ClassificationDTO other))
                return false;

            return other.Id == Id && other.CustomName == CustomName && Object.Equals(other.UnitType, UnitType);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + CustomName.GetHashCode() + UnitType.GetHashCode();
        }
    }
}