using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.Gasto
{
    public class GastoCreateDto
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid Categoria { get; set; }
    }
}
