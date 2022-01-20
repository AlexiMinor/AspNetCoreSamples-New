namespace FirstMvcApp.Models;

public class ArticleTableViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public float Rate { get; set; }

}