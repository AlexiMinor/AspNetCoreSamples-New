using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;
using FirstMvcApp.Models;

namespace FirstMvcApp.Mappers
{
    public class SourceProfile : Profile
    {
        public SourceProfile()
        {
            CreateMap<Source, RssUrlsFromSourceDto>()
                .ForMember(dto => dto.SourceId, 
                    opt => opt.MapFrom(source => source.Id))
                .ForMember(dto => dto.RssUrl, 
                    opt => opt.MapFrom(source => source.RssUrl));
        }
    }
}
