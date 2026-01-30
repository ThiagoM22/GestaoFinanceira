using GestaoFinanceira.Models;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.Gastos
{
    public interface IGastoRepository
    {
        Task<List<Gasto>> GetGastoa(Expression<Func<Gasto, bool>>? filtro = null);
        Task<Gasto?> FindGastoById(Guid gastoId);
        Task<Gasto> CreateGasto(Gasto gasto);
        Task<Gasto> UpdateGasto(Gasto gasto);
        Task<bool> DeleteGasto(Gasto gasto);
    }
}
