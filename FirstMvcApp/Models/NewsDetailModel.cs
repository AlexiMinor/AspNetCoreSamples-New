using FirstMvcApp.Core;

namespace FirstMvcApp.Models;

public class NewsDetailModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreationDate { get; set; }

    public ArticleType ArticleType { get; set; }
    public string SourceName { get; set; }
}