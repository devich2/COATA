using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstract.IRepository.Base;
using DAL.Entities.Tables;

namespace DAL.Abstract.IRepository
{
    public interface IUnitTreeRepository: IGenericKeyRepository<int, UnitTree>
    {
        Task<List<UnitTree>> GetSinglePathByTypeAndName(string name, string type);
        Task<List<UnitTree>> GetNodesByParentIds(List<int?> parentIds);
        Task<List<UnitTree>> GetByParentId(int? parentId);
        Task DeleteSubTree(int unitId);
        Task UpdateBowers(int unitId, int parentId);
        Task<UnitTree> GetByIdExpandedAsync(int id);
    }
}