using GestaoFinanceira.Data;
using GestaoFinanceira.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.Gastos
{
    public class GastoRepository : IGastoRepository
    {
        private readonly ApiDbContext _context;

        public GastoRepository(ApiDbContext context)
        {
            _context = context;
        }

        //Listar Gastos
        public async Task<IEnumerable<Gasto>> GetGastoa(Expression<Func<Gasto, bool>> ? filtro = null) 
        {
            IQueryable<Gasto> query = _context.Gastos
                .Include(l => l.CategoriaGasto)
                .AsNoTracking();
            if (filtro != null) 
            {
              query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        //Buscar Gasto por Id
        public async Task<Gasto?> FindGastoById(Guid gastoId)
        {
            return await _context.Gastos.FindAsync(gastoId);
        }

        //Adicionar Gasto
        public async Task<Gasto> CreateGasto(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();
            return gasto;
        }

        //Atualizar Gasto
        public async Task<Gasto> UpdateGasto(Gasto gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
            return gasto;
        }

        //Deletar Gasto
        public async Task<bool> DeleteGasto(Gasto gasto)
        {
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
