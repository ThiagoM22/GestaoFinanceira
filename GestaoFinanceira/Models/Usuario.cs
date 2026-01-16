using Microsoft.AspNetCore.Identity;

namespace GestaoFinanceira.Models
{
    public class Usuario : IdentityUser // Herda os atributos do IdentityUser para incluir funcionalidades de autenticação e autorização
    {
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
