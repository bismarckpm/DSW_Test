using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UCABPagaloTodoMS.Application.Queries;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class EjemploControllerTest
    {
        private readonly EjemploController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<EjemploController>> _loggerMock;

        public EjemploControllerTest()
        {
            _loggerMock = new Mock<ILogger<EjemploController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new EjemploController(_loggerMock.Object, _mediatorMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact]
        public async Task ConsultarValoresTest()
        {
            var valores = BuildDataContextFaker.BuildListaValores();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsultarValoresPruebaQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(valores));

            var result = await _controller.ConsultaValores();
            var response = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, response.StatusCode);
            _mediatorMock.Verify();

        }

        [Fact]
        public async Task ConsultarValoresFalla()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsultarValoresPruebaQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            var result = await _controller.ConsultaValores();
            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, response.StatusCode);
            _mediatorMock.Verify();
        }

    }
}
