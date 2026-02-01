using System.ComponentModel.DataAnnotations;

namespace GestaoFinanceira.DTOs.CategoriaLucro
{
    public class CategoriaLucroCreateDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
