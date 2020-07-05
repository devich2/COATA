using System.Threading.Tasks;
using BLL.DTO.Response;
using BLL.DTO.Result;
using BLL.DTO.Unit;

namespace BLL.Abstract.UnitTree
{
    public interface IUnitEditService
    {
        Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitCreateOrUpdateDTO model);
        Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitCreateOrUpdateDTO model);
        Task<Result> ProcessUnitDelete(int unitId);
    }
}