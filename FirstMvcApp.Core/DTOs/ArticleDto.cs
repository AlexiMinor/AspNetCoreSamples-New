using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Core.DTOs;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description{ get; set; }
    public string Body { get; set; }
    public DateTime CreationDate { get; set; }
    public float PositivityRate { get; set; }

    public List<CommentDto> CommentDtos { get; set; }
}

public class CommentDto
{
    public string Text { get; set; }
    public string UserName { get; set; }
    public DateTime CreationDateTime { get; set; }

}