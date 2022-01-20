using AutoMapper;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace FirstMvcApp.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IArticlesService _articlesService;
        public ArticlesController(IMapper mapper, IArticlesService articlesService)
        {
            _mapper = mapper;
            _articlesService = articlesService;
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

        //[HttpGet]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var article = _articlesService.GetArticleWithAllNavigationProperties(id);

        //    if (article == null)
        //        return BadRequest("Incorrect id");

        //    var viewModel = new NewsDetailViewModel
        //    {
        //        Comments = article.Comments
        //            .Select(comment => new CommentModel()
        //            {
        //                Id = comment.Id,
        //                Text = comment.Text,
        //                CreationDate = comment.CreationDateTime,
        //                UserName = comment.User.Email
        //            })
        //            .OrderBy(model => model.CreationDate)
        //            .ToList(),
        //        NewsDetailModel = new NewsDetailModel()
        //        {
        //            Id = article.Id,
        //            SourceName = article.Source.Name,
        //            Body = article.Body,
        //            Title = article.Title,
        //            CreationDate = article.CreationDate
        //        }
        //    };

        //    return View(viewModel);
        //}

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
    }


}
