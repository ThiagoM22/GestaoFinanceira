using GestaoFinanceira.DTOs.Lucro;

namespace GestaoFinanceira.Services.Lucros
{
    public interface ILucroService
    {
        Task<List<LucroResponseDto>> ListarLucros(string userId, int mes, string nomeCategoria);
        Task<LucroResponseDto?> BuscarLucroPorId(Guid lucroId, string userId);
        Task<LucroResponseDto> CriarLucro(LucroCreateDto dto, string userId);
        Task<LucroResponseDto> AtualizarLucro(Guid lucroId, LucroCreateDto dto, string userId);
        Task<string> DeletarLucro(Guid id);
    }
}
