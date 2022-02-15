using AutoMapper;
using AutoMapper.QueryableExtensions;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Syndication;
using System.Xml;

namespace FirstMvcApp.Domain.Services
{
    public class RssService : IRssService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RssService> _logger;

        public RssService(IMapper mapper, ILogger<RssService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<RssArticleDto?> GetArticlesInfoFromRss(string rssUrl)
        {
            try
            {
                using (var reader = XmlReader.Create(rssUrl))
                {
                    var feed = SyndicationFeed.Load(reader);
                    List<RssArticleDto?> result = feed.Items
                        .Select(item => _mapper.Map<RssArticleDto>(item))
                        .ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
           
        }
    }
}