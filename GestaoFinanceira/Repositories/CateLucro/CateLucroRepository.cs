using GestaoFinanceira.Data;
using GestaoFinanceira.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoFinanceira.Repositories.CateLucro
{
    public class CateLucroRepository : ICateLucroRepository
    {
        private readonly ApiDbContext _context;

        public CateLucroRepository(ApiDbContext context)
        {
            _context = context;
        }

        //Listar Categorias de Lucro com filtro opcional
        public async Task<IEnumerable<CategoriaLucro>> GetCategoriaLucros(Expression<Func<CategoriaLucro, bool>>? filtro = null) 
        {
            IQueryable<CategoriaLucro> query = _context.CategoriaLucros.AsNoTracking();// Evita o rastreamento de alterações para melhor desempenho
            if (filtro != null) // Aplica o filtro se fornecido
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync(); // Executa a consulta e retorna os resultados como uma lista
        }

        //Buscar Categoria de Lucro por Id
        public async Task<CategoriaLucro?> GetCategoriaLucroById(Guid categoriaLucroId)
        {
            return await _context.CategoriaLucros.FindAsync(categoriaLucroId);
        }

        //Criando nova Categoria de Lucro
        public async Task<CategoriaLucro> CreateCategoriaLucro(CategoriaLucro categoriaLucro)
        {
            _context.CategoriaLucros.Add(categoriaLucro);
            await _context.SaveChangesAsync();
            return categoriaLucro;
        }

        //Atualizando Categoria de Lucro
        public async Task<CategoriaLucro> UpdateCategoriaLucro(CategoriaLucro categoriaLucro)
        {
            _context.CategoriaLucros.Update(categoriaLucro);
            await _context.SaveChangesAsync();
            return categoriaLucro;
        }

        //Deletando Categoria de Lucro
        public async Task<bool> DeleteCategoriaLucro(CategoriaLucro categoriaLucro)
        {
            _context.CategoriaLucros.Remove(categoriaLucro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
