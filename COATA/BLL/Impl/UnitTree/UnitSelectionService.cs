using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract.UnitTree;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using BLL.DTO.Unit;
using DAL.Abstract;
using DAL.Entities.Tables;

namespace BLL.Impl.UnitTree
{
    public class UnitSelectionService : IUnitSelectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitSelectionService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<DataResult<UnitSelectionDTO>> GetUnitsGroupedByClassificationByParentId(int? unitId,
            int? classificationId)
        {
            List<DAL.Entities.Tables.UnitTree> unitTrees =
                await _unitOfWork.Units.GetByParentId(unitId, classificationId);
            if (!unitTrees.Any())
            {
                return new DataResult<UnitSelectionDTO>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }

            return new DataResult<UnitSelectionDTO>()
            {
                Data = FlatToHierarchy(unitTrees, unitId),
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        public async Task<DataResult<List<UnitPlainDTO>>> GetUnitsByParentId(int? unitId, int? classificationId)
        {
            List<DAL.Entities.Tables.UnitTree> unitTrees =
                await _unitOfWork.Units.GetByParentId(unitId, classificationId);
            if (!unitTrees.Any())
            {
                return new DataResult<List<UnitPlainDTO>>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }

            var mapped = unitTrees.Select(_mapper.Map<UnitPlainDTO>).ToList();
            return new DataResult<List<UnitPlainDTO>>()
            {
                Data = mapped,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        public async Task<DataResult<UnitSelectionDTO>> SearchByTypeAndNameWithParents(string name, string unitType)
        {
            List<DAL.Entities.Tables.UnitTree> uplineNodes =
                await _unitOfWork.Units.GetSinglePathByTypeAndName(name, unitType);

            if (!uplineNodes.Any())
            {
                return new DataResult<UnitSelectionDTO>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }

            List<int?> parentIds = uplineNodes.Select(x => x.ParentId).Distinct().ToList();
            List<DAL.Entities.Tables.UnitTree> expanded = await _unitOfWork.Units.GetNodesByParentIds(parentIds);

            return new DataResult<UnitSelectionDTO>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = FlatToHierarchy(expanded)
            };
        }

        public async Task<DataResult<UnitSelectionDTO>> SearchWithExpandedClassifications(string name, string unitType)
        {
            List<DAL.Entities.Tables.UnitTree> uplineNodes =
                await _unitOfWork.Units.GetSinglePathByTypeAndName(name, unitType);

            if (!uplineNodes.Any())
            {
                return new DataResult<UnitSelectionDTO>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }

            List<int?> parentIds = uplineNodes.Select(x => x.ParentId).Distinct().ToList();
            List<DAL.Entities.Tables.UnitTree> expanded = await _unitOfWork.Units.GetNodesByParentIds(parentIds);
            List<int> expandableClassifications = uplineNodes.Select(x => x.UnitClassificationId).Distinct().ToList();
            return new DataResult<UnitSelectionDTO>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = FlatToHierarchy2(expanded, expandableClassifications)
            };
        }

        private UnitSelectionDTO FlatToHierarchy2(List<DAL.Entities.Tables.UnitTree> list,
            List<int> expandableClassifications, int? topLevelId = null)
        {
            foreach (var unitTree in list.Where(x => x.ParentId == topLevelId))
            {
                unitTree.ParentId = -1;
            }

            var lookup = new Dictionary<int, UnitSelectionDTO>()
            {
                {-1, new UnitSelectionDTO()}
            };

            foreach (DAL.Entities.Tables.UnitTree item in list)
            {
                UnitSelectionDTO unitSelectionDto = _mapper.Map<UnitSelectionDTO>(item);
                ClassificationDTO classification = _mapper.Map<ClassificationDTO>(item.UnitClassification);
                if (expandableClassifications.Contains(item.UnitClassificationId))
                {
                    if (lookup.ContainsKey(item.ParentId.Value))
                    {
                        AppendIfNotExist(lookup[item.ParentId.Value].Children, classification, unitSelectionDto);
                    }
                    lookup.Add(item.Id, unitSelectionDto);
                }
            }

            foreach (var unitTree in lookup.Where(x => x.Value.ParentId == -1))
            {
                unitTree.Value.ParentId = topLevelId;
            }

            return lookup[-1];
        }

        private UnitSelectionDTO FlatToHierarchy(List<DAL.Entities.Tables.UnitTree> list, int? topLevelId = null)
        {
            foreach (var unitTree in list.Where(x => x.ParentId == topLevelId))
            {
                unitTree.ParentId = -1;
            }

            var lookup = new Dictionary<int, UnitSelectionDTO>()
            {
                {-1, new UnitSelectionDTO()}
            };

            foreach (DAL.Entities.Tables.UnitTree item in list)
            {
                UnitSelectionDTO unitSelectionDto = _mapper.Map<UnitSelectionDTO>(item);
                ClassificationDTO classification = _mapper.Map<ClassificationDTO>(item.UnitClassification);
                if (lookup.ContainsKey(item.ParentId.Value))
                {
                    AppendIfNotExist(lookup[item.ParentId.Value].Children, classification, unitSelectionDto);
                }

                lookup.Add(item.Id, unitSelectionDto);
            }

            foreach (var unitTree in lookup.Where(x => x.Value.ParentId == -1))
            {
                unitTree.Value.ParentId = topLevelId;
            }

            return lookup[-1];
        }

        private Dictionary<ClassificationDTO, List<UnitSelectionDTO>> AppendIfNotExist(
            Dictionary<ClassificationDTO, List<UnitSelectionDTO>> dictionary,
            ClassificationDTO key, UnitSelectionDTO unitSelectionDto)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(unitSelectionDto);
            }
            else
            {
                dictionary.Add(key, new List<UnitSelectionDTO>() {unitSelectionDto});
            }

            return dictionary;
        }
    }
}