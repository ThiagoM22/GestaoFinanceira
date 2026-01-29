using GestaoFinanceira.Models;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.CateGasto
{
    public interface ICateGastoRepository
    {
        Task<IEnumerable<CategoriaGasto>> GetCategoriaGasto(Expression<Func<CategoriaGasto, bool>>? filtro = null);
        Task<CategoriaGasto?> FindCategoriaGastoById(Guid id);
        Task<CategoriaGasto> CreateCategoriaGasto(CategoriaGasto categoriaGasto);
        Task<CategoriaGasto> UpdateCategoriaGasto(CategoriaGasto categoriaGasto);
        Task<bool> DeleteCategoriaGasto(CategoriaGasto categoriaGasto);
    }
}
