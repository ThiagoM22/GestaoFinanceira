using GestaoFinanceira.Data;
using System.Linq.Expressions;
using GestaoFinanceira.Models;

using Microsoft.EntityFrameworkCore;

namespace GestaoFinanceira.Repositories.Lucros
{
    public class LucroRepository : ILucroRepository
    {
        private readonly ApiDbContext _context;

        public LucroRepository(ApiDbContext context)
        {
            _context = context;
        }

        //Listar Lucros
      public async Task<List<Lucro>> GetLucros(Expression<Func<Lucro, bool>> ? filtro = null)
        {
            IQueryable<Lucro> query = _context.Lucros
                .Include(l => l.CategoriaLucro)
                .AsNoTracking();
            if(filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }
        //Buscar Lucro por Id
        public async Task<Lucro?> FindLucroById(Guid lucroId)
        {
            return await _context.Lucros.FindAsync(lucroId);
        }
        //Adicionar Lucro
        public async Task<Lucro> CreateLucro(Lucro lucro)
        {
            _context.Lucros.Add(lucro);
            await _context.SaveChangesAsync();
            return lucro;
        }
        //Atualizar Lucro
        public async Task<Lucro> UpdateLucro(Lucro lucro)
        {
            _context.Lucros.Update(lucro);
            await _context.SaveChangesAsync();
            return lucro;
        }
        //Deletar Lucro
        public async Task<bool> DeleteLucro(Lucro lucro)
        {
            _context.Lucros.Remove(lucro);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
