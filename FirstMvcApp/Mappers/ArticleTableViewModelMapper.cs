using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data;
using FirstMvcApp.Models;

namespace FirstMvcApp.Mappers
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDto, ArticleTableViewModel>();
            CreateMap<Article, ArticleTableViewModel>();
            CreateMap<Article, ArticleDto>();

            CreateMap<ArticleDto, ArticleListItemViewModel>()
                .ForMember(dest => dest.Rate,
                    opt => opt.MapFrom(src => src.PositivityRate)); 

            CreateMap<Article, ArticleWithSourceNameDto>()
                .ForMember(dest => dest.SourceName,
                    opt => opt.MapFrom(src => src.Source.Name));
        }
    }
}
