using Bogus;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataContextFaker
    {
        public static Faker<ValoresRequest> BuildValoresRequest()
        {
            Randomizer.Seed = new Random(100);
            return new Faker<ValoresRequest>()
                .RuleFor(cs => cs.Nombre, fk => fk.Lorem.Word())
                .RuleFor(cs => cs.Apellido, fk => fk.Lorem.Word())
                .RuleFor(cs => cs.Identificacion, fk => fk.Lorem.Word());
        }


        public static List<ValoresResponse> BuildListaValores()
        {
            var data = new List<ValoresResponse>()
            {
                new ValoresResponse()
                {
                    Nombre = "BETZY DEL CARMEN ALMANZA ADAMES",
                    Identificacion = "PN8388-1",
                    Id= new Guid("f1da2b15-922e-44ce-92bb-07b069b43dfc")
                },
                new ValoresResponse()
                {
                    Nombre = "ADNAN MAURICIO QUIEL TIJERINO",
                    Identificacion = "PP103-2019",
                    Id= new Guid("33b6653b-128d-4178-bb89-5fd77d2164b6")
                },
                new ValoresResponse()
                {
                    Nombre = "AILYN NICOLE WU MONTERREY",
                    Identificacion = "PP045-2019",
                    Id= new Guid("c2d0efa5-c36a-4ef8-8741-c16972d5db83")
                }
            };
            return data;
        }
    }
}
