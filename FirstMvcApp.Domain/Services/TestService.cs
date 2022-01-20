using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.Domain.Services
{
    public class TestService : ITestService
    {
        private readonly NewsAggregatorContext _db;

        public async Task Do()
        {
            //var smth = await _db.Articles
            //    .Select(article => article.Title).ToListAsync();


            return;
        }
    }
}