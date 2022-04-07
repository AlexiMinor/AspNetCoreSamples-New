namespace WebApiFirstAppSample.Models.Requests;

public class GetArticleRequest
{
    public string Name { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }

    public ArticlesSortType SortType { get; set; }
}