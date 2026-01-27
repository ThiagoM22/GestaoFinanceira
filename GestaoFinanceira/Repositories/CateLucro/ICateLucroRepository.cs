using GestaoFinanceira.Models;

namespace GestaoFinanceira.Repositories.CateLucro
{
    public interface ICateLucroRepository
    {
        Task<List<CategoriaLucro>> GetCategoriaLucros();
        Task<CategoriaLucro?> GetCategoriaLucroById(Guid categoriaLucroId);
        Task<CategoriaLucro> CreateCategoriaLucro(CategoriaLucro categoriaLucro);
        Task<CategoriaLucro> UpdateCategoriaLucro(CategoriaLucro categoriaLucro);
        Task<bool> DeleteCategoriaLucro(CategoriaLucro categoriaLucro);
    }
}
