using System.Collections.Generic;
using DAL.Entities.Tables;

namespace DAL.Seed
{
    public class UnitEntitiesHolder
    {
        private readonly List<UnitTree> _unitList = new List<UnitTree>()
        {
            #region Області

            new UnitTree()
            {
                Id = 1,
                ParentId = null,
                Name = "ROOT",
                UnitClassificationId = 1
            },
            new UnitTree()
            {
                Id = 2,
                ParentId = 1,
                Name = "ВІННИЦЬКА ОБЛАСТЬ/М.ВІННИЦЯ",
                UnitClassificationId = 1
            },
            new UnitTree()
            {
                Id = 3,
                ParentId = 1,
                Name = "ВОЛИНСЬКА ОБЛАСТЬ/М.ЛУЦЬК",
                UnitClassificationId = 1
            },  
            new UnitTree()
            {
                Id = 4,
                ParentId = 1,
                Name = "ДНІПРОПЕТРОВСЬКА ОБЛАСТЬ/М.ДНІПРО",
                UnitClassificationId = 1
            },  
            new UnitTree()
            {
                Id = 5,
                ParentId = 1,
                Name = "ДОНЕЦЬКА ОБЛАСТЬ/М.ДОНЕЦЬК",
                UnitClassificationId = 1
            },  
            new UnitTree()
            {
                Id = 6,
                ParentId = 1,
                Name = "ЖИТОМИРСЬКА ОБЛАСТЬ/М.ЖИТОМИР",
                UnitClassificationId = 1
            },  
            new UnitTree()
            {
                Id = 7,
                ParentId = 1,
                Name = "ЗАКАРПАТСЬКА ОБЛАСТЬ/М.УЖГОРОД",
                UnitClassificationId = 1
            },  

            #endregion

            #region Райони
            //Вінниця
            new UnitTree()
            {
                Id = 8,
                ParentId = 2,
                Name = "БАРСЬКИЙ РАЙОН/М.БАР",
                UnitClassificationId = 2
            },  
            new UnitTree()
            {
                Id = 9,
                ParentId = 2,
                Name = "БЕРШАДСЬКИЙ РАЙОН/М.БЕРШАДЬ",
                UnitClassificationId = 2
            },  
            new UnitTree()
            {
                Id = 10,
                ParentId = 2,
                Name = "ВІННИЦЬКИЙ РАЙОН/М.ВІННИЦЯ",
                UnitClassificationId = 2
            },  
            new UnitTree()
            {
                Id = 11,
                ParentId = 2,
                Name = "ГАЙСИНСЬКИЙ РАЙОН/М.ГАЙСИН",
                UnitClassificationId = 2
            },  
            new UnitTree()
            {
                Id = 12,
                ParentId = 2,
                Name = "ЖМЕРИНСЬКИЙ РАЙОН/М.ЖМЕРИНКА",
                UnitClassificationId = 2
            },  
            //Волинь
            new UnitTree()
            {
                Id = 13,
                ParentId = 3,
                Name = "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ" ,
                UnitClassificationId = 3
            }, 
            new UnitTree()
            {
                Id = 14,
                ParentId = 3,
                Name = "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ" ,
                UnitClassificationId = 3
            }, 
            new UnitTree()
            {
                Id = 15,
                ParentId = 3,
                Name = "ГОРОХІВСЬКИЙ РАЙОН/М.ГОРОХІВ" ,
                UnitClassificationId = 3
            }, 
            new UnitTree()
            {
                Id = 16,
                ParentId = 3,
                Name = "ІВАНИЧІВСЬКИЙ РАЙОН/СМТ ІВАНИЧІ" ,
                UnitClassificationId = 3
            }, 
            //Дніпро
            new UnitTree()
            {
                Id = 17,
                ParentId = 4,
                Name = "АПОСТОЛІВСЬКИЙ РАЙОН/М.АПОСТОЛОВЕ" ,
                UnitClassificationId = 4
            }, 
            new UnitTree()
            {
                Id = 18,
                ParentId = 4,
                Name = "ВАСИЛЬКІВСЬКИЙ РАЙОН/СМТ ВАСИЛЬКІВКА" ,
                UnitClassificationId = 4
            }, 
            new UnitTree()
            {
                Id = 19,
                ParentId = 4,
                Name = "ВЕРХНЬОДНІПРОВСЬКИЙ РАЙОН/М.ВЕРХНЬОДНІПРОВСЬК" ,
                UnitClassificationId = 4
            }, 
            new UnitTree()
            {
                Id = 20,
                ParentId = 4,
                Name = "ДНІПРОВСЬКИЙ РАЙОН/М.ДНІПРО" ,
                UnitClassificationId = 4
            }, 
            new UnitTree()
            {
                Id = 21,
                ParentId = 4,
                Name = "КРИВОРІЗЬКИЙ РАЙОН/М.КРИВИЙ РІГ" ,
                UnitClassificationId = 4
            }, 
            #endregion

            #region Міста в областях
            //Вінницька область
            new UnitTree()
            {
                Id = 22,
                ParentId = 2,
                Name = "ВІННИЦЯ" ,
                UnitClassificationId = 5
            }, 
            new UnitTree()
            {
                Id = 23,
                ParentId = 2,
                Name = "МОГИЛІВ-ПОДІЛЬСЬКИЙ" ,
                UnitClassificationId = 5
            }, 
            new UnitTree()
            {
                Id = 24,
                ParentId = 2,
                Name = "КОЗЯТИН" ,
                UnitClassificationId = 5
            }, 
            new UnitTree()
            {
                Id = 25,
                ParentId = 2,
                Name = "ЛАДИЖИН" ,
                UnitClassificationId = 5
            }, 
            new UnitTree()
            {
                Id = 26,
                ParentId = 2,
                Name = "ХМІЛЬНИК" ,
                UnitClassificationId = 5
            },
            //Волинь
            new UnitTree()
            {
                Id = 27,
                ParentId = 3,
                Name = "ЛУЦЬК" ,
                UnitClassificationId = 6
            },
            new UnitTree()
            {
                Id = 28,
                ParentId = 3,
                Name = "ВОЛОДИМИР-ВОЛИНСЬКИЙ" ,
                UnitClassificationId = 6
            },
            new UnitTree()
            {
                Id = 29,
                ParentId = 3,
                Name = "КОВЕЛЬ" ,
                UnitClassificationId = 6
            },
            new UnitTree()
            {
                Id = 30,
                ParentId = 3,
                Name = "НОВОВОЛИНСЬК" ,
                UnitClassificationId = 6
            },
            #endregion

            #region МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ
            //БАРСЬКИЙ РАЙОН
            new UnitTree()
            {
                Id = 31,
                ParentId = 8,
                Name = "БАР" ,
                UnitClassificationId = 7
            },
            //БЕРШАДСЬКИЙ РАЙОН/М.БЕРШАДЬ
            new UnitTree()
            {
                Id = 32,
                ParentId = 9,
                Name = "БЕРШАДЬ" ,
                UnitClassificationId = 8
            },
            //
            #endregion

            #region Сільради в районах
            //Барський район
            new UnitTree()
            {
                Id = 50,
                ParentId = 8,
                Name = "АНТОНІВИІВСЬКА/С.АНТОНІВКА" ,
                UnitClassificationId = 9
            },
            new UnitTree()
            {
                Id = 51,
                ParentId = 8,
                Name = "БАЛКІВСЬКА/С.БАЛКИ" ,
                UnitClassificationId = 9
            },
            new UnitTree()
            {
                Id = 52,
                ParentId = 8,
                Name = "ВЕРХІВСЬКА/С.ВЕРХІВКА" ,
                UnitClassificationId = 9
            },
            //БЕРШАДСЬКИЙ РАЙОН/М.БЕРШАДЬ
            new UnitTree()
            {
                Id = 60,
                ParentId = 9,
                Name = "БАЛАНІВСЬКА C" ,
                UnitClassificationId = 10
            },
            new UnitTree()
            {
                Id = 61,
                ParentId = 9,
                Name = "БИРЛІВСЬКА C" ,
                UnitClassificationId = 10
            },
            new UnitTree()
            {
                Id = 62,
                ParentId = 9,
                Name = "ВЕЛИКОКИРІЇВСЬКА/С.ВЕЛИКА КИРІЇВКА" ,
                UnitClassificationId = 10
            },
            new UnitTree()
            {
                Id = 63,
                ParentId = 9,
                Name = "ГОЛДАШІВСЬКА/С.ГОЛДАШІВКА" ,
                UnitClassificationId = 10
            },
            #endregion

            #region Селища в районах
            //ВІННИЦЬКИЙ РАЙОН/М.ВІННИЦЯ
            new UnitTree()
            {
                Id = 80,
                ParentId = 10,
                Name = "ВОРОНОВИЦЯ" ,
                UnitClassificationId = 11
            },
            new UnitTree()
            {
                Id = 80,
                ParentId = 10,
                Name = "ДЕСНА" ,
                UnitClassificationId = 11
            },
            new UnitTree()
            {
                Id = 80,
                ParentId = 10,
                Name = "СТРИЖАВКА" ,
                UnitClassificationId = 11
            },
            //ЖМЕРИНСЬКИЙ РАЙОН/М.ЖМЕРИНКА
            new UnitTree()
            {
                Id = 85,
                ParentId = 12,
                Name = "БРАЇЛІВ" ,
                UnitClassificationId = 12
            },
            //ГОРОХІВСЬКИЙ РАЙОН/М.ГОРОХІВ
            new UnitTree()
            {
                Id = 95,
                ParentId = 15,
                Name = "МАР'ЯНІВКА" ,
                UnitClassificationId = 15
            },
            new UnitTree()
            {
                Id = 96,
                ParentId = 15,
                Name = "СЕНКЕВИЧІВКА" ,
                UnitClassificationId = 15
            },
            //ІВАНИЧІВСЬКИЙ РАЙОН/СМТ ІВАНИЧІ
            new UnitTree()
            {
                Id = 100,
                ParentId = 16,
                Name = "ІВАНИЧІ" ,
                UnitClassificationId = 17
            },
            //ВАСИЛЬКІВСЬКИЙ РАЙОН/СМТ ВАСИЛЬКІВКА
            new UnitTree()
            {
                Id = 110,
                ParentId = 18,
                Name = "ВАСИЛЬКІВКА" ,
                UnitClassificationId = 20
            },
            new UnitTree()
            {
                Id = 110,
                ParentId = 18,
                Name = "ПИСЬМЕННЕ" ,
                UnitClassificationId = 20
            },
            new UnitTree()
            {
                Id = 111,
                ParentId = 18,
                Name = "ЧАПЛИНЕ" ,
                UnitClassificationId = 20
            },
            //ВЕРХНЬОДНІПРОВСЬКИЙ РАЙОН/М.ВЕРХНЬОДНІПРОВСЬ
            new UnitTree()
            {
                Id = 120,
                ParentId = 19,
                Name = "НОВОМИКОЛАЇВКА" ,
                UnitClassificationId = 25
            },
            new UnitTree()
            {
                Id = 121,
                ParentId = 19,
                Name = "ДНІПРОВСЬКЕ" ,
                UnitClassificationId = 25
            },
            #endregion
        };
        
        public List<UnitTree> GetUnitList()
        {
            return _unitList;
        }
    }
}