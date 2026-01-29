using GestaoFinanceira.DTOs.CategoriaGasto;

namespace GestaoFinanceira.Services.CateGasto
{
    public interface ICateGastoService
    {
        Task<List<CategoriaGastoResponseDto>> ListarCategoriasGasto(string userId);
        Task<CategoriaGastoResponseDto> BuscarCategoriaGastoPorId(Guid categoriaGastoId, string userId);
        Task<CategoriaGastoResponseDto> CriarCategoriaGasto(CategoriaGastoCreateDto dto, string userId);
        Task<CategoriaGastoResponseDto> AtualizarCategoriaGasto(Guid categoriaGastoId, CategoriaGastoCreateDto dto, string userId);
        Task<string> DeletarCategoriaGasto(Guid categoriaGastoId, string userId);

    }
}
