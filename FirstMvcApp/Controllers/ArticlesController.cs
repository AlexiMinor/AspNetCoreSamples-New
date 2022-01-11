using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Data;
using FirstMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace FirstMvcApp.Controllers
{
    public class ArticlesController : Controller
    {
        //private readonly INewsService _newsService;
        private readonly NewsAggregatorContext _db;
        public ArticlesController(NewsAggregatorContext db)
        {
            _db = db;
            //_newsService = newsService;`
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _db.Articles
                .Select(article => new ArticleListItemViewModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    CreationDate = article.CreationDate,
                    Description = article.Description
                })
                .OrderByDescending(article => article.CreationDate).ToListAsync();

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var article = await _db.Articles
                .Include(article => article.Source)
                .Include(article => article.Comments)
                .ThenInclude(comment => comment.User)
                .FirstOrDefaultAsync(article => article.Id.Equals(id));

            if (article == null)
                return BadRequest("Incorrect id");

            var viewModel = new NewsDetailViewModel
            {
                Comments = article.Comments
                    .Select(comment => new CommentModel()
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        CreationDate = comment.CreationDateTime,
                        UserName = comment.User.Email
                    })
                    .OrderBy(model => model.CreationDate)
                    .ToList(),
                NewsDetailModel = new NewsDetailModel()
                {
                    Id = article.Id,
                    SourceName = article.Source.Name,
                    Body = article.Body,
                    Title = article.Title,
                    CreationDate = article.CreationDate
                }
            };

            return View(viewModel);
        }





        [HttpGet]
        public IActionResult Page()
        {

            return View();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await _db.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(article 
                    => article.Id.Equals(id));

            if (article != null)
            {
                var model = new NewsDetailModel()
                {
                    Id = id,
                    Body = article.Body,
                    Title = article.Title,
                    CreationDate = article.CreationDate
                };
                return View(model);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsDetailModel model)
        {
            

            return BadRequest();
        }
    }
}
