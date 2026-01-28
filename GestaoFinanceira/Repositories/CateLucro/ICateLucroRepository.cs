using GestaoFinanceira.Models;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.CateLucro
{
    public interface ICateLucroRepository
    {
        Task<IEnumerable<CategoriaLucro>> GetCategoriaLucros(Expression<Func<CategoriaLucro, bool>>? filtro = null);
        Task<CategoriaLucro?> GetCategoriaLucroById(Guid categoriaLucroId);
        Task<CategoriaLucro> CreateCategoriaLucro(CategoriaLucro categoriaLucro);
        Task<CategoriaLucro> UpdateCategoriaLucro(CategoriaLucro categoriaLucro);
        Task<bool> DeleteCategoriaLucro(CategoriaLucro categoriaLucro);
    }
}
