namespace FirstMvcApp.Data.Entities;

public class Source : BaseEntity
{
    public string Name { get; set; }
    public string BaseUrl { get; set; }
    public string RssUrl { get; set; }
}