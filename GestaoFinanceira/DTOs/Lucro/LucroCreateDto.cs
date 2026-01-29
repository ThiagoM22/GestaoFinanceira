using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.Lucro
{
    public class LucroCreateDto
    {
        [Required]
        public decimal Valor {  get; set; }
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public Guid Categoria { get; set; }

    }
}
