using GestaoFinanceira.Data;
using GestaoFinanceira.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.CateGasto
{
    public class CateGastoRepository : ICateGastoRepository
    {
        private readonly ApiDbContext _context;

        public CateGastoRepository(ApiDbContext context)
        {
            _context = context;
        }

        //Listar Categoria de gasto por Usuario
        public async Task<IEnumerable<CategoriaGasto>> GetCategoriaGasto(Expression<Func<CategoriaGasto, bool>>?filtro = null)
        {
            IQueryable<CategoriaGasto> query = _context.CategoriaGastos.AsNoTracking();
            if(filtro  != null)            
                query = query.Where(filtro);

            return await query.ToListAsync();
        }

        //Listar por id 
        public async Task<CategoriaGasto?> FindCategoriaGastoById(Guid id) 
        {
            return await _context.CategoriaGastos.FindAsync(id);
        }

        //Criar nova Categoria lucro
        public async Task<CategoriaGasto> CreateCategoriaGasto(CategoriaGasto categoriaGasto)
        {
            await _context.CategoriaGastos.AddAsync(categoriaGasto);
            await _context.SaveChangesAsync();
            return categoriaGasto;
        }

        //Atualizar Categoria de Gasto
        public async Task<CategoriaGasto> UpdateCategoriaGasto(CategoriaGasto categoriaGasto)
        {
            _context.CategoriaGastos.Update(categoriaGasto);
            await _context.SaveChangesAsync();
            return categoriaGasto;
        }

        //Deletar Categoria Lucro
        public async Task<bool> DeleteCategoriaGasto(CategoriaGasto categoriaGasto)
        {
            _context.CategoriaGastos.Remove(categoriaGasto);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
