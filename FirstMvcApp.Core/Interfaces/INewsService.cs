using FirstMvcApp.Core.DTOs;

namespace FirstMvcApp.Core.Interfaces;

public interface INewsService
{
    //todo add model
    public Task<IEnumerable<NewsDto>> GetAllNewsAsync();
}