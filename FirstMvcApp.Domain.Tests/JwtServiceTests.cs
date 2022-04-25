using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMvcApp.Domain.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FirstMvcApp.Domain.Tests
{
    [TestFixture]
    public class JwtServiceTests
    {
        private JwtService _jwtService;

        private Mock<ILogger<JwtService>> _loggerMock;
        private Mock<IConfiguration> _configurationMock;


        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<JwtService>>();
            _configurationMock = new Mock<IConfiguration>();
            
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:Secret")])
                .Returns("123");

            _jwtService = new JwtService(_configurationMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GenerateJwtToken_ByCorrectParameters_ReturnsCorrectData()
        {
            _loggerMock.Setup(logger => logger.LogError(It.IsAny<string>()));


        }
    }
}
