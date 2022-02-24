using System.Collections.Concurrent;
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
        private readonly ISourceService _sourceService;
        private readonly IArticlesService _articlesService;

        public RssService(IMapper mapper, 
            ILogger<RssService> logger, 
            ISourceService sourceService, 
            IArticlesService articlesService)
        {
            _mapper = mapper;
            _sourceService = sourceService;
            _articlesService = articlesService;
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

        public async Task<int> AggregateArticleDataFromRssSources()
        {
            var rssUrls = await _sourceService.GetRssUrlsAsync();
            var concurrentDictionary = new ConcurrentDictionary<string, RssArticleDto?>();

            var result = Parallel.ForEach(rssUrls, parallelOptions: new ParallelOptions() { }, dto =>
            {
                GetArticlesInfoFromRss(dto.RssUrl).AsParallel().ForAll(articleDto
                    => concurrentDictionary.TryAdd(articleDto.Url, articleDto));
            });

            var extArticlesUrls = await _articlesService.GetAllExistingArticleUrls();

            Parallel.ForEach(extArticlesUrls.Where(url => concurrentDictionary.ContainsKey(url)),
                s => concurrentDictionary.Remove(s, out var dto));

            var articleDtos = concurrentDictionary.Values.Select(dto => _mapper.Map<ArticleDto>(dto)).ToArray();

            return await _articlesService.InsertNews(articleDtos);

        }
    }
}