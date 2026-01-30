using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.Gasto
{
    public class GastoCreateDto
    {
        [Required]
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public Guid Categoria { get; set; }
    }
}
