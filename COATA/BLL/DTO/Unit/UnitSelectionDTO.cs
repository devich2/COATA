using System.Collections.Generic;
using BLL.DTO.Classification;

namespace BLL.DTO.Unit
{
    public class UnitSelectionDTO
    {
        public int Id {get; set;}
        public int ParentId {get; set;}
        public string Name {get; set;}
        public Dictionary<ClassificationDTO, UnitSelectionDTO> Children {get; set;}
    }
}