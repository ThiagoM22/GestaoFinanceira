using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.CategoriaLucro
{
    public class CategoriaLucroCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
    }
}
