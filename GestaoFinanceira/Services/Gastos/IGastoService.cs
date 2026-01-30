using GestaoFinanceira.DTOs.Gasto;

namespace GestaoFinanceira.Services.Gastos
{
    public interface IGastoService
    {
        Task<List<GastoResponseDto>> ListarGastos(string userId, int? mes, string? nomeCategoria);
        Task<GastoResponseDto?> BuscarGastoPorId(Guid gastoId, string userId);
        Task<GastoResponseDto> CriarGasto(GastoCreateDto dto, string userId);
        Task<GastoResponseDto?> AtualizarGasto(Guid gastoId, GastoCreateDto dto, string userId);
        Task<string> DeletarGasto(Guid gastoId);
    }
}
