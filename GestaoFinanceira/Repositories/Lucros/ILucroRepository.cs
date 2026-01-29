using GestaoFinanceira.Models;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.Lucros
{
    public interface ILucroRepository
    {
        Task<List<Lucro>> GetLucros(Expression<Func<Lucro, bool>>? filtro = null);
        Task<Lucro?> FindLucroById(Guid lucroId);
        Task<Lucro> CreateLucro(Lucro lucro);
        Task<Lucro> UpdateLucro(Lucro lucro);
        Task<bool> DeleteLucro(Lucro lucro);
    }
}
