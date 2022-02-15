namespace FirstMvcApp.Core.Interfaces;

public interface IHtmlParserService
{
    Task<string> GetArticleContentFromUrlAsync(string url);
}