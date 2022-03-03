using FirstMvcApp.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMvcApp.Models;

public class ArticleChangeModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public string SourceUrl { get; set; }
    public DateTime CreationDate { get; set; }
    public float PositivityRate { get; set; }

    public Guid SourceId { get; set; }
    public List<SelectListItem> Sources { get; set; }
}