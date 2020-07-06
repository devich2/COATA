using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Classification;
using BLL.DTO.Result;

namespace BLL.Abstract.Classification
{
    public interface IClassificationService
    {
        Task<DataResult<List<ClassificationDTO>>> GetClassificationByParentId (int? unitId);
    }
}