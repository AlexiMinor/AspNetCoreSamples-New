using AutoMapper;
using AutoMapper.QueryableExtensions;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.Domain.Services
{
    public class CommentsService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ITestService _testService;
        private readonly IUnitOfWork _unitOfWork;

        public CommentsService(ITestService testService,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _testService = testService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<object>> GetAllCommentsWithUsernameByArticleId(Guid articleId)
        {
            return await _unitOfWork.Comments.FindBy(comment => comment.ArticleId.Equals(articleId),
                comment => comment.User.Email);

            //todo map
        }
    }
}