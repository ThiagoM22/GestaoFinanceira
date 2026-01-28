using GestaoFinanceira.DTOs.CategoriaLucro;

namespace GestaoFinanceira.Services.CateLucro
{
    public interface ICateLucroService
    {
        Task<List<CategoriaLucroResponseDto>> ListarCategoriasLucro(string userId);
        Task<CategoriaLucroResponseDto> BuscarCategoriaLucroPorId(Guid categoriaLucroId, string userId);
        Task<CategoriaLucroResponseDto> CriarCategoriaLucro(CategoriaLucroCreateDto dto, string userId);
        Task<CategoriaLucroResponseDto> AtualizarCategoriaLucro(CategoriaLucroCreateDto dto, Guid id);
        Task<string> DeletarCategoriaLucro(Guid id);
    }
}
