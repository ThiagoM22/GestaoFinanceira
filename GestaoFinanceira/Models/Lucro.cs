namespace GestaoFinanceira.Models
{
    public class Lucro
    {
        public Guid LucroId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Guid CategoriaLucroId { get; set; }
        public CategoriaLucro? CategoriaLucro { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
