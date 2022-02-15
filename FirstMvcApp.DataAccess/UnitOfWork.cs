using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsAggregatorContext _db;
        private readonly IArticleRepository _articleRepository;
        private readonly IRepository<Source> _sourceRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Role> _roleRepository;

        public UnitOfWork(NewsAggregatorContext context, 
            IArticleRepository articleRepository,
            IRepository<User> users, IRepository<Role> roles, 
            IRepository<Comment> comments, 
            IRepository<Source> sourceRepository, 
            IRepository<Comment> commentRepository, 
            IRepository<User> userRepository)
        {
            _articleRepository = articleRepository;
            _db = context;
           _sourceRepository = sourceRepository;
           _commentRepository = commentRepository;
           _userRepository = userRepository;
        }

        public IArticleRepository Articles => _articleRepository;

        public IRepository<Role> Roles => _roleRepository;
        public IRepository<User> Users => _userRepository;
        public IRepository<Source> Sources => _sourceRepository;
        public IRepository<Comment> Comments => _commentRepository;


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