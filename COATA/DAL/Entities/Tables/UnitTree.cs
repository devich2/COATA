using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Entities.Tables
{
    public class UnitTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public UnitTree Parent {get; set;}
        public int UnitClassificationId {get; set;}
        public UnitClassification UnitClassification {get; set;}
        public int Lft { get; set; }
        public int Rgt { get; set; }
    }
}