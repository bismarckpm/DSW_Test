using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using BbcTravelMS.Application.Validators;
using FluentValidation;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarValoresPorNombreQueryHandler : IRequestHandler<ConsultarValoresPorNombreQuery, List<ValoresResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarValoresPorNombreQueryHandler> _logger;

        public ConsultarValoresPorNombreQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarValoresPorNombreQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ValoresResponse>> Handle(ConsultarValoresPorNombreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Nombre))
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request vacio o nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<ValoresResponse>> HandleAsync(ConsultarValoresPorNombreQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.HandleAsync");
                await ValidarParametros(request);
                var result = _dbContext.Valores.Where(c => c.Nombre == request.Nombre).Select(c => new ValoresResponse()
                {
                    Id = c.Id,
                    Nombre = c.Nombre + " " + c.Apellido,
                    Identificacion = c.Identificacion,
                });
                await _dbContext.SaveEfContextChanges("App");

                if (result.Count() == 0)
                    throw new Exception("No existen registros con Nombre: " + request.Nombre);

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }

        private async Task ValidarParametros(ConsultarValoresPorNombreQuery request)
        {
            _logger.LogInformation("ConsultarValoresQueryHandler.ValidarParametros: Request {Request}", request);

            var validator = new ConsultarValoresPorNombreValidator();
            var result = await validator.ValidateAsync(request, new CancellationToken());
            if (!result.IsValid)
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.ValidarParametros: Ha ocurrido un error al validar los parámetros.");

                throw new Exception("Parámetros inválidos");
            }

            _logger.LogInformation("ConsultarValoresQueryHandler.ValidarParametros: Result {Result}", result);

        }
    }
}