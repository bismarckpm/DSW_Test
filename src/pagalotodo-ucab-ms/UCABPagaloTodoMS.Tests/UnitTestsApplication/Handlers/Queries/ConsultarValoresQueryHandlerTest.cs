using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Infrastructure.Settings;
using UCABPagaloTodoMS.Tests.DataSeed;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries
{
    public class ConsultarValoresQueryHandlerTest
    {
        private readonly ConsultarValoresQueryHandler _handler;
        private readonly Mock<IUCABPagaloTodoDbContext> _contextMock;
        private readonly Mock<ILogger<ConsultarValoresQueryHandler>> _mockLogger;
        
        public ConsultarValoresQueryHandlerTest() 
        {
            var faker = new Faker();
            _contextMock = new Mock<IUCABPagaloTodoDbContext>();
            _mockLogger = new Mock<ILogger<ConsultarValoresQueryHandler>>();
            _handler = new ConsultarValoresQueryHandler(_contextMock.Object, _mockLogger.Object);
            _contextMock.SetupDbContextData();
        }

        [Fact]
        public async Task<Task> ConsultarValoresTest()
        {
                var query = new ConsultarValoresPruebaQuery();
            var valores = await _handler.Handle(query, new CancellationToken());
            Assert.NotNull(valores);
            return Task.CompletedTask;
        }
    }
}

