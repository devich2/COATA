using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstract.IRepository.Base;
using DAL.Entities.Tables;

namespace DAL.Abstract.IRepository
{
    public interface IUnitClassificationRepository: IGenericKeyRepository<int, UnitClassification>
    {
        Task<List<UnitClassification>> GetClassificationByParentId(int? unitId);
    }
}