using AutoMapper;
using BLL.DTO.Unit;
using DAL.Entities.Tables;

namespace BLL.Mappers
{
    public class UnitProfile: Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitCreateOrUpdateDTO, UnitTree>().ReverseMap();
        }
    }
}