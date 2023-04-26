using MediatR;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class ConsultarValoresPorNombreQuery : IRequest<List<ValoresResponse>>
    {
        public string? Nombre { get; set; }

        public ConsultarValoresPorNombreQuery(string? nombre)
        {
            Nombre = nombre;
        }
    }
}
