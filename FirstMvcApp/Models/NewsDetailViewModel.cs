namespace FirstMvcApp.Models;

public class NewsDetailViewModel
{
    public NewsDetailModel NewsDetailModel { get; set; }
    public List<CommentModel> Comments { get; set; }
}