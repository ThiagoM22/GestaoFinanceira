using System.ComponentModel.DataAnnotations;
namespace GestaoFinanceira.DTOs.Usuario
{
    public class UsuarioCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress] // Validação de formato de email
        public string Email { get; set; }
        [Required]
        [MinLength(6)] 
        public string Senha { get; set; }
    }
}
