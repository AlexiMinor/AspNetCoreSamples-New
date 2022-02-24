using System.ServiceModel.Syndication;
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
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(article => article.Body));
            CreateMap<ArticleDto, Article>();

            CreateMap<ArticleDto, ArticleListItemViewModel>()
                .ForMember(dest => dest.Rate,
                    opt => opt.MapFrom(src => src.PositivityRate)); 

            CreateMap<Article, ArticleWithSourceNameDto>()
                .ForMember(dest => dest.SourceName,
                    opt => opt.MapFrom(src => src.Source.Name));

            CreateMap<SyndicationItem, RssArticleDto>()
                .ForMember(dto => dto.Url, opt => opt.MapFrom(item =>item.Id))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(item => item.Title.Text))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(item => item.Summary.Text));


            CreateMap<RssArticleDto, ArticleDto>()
                .ForMember(dto => dto.Id, opt => opt.AddTransform(guid => Guid.NewGuid()))
                .ForMember(dto => dto.Url, opt => opt.MapFrom(item => item.Url))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(item => item.Title))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(item => item.Description));
        }
    }
}
