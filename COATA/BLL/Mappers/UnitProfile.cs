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
            CreateMap<UnitTree, UnitSelectionDTO>().ReverseMap();
            CreateMap<UnitTree, UnitPlainDTO>()
                .ForMember(x=>x.Id, opt=>
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.Name, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.ParentId, opt=>
                    opt.MapFrom(src=>src.ParentId))
                .ForMember(x=>x.Classification, opt=>
                    opt.MapFrom(src=>src.UnitClassification));
        }
    }
}