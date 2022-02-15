using System.Collections.Concurrent;
using System.Diagnostics;
using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace FirstMvcApp.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticlesService _articlesService;
        private readonly ISourceService _sourceService;
        private readonly IRssService _rssService;
        private readonly IHtmlParserService _htmlParserService;

        public ArticlesController(IMapper mapper, 
            IArticlesService articlesService, 
            ILogger<ArticlesController> logger, IConfiguration configuration,
            ISourceService sourceService, IRssService rssService, IHtmlParserService htmlParserService)
        {
            _mapper = mapper;
            _articlesService = articlesService;
            _logger = logger;
            _configuration = configuration;
            _sourceService = sourceService;
            _rssService = rssService;
            _htmlParserService = htmlParserService;
            //_newsService = newsService;`
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = (await _articlesService.GetAllNewsAsync())
                .Select(article => _mapper.Map<ArticleListItemViewModel>(article))
                .OrderByDescending(article => article.CreationDate).ToList();

            return View(articles);
        }

        //[HttpGet]
        //public async Task<IActionResult> TopRated()
        //{
        //    var articles = await getArticles
        //        .Select(article => new ArticleListItemViewModel()
        //        {
        //            Id = article.Id,
        //            Title = article.Title,
        //            CreationDate = article.CreationDate,
        //            Description = article.Description,
        //            Rate = article.PositivityRate
        //        })
        //        .OrderByDescending(article => article.Rate).ToListAsync();

        //    return View(articles);
        //}

        [HttpGet]
        public async Task<IActionResult> Table()
        {
            var articles =
                (await _articlesService.GetAllNewsAsync())
                .Select(a => _mapper.Map<ArticleTableViewModel>(a))
                .ToArray();


            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var article = await _articlesService.GetArticleWithAllNavigationProperties(id);

            if (article == null)
                return BadRequest("Incorrect id");

            var viewModel = new NewsDetailViewModel
            {
                //Comments = article.Comments
                //    .Select(comment => new CommentModel()
                //    {
                //        Id = comment.Id,
                //        Text = comment.Text,
                //        CreationDate = comment.CreationDateTime,
                //        UserName = comment.User.Email
                //    })
                //    .OrderBy(model => model.CreationDate)
                //    .ToList(),
                NewsDetailModel = new NewsDetailModel()
                {
                    Id = article.Id,
                    //SourceName = article.Source.Name,
                    Body = article.Body,
                    Title = article.Title,
                    CreationDate = article.CreationDate
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newsDetailModel = new NewsDetailModel();
            return View(newsDetailModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(NewsDetailModel model)
        //{
        //    //is sourse is correct
        //    if (!string.IsNullOrEmpty(model.SourceName))
        //    {
        //        var sourse = await _db.Sources.FirstOrDefaultAsync(source => source.Name.Equals(model.SourceName));
        //        if (sourse != null)
        //        {
        //            //check fields for null
        //            var entity = new Article()
        //            {
        //                Id = Guid.NewGuid(),
        //                Body = model.Body,
        //                Description = model.Body,
        //                CreationDate = DateTime.Now,
        //                Title = model.Title,
        //                SourceId = sourse.Id
        //            };
        //        }
        //    }


        //    //todo insert to db
        //    return BadRequest();
        //}
        //[HttpGet]
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var article = await _db.Articles
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(article 
        //            => article.Id.Equals(id));

        //    if (article != null)
        //    {
        //        var model = new NewsDetailModel()
        //        {
        //            Id = id,
        //            Body = article.Body,
        //            Title = article.Title,
        //            CreationDate = article.CreationDate
        //        };
        //        return View(model);
        //    }

        //    return BadRequest();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(NewsDetailModel model)
        //{


        //    return BadRequest();
        //}

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var deleteModel = new DeleteModel(){Id = id};
            return View(deleteModel);
        }

        [HttpPost]
        public IActionResult Delete(DeleteModel model)
        {

            _articlesService.DeleteAsync(model.Id);
            return RedirectToAction("Table");
        }

        [HttpPost, ActionName("DeleteExample")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteExamplePost(DeleteModel model)
        {
            var deleteResult = await _articlesService.DeleteAsync(model.Id);

            if (deleteResult == null)
                return BadRequest();


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GetNewsFromSources()
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();
                var rssUrls = await _sourceService.GetRssUrlsAsync();
                var concurrentDictionary = new ConcurrentDictionary<string, RssArticleDto?>();

                var result = Parallel.ForEach(rssUrls,parallelOptions:new ParallelOptions(){}, dto =>
                {
                    _rssService.GetArticlesInfoFromRss(dto.RssUrl).AsParallel().ForAll(articleDto 
                        => concurrentDictionary.TryAdd(articleDto.Url, articleDto));
                });

                var extArticlesUrls = await _articlesService.GetAllExistingArticleUrls();

                Parallel.ForEach(extArticlesUrls.Where(url => concurrentDictionary.ContainsKey(url)),
                    s => concurrentDictionary.Remove(s, out var dto));

                //var groupedRssArticles = concurrentDictionary.GroupBy(pair => _sourceService.GetSourceByUrl(pair.Key).Result);

                //Parallel.ForEach(groupedRssArticles,)


                foreach (var rssArticleDto in concurrentDictionary)
                {
                    var body = await _htmlParserService.GetArticleContentFromUrlAsync(rssArticleDto.Key);
                }
           

                sw.Stop();
                return Ok();
            }
            catch (Exception ex)
            {
                var exMessage =
                    string.Format(_configuration.GetSection("ApplicationVariables")["LogErrorMessageFormat"],
                        ex.Message,
                        ex.StackTrace);
                _logger.LogError(ex, exMessage);
                return StatusCode(500, new { ex.Message});
            }
        }
    }


}
