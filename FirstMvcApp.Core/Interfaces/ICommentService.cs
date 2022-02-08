using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface ICommentService
{
    //todo add model
    Task<IEnumerable<object>> GetAllCommentsWithUsernameByArticleId(Guid articleId);
    
}