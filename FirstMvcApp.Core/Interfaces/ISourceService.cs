using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface ISourceService
{
    public Task<IEnumerable<RssUrlsFromSourceDto>> GetRssUrlsAsync();
    public Task<Guid> GetSourceByUrl(string url);
    public Task<IEnumerable<SourceDropDownDto>> GetSourcesForDropdownSelect();
}