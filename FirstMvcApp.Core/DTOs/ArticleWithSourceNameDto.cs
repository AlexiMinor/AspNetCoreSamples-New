namespace FirstMvcApp.Core.DTOs;

public class ArticleWithSourceNameDto
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public string Description{ get; set; }
    public string Body { get; set; }
    public string SourceName { get; set; }
    public DateTime CreationDate { get; set; }
    public float PositivityRate { get; set; }
}