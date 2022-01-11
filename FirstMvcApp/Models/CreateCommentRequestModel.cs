namespace FirstMvcApp.Controllers;

public class CreateCommentRequestModel
{
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public Guid ArticleId { get; set; }
}