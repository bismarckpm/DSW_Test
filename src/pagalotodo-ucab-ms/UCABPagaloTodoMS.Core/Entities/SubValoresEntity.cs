namespace UCABPagaloTodoMS.Core.Entities
{
    public class SubValoresEntity : BaseEntity
    {
        public string? Descripcion { get; set; }
        public int Numero { get; set; }
        public Guid IdValores { get; set; }
        public ValoresEntity? Valores { get; set; }
    }
}
