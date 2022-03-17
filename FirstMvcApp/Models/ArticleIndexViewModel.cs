namespace FirstMvcApp.Models;

public class ArticleIndexViewModel
{
    public List<ArticleListItemViewModel> ArticleList { get; set; }

    public int PagesAmount { get; set; }
}