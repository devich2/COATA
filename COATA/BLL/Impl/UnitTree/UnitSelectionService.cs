using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract.UnitTree;
using BLL.DTO.Result;
using BLL.DTO.Unit;
using DAL.Abstract;

namespace BLL.Impl.UnitTree
{
    public class UnitSelectionService: IUnitSelectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitSelectionService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<DataResult<List<UnitPlainDTO>>> GetUnitsByParentId(int unitId)
        {
            List<DAL.Entities.Tables.UnitTree> unitTrees = await _unitOfWork.Units.GetByParentId(unitId);
            if(!unitTrees.Any())
            {
                return new DataResult<List<UnitPlainDTO>>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }
            List<UnitPlainDTO> mapped = unitTrees.Select(_mapper.Map<UnitPlainDTO>).ToList();
            return new DataResult<List<UnitPlainDTO>>()
            {
                Data = mapped,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }

        public async Task<DataResult<List<DAL.Entities.Tables.UnitTree>>> SearchByTypeAndName(string name, string unitType)
        {
            List<DAL.Entities.Tables.UnitTree> uplineNodes = await _unitOfWork.Units.GetSinglePathByTypeAndName(name, unitType);
            
            if(!uplineNodes.Any())
            {
                return new DataResult<List<DAL.Entities.Tables.UnitTree>>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }
            List<int?> parentIds = uplineNodes.Select(x=>x.ParentId).ToList();
            List<DAL.Entities.Tables.UnitTree> expanded = await _unitOfWork.Units.GetNodesByParentIds(parentIds);
            return new DataResult<List<DAL.Entities.Tables.UnitTree>>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = expanded
            };
        }
    }
}