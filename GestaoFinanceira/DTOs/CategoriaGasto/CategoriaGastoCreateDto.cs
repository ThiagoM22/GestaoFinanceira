using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.CategoriaGasto
{
    public class CategoriaGastoCreateDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
