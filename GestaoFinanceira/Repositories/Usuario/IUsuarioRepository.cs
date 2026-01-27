namespace GestaoFinanceira.Repositories.Usuario
{
    public interface IUsuarioRepository
    {
        Task<List<Models.Usuario>> GetUsers();
        Task<Models.Usuario> GetUserById(string id);
        Task<Models.Usuario> CreateUser(Models.Usuario usuario);
        Task<Models.Usuario> UpdateUser(Models.Usuario usuario);
        Task<bool> DeleteUser(Models.Usuario usuario);
    }
}
