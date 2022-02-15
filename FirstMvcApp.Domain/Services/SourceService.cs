using AutoMapper;
using AutoMapper.QueryableExtensions;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FirstMvcApp.Domain.Services
{
    public class SourceService : ISourceService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SourceService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SourceService(IMapper mapper, IUnitOfWork unitOfWork, 
            ILogger<SourceService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IEnumerable<RssUrlsFromSourceDto>> GetRssUrlsAsync()
        {
            try
            {
                var result = await _unitOfWork.Sources.Get()
                    .Select(source => _mapper.Map<RssUrlsFromSourceDto>(source))
                    .ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return null;
            }
        }

        public async Task<Guid> GetSourceByUrl(string url)
        {
            var domain = string.Join(".", 
                new Uri(url).Host
                    .Split('.')
                    .TakeLast(2)
                    .ToList());
            return (await _unitOfWork.Sources.Get()
                       .FirstOrDefaultAsync(source => source.BaseUrl.Equals(domain)))?.Id 
                   ?? Guid.Empty;

        }
    }
}