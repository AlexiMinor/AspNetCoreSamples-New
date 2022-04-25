using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreSamples.CQS.Models.Queries;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Domain.Services;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace FirstMvcApp.Domain.Tests
{
    [TestFixture]
    public class ArticleCqsServiceTests
    {
        private ArticleCqsService _articleCqsService;
        private Mock<IMediator> _mediator;
        
        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _articleCqsService = new ArticleCqsService(_mediator.Object);
        }

        [Test]
        [TestCase(0)]
        [TestCase(50)]
        public async Task GetNewsByPageAsync_NonNegativePageNumber_CorrectlyReturnedData(int pageNumber)
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetArticlesByPageQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new[]
                {
                    new ArticleDto(), 
                    new ArticleDto(), 
                    new ArticleDto()
                });

            var articles = await _articleCqsService.GetNewsByPageAsync(pageNumber);
            if (pageNumber >=0)
            {
                Assert.AreEqual(articles.Count(), 3);
            }
        }

        [TestCase(-100)]
        public async Task GetNewsByPageAsync_NegativePageNumber_ThrowArgumentException(int pageNumber)
        {
          var result = Assert.ThrowsAsync<ArgumentException>(() => _articleCqsService.GetNewsByPageAsync(pageNumber));

           Assert.AreEqual("Page number under 0 (Parameter 'page')", result.Message);
        }

    }
}