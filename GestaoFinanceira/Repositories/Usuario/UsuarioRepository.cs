using GestaoFinanceira.Data;
using Microsoft.EntityFrameworkCore;


namespace GestaoFinanceira.Repositories.Usuario
{
    public class UsuarioRepository : IUsuarioRepository // Implementação do repositório de usuários
    {
        private readonly ApiDbContext _context; // Contexto do banco de dados
        // Construtor que recebe o contexto do banco de dados via injeção de dependência
        public UsuarioRepository(ApiDbContext context)  
        {
            _context = context;
        }

        // Listar Usuario
        public async Task<List<Models.Usuario>> GetUsers()
        {
            var usuarios = await _context.Users.ToListAsync(); // Busca todos os usuários do banco de dados
            return usuarios; // Retorna a lista de usuários
        }

        //Buscar Usuario por Id
        public async Task<Models.Usuario?> GetUserById(string id)
        {
            var usuario = await _context.Users.FindAsync(id); 
            return usuario; 
        }

        //Criar Usuario com base no IdentityUser
        public async Task<Models.Usuario> CreateUser(Models.Usuario usuario)
        {
            _context.Users.Add(usuario); // Adiciona o novo usuário ao contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return usuario; // Retorna o usuário criado
        }

        //Atualizar Usuario
        public async Task<Models.Usuario> UpdateUser(Models.Usuario usuario)
        {
            _context.Users.Update(usuario); 
            await _context.SaveChangesAsync(); 
            return usuario; 
        }

        //Deletar Usuario
        public async Task<bool> DeleteUser(Models.Usuario usuario)
        {
            _context.Users.Remove(usuario); 
            await _context.SaveChangesAsync(); 
            return true; 
        }
    }
}
