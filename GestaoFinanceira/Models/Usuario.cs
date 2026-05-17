using Microsoft.AspNetCore.Identity;

namespace GestaoFinanceira.Models
{
    public class Usuario : IdentityUser // Herda os atributos do IdentityUser para incluir funcionalidades de autenticação e autorização
    {
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public ICollection<CategoriaGasto>? CategoriaGastos { get; set; }
        public ICollection<CategoriaLucro>? CategoriaLucros { get; set; }

        public ICollection<Lucro>? Lucros { get; set; }
        public ICollection<Gasto>? Gastos { get; set; }
    }
}
