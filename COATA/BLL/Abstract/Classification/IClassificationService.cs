using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Classification;
using BLL.DTO.Response;
using BLL.DTO.Result;

namespace BLL.Abstract.Classification
{
    public interface IClassificationService
    {
        Task<DataResult<ClassificationAddResponse>> ProcessClassificationCreate(ClassificationCreateDTO model);
        Task<DataResult<List<ClassificationDTO>>> GetClassificationByParentId (int? unitId);
    }
}