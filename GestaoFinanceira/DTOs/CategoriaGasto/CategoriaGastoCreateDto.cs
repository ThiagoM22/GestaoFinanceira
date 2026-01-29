using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.CategoriaGasto
{
    public class CategoriaGastoCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
    }
}
