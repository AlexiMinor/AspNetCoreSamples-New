using AutoMapper;
using AutoMapper.QueryableExtensions;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FirstMvcApp.Domain.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITestService _testService;
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;

        public ArticlesService(ITestService testService,
            IMapper mapper, IUnitOfWork unitOfWork, 
            IConfiguration configuration)
        {
            _testService = testService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<int> UpdateNameAsync(Guid articleId, string newName)
        {
            await _unitOfWork.Articles.PatchAsync(articleId, new List<PatchModel>()
            {
                new() { PropertyName = "Title", PropertyValue = newName }
            });
            return await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ArticleDto>> GetAllNewsAsync()
        {
            //await _testService.Do();
            //todo go to db
            return await _unitOfWork.Articles.Get()
                .Select(article => _mapper.Map<ArticleDto>(article))
                .ToArrayAsync();
            //take all news from db
        }

        public async Task<IEnumerable<ArticleDto>> GetNewsByPageAsync(int page)
        {
            var pageSize = Convert.ToInt32(
                _configuration["ApplicationVariables:PageSize"]);
            return await _unitOfWork.Articles.Get()
                .OrderBy(article => article.CreationDate)
                .Skip(page* pageSize)
                .Take(pageSize)
                .Select(article => _mapper.Map<ArticleDto>(article))
                .ToArrayAsync();
        }

        public async Task<int> InsertNews(IEnumerable<ArticleDto> articles)
        {
            var entities = articles.Select(dto => _mapper.Map<Article>(dto)).ToArray();
            await _unitOfWork.Articles.AddRange(entities);
            return await _unitOfWork.Commit();
        }

        public async Task<ArticleDto> GetArticleWithAllNavigationProperties(Guid id)
        {
            var article = await _unitOfWork.Articles.GetByIdWithIncludes(id,
                article => article.Source,
                article => article.Comments);
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<ArticleDto> GetArticleAsync(Guid id)
        {
            var article = await _unitOfWork.Articles.GetById(id);
            return _mapper.Map<ArticleDto>(article);
        }


        public async Task<ArticleDto> GetArticleWithCommentsAndUsernames(Guid id)
        {
            var article = await _unitOfWork.Articles.GetById(id);

            var articleWithData = await _unitOfWork.Articles.Get()
                .Where(a => a.Id.Equals(id)) // better way 
                .Include(a => a.Comments)
                .ThenInclude(comment => comment.User)
                .FirstOrDefaultAsync(/*a => a.Id.Equals(id)*/);


            var comments = await _commentService.GetAllCommentsWithUsernameByArticleId(id);
            
            return _mapper.Map<ArticleDto>(article);
        }

     
        public async Task<int?> DeleteAsync(Guid modelId)
        {
            try
            {
                if (await _unitOfWork.Articles.GetById(modelId) != null)
                {
                    await _unitOfWork.Articles.Remove(modelId);
                    return await _unitOfWork.Commit();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                //todo add log
                throw;
            }
        }

        public async Task<IEnumerable<ArticleWithSourceNameDto>> GetNewsWithSourceNameBySourceId(Guid sourceId)
        {
            var articles = await _unitOfWork.Articles.FindBy(article => article.SourceId.Equals(sourceId),
                article => article.Source);
            var result = await articles
                .Select(article => _mapper.Map<ArticleWithSourceNameDto>(article))
                .ToArrayAsync();
            return result;
        }

        public async Task<List<string>> GetAllExistingArticleUrls()
        {
            return await _unitOfWork.Articles.Get().Select(article => article.SourceUrl).ToListAsync();
        }
    }
}