using System.Collections.Generic;
using DAL.Entities.Tables;

namespace DAL.Seed
{
    public class UnitClassificationEntitiesHolder
    {
        private List<UnitClassification> _unitClassifications = new List<UnitClassification>()
        {
            new UnitClassification()
            {
                Id = 1,
                UnitTypeId = 1,
                Name = "Області"
            },
            new UnitClassification()
            {
                Id = 2,
                UnitTypeId = 2,
                Name = "РАЙОНИ ВІННИЦЬКОЇ ОБЛАСТІ"
            },
            new UnitClassification()
            {
                Id = 3,
                UnitTypeId = 2,
                Name = "РАЙОНИ ВОЛИНСЬКОЇ ОБЛАСТІ"
            },
            new UnitClassification()
            {
                Id = 4,
                UnitTypeId = 2,
                Name = "РАЙОНИ ДНІПРОПЕТРОВСЬКОЇ ОБЛАСТІ"
            },
            new UnitClassification()
            {
                Id = 5,
                UnitTypeId = 3,
                Name = "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВІННИЦЬКОЇ ОБЛАСТІ"
            },
            new UnitClassification()
            {
                Id = 6,
                UnitTypeId = 3,
                Name = "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВОЛИНСЬКОЇ ОБЛАСТІ"
            },
            new UnitClassification()
            {
                Id = 7,
                UnitTypeId = 3,
                Name = "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БАРСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 8,
                UnitTypeId = 3,
                Name = "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БЕРШАДСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 9,
                UnitTypeId = 5,
                Name = "СІЛЬРАДИ БАРСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 10,
                UnitTypeId = 5,
                Name = "СІЛЬРАДИ БЕРШАДСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 11,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ВІННИЦЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 12,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ЖМЕРИНСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 15,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ГОРОХІВСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 17,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ІВАНИЧІВСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 20,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ВАСИЛЬКІВСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 25,
                UnitTypeId = 4,
                Name = "СЕЛИЩА МІСЬКОГО ТИПУ ВЕРХНЬОДНІПРОВСЬКОГО Р-НУ"
            },
            new UnitClassification()
            {
                Id = 35,
                UnitTypeId = 7,
                Name = "ROOT"
            },
        };
        public List<UnitClassification> GetClassifications()
        {
            return _unitClassifications;
        }
    }
}