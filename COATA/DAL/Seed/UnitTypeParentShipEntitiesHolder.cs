using System.Collections.Generic;
using DAL.Entities.Tables;

namespace DAL.Seed
{
    public class UnitTypeParentShipEntitiesHolder
    {
        private List<UnitTypeParentShip> _unitTypeParentShips = new List<UnitTypeParentShip>()
        {
            //Райони в областях
            new UnitTypeParentShip()
            {
                UnitTypeId = 2,
                ParentUnitTypeId = 1
            },
            //Міста в областях
            new UnitTypeParentShip()
            {
                UnitTypeId = 3,
                ParentUnitTypeId = 1
            },
            //Міста в районах
            new UnitTypeParentShip()
            {
                UnitTypeId = 3,
                ParentUnitTypeId = 2
            },
            //Сільради в районах
            new UnitTypeParentShip()
            {
                UnitTypeId = 5,
                ParentUnitTypeId = 2
            },
            //Селища в районах
            new UnitTypeParentShip()
            {
                UnitTypeId = 4,
                ParentUnitTypeId = 2
            },
            //Села в районах
            new UnitTypeParentShip()
            {
                UnitTypeId = 6,
                ParentUnitTypeId = 2
            },
            //Селища в містах
            new UnitTypeParentShip()
            {
                UnitTypeId = 4,
                ParentUnitTypeId = 3
            },
            //Райони в містах
            new UnitTypeParentShip()
            {
                UnitTypeId = 2,
                ParentUnitTypeId = 3
            },
            //Села в містах
            new UnitTypeParentShip()
            {
                UnitTypeId = 6,
                ParentUnitTypeId = 3
            },
            //Сільради в селах
            new UnitTypeParentShip()
            {
                UnitTypeId = 5,
                ParentUnitTypeId = 4
            },
            //Сільради в селищах
            new UnitTypeParentShip()
            {
                UnitTypeId = 5,
                ParentUnitTypeId = 6
            },
        };
        public List<UnitTypeParentShip> GetUnitParentShips()
        {
            return _unitTypeParentShips;
        }
    }
}