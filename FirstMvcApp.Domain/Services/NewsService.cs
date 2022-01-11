using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;

namespace FirstMvcApp.Domain.Services
{
    public class NewsService : INewsService
    {

        private readonly ITestService _testService;

        public NewsService(ITestService testService)
        {
            _testService = testService;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            await _testService.Do();
            //todo go to db
            //take all news from db

            return null;
        }
    }
}