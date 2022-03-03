namespace FirstMvcApp.Models;

public class ArticleIndexViewModel
{
    List<ArticleListItemViewModel> ArticleList { get; set; }

    private bool IsAdmin { get; set; }
}