namespace GestaoFinanceira.Models
{
    public class CategoriaGasto
    {
        public Guid CategoriaGastoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; } // Navegação para o usuário associado
    }
}
