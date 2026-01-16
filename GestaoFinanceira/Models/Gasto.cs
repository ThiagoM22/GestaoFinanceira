namespace GestaoFinanceira.Models
{
    public class Gasto
    {
        public Guid GastoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Guid CategoriaGastoId { get; set; }
        public CategoriaGasto? CategoriaGasto { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
