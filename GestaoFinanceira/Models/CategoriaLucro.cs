namespace GestaoFinanceira.Models
{
    public class CategoriaLucro
    {
        public Guid CategoriaLucroId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
