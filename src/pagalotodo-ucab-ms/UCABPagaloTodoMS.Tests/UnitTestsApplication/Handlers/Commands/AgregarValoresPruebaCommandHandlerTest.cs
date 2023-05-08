using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Handlers.Commands;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.DataSeed;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Commands
{
    public class AgregarValoresPruebaCommandHandlerTest
    {
        private readonly AgregarValorePruebaCommandHandler _handler;
        private readonly Mock<IUCABPagaloTodoDbContext> _contextMock;
        private readonly Mock<ILogger<AgregarValorePruebaCommandHandler>> _mockLogger;

        public AgregarValoresPruebaCommandHandlerTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IUCABPagaloTodoDbContext>();
            _mockLogger = new Mock<ILogger<AgregarValorePruebaCommandHandler>>();
            _handler = new AgregarValorePruebaCommandHandler(_contextMock.Object, _mockLogger.Object);
            _contextMock.SetupDbContextData();
        }

        [Fact]
        public Task AgregarValoresTest()
        {
            throw new NotImplementedException();
        }
    }
}
