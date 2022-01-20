﻿using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;

namespace FirstMvcApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsAggregatorContext _db;
        private readonly IArticleRepository _articleRepository;

        public UnitOfWork(IArticleRepository articleRepository,
            NewsAggregatorContext context)
        {
            _articleRepository = articleRepository;
            _db = context;
        }

        public IArticleRepository Articles => _articleRepository;

        public object Roles { get; }
        public object Users { get; }
        public object Sources { get; }
        public object Comments { get; }


        public async Task<int> Commit()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _articleRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}