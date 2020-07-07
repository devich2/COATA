using AutoMapper;
using BLL.DTO.Classification;
using DAL.Entities.Tables;

namespace BLL.Mappers
{
    public class UnitClassificationProfile: Profile
    {
        public UnitClassificationProfile()
        {
            CreateMap<UnitClassification, ClassificationDTO>()
                .ForMember(x=>x.Id, opt=>
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.Name, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.UnitType, opt=>
                    opt.MapFrom(src=>src.UnitType));
        }
    }
}