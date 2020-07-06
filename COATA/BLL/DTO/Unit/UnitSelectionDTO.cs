using System.Collections.Generic;
using BLL.DTO.Classification;
using BLL.Impl.UnitTree;
using Newtonsoft.Json;

namespace BLL.DTO.Unit
{
    public class UnitSelectionDTO
    {
        public int Id {get; set;}
        public int? ParentId {get; set;}
        public string Name {get; set;}
        public Dictionary<ClassificationDTO, List<UnitSelectionDTO>> Children {get; set;}
        public UnitSelectionDTO()
        {
            Children = new Dictionary<ClassificationDTO, List<UnitSelectionDTO>>();
        }
    }
}