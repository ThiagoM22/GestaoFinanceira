namespace GestaoFinanceira.DTOs.Gasto
{
    public class GastoResponseDto
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; } 
    }
}
