using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

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

        public IRepository<Role> Roles { get; }
        public IRepository<User> Users { get; }
        public IRepository<Source> Sources { get; }
        public IRepository<Comment> Comments { get; }


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