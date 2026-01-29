namespace GestaoFinanceira.DTOs.Lucro
{
    public class LucroResponseDto
    {
        public Guid Id { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime? Data { get; set; }
    }
}
