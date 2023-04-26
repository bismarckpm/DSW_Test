using FluentValidation;
using UCABPagaloTodoMS.Application.Queries;

namespace BbcTravelMS.Application.Validators
{
    public class ConsultarValoresPorNombreValidator : AbstractValidator<ConsultarValoresPorNombreQuery>
    {
        public ConsultarValoresPorNombreValidator()
        {
            RuleFor(c => c.Nombre)
                .Empty().WithMessage("El nombre es requerido");
        }
    }
}
