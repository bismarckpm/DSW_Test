using MockQueryable.Moq;
using Moq;
using NetTopologySuite.IO;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Tests.DataSeed
{
    public static class DataSeed
    {
        public static void SetupDbContextData(this Mock<IUCABPagaloTodoDbContext> mockContext)
        {
            var valores = new List<ValoresEntity>
            {
                new ValoresEntity
                {
                    Id = new Guid("43ee7257-01be-4a50-bdf6-c9f46b70699c"),
                    Nombre = "Ana",
                    Apellido = "Perez",
                    Identificacion = "798794513",
                    SubValores = new List<SubValoresEntity>
                    {
                        new SubValoresEntity
                        {
                            Numero = 1,
                            Descripcion = "Sub Valor 1",
                            IdValores = new Guid("43ee7257-01be-4a50-bdf6-c9f46b70699c")
                        },
                        new SubValoresEntity
                        {
                            Numero = 2,
                            Descripcion = "Sub Valor 2",
                            IdValores = new Guid("43ee7257-01be-4a50-bdf6-c9f46b70699c")
                        }
                    }
                },
                new ValoresEntity
                {
                    Id = new Guid("907dd04e-6648-4168-a835-129c29ac9fb2"),
                    Nombre = "Maria",
                    Apellido = "Arteaga",
                    Identificacion = "213213344"
                },
                new ValoresEntity
                {
                    Id = new Guid("aecfd10b-2d7a-40c0-b97f-55a10e62947c"),
                    Nombre = " Carlos",
                    Apellido = "Marcano",
                    Identificacion = "15456115"
                }
            };

            var cuentas = new List<CuentasEntity>
            {
                new CuentasEntity
                {
                    cantidad = 2
                },
                new CuentasEntity
                {
                    cantidad = 3
                }
            };

            mockContext.Setup(c => c.Valores).Returns(valores.AsQueryable().BuildMockDbSet().Object);
            mockContext.Setup(c => c.Cuentas).Returns(cuentas.AsQueryable().BuildMockDbSet().Object);
        }
    }
}




















/*var quotationPassengers = quotations.SelectMany(x => x.Passengers).Concat(new List<PassengerEntity>
{ });*/
