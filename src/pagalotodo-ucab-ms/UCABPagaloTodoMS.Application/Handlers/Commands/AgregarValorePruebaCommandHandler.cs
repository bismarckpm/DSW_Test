using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class AgregarValorePruebaCommandHandler : IRequestHandler<AgregarValorPruebaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarValorePruebaCommandHandler> _logger;

        public AgregarValorePruebaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarValorePruebaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(AgregarValorPruebaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request._request.Apellido))
                {

                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException("Valores.Apellido", "Apellido es requerido");
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(AgregarValorPruebaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}" , request);
                var entity = ValoresMapper.MapRequestEntity(request._request);
                _dbContext.Valores.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
