using GestaoFinanceira.DTOs.Usuario;
namespace GestaoFinanceira.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDto>> ListarUsuarios();
        Task<UsuarioResponseDto> ResgistrarUsuario(UsuarioCreateDto dto);
        Task<UsuarioResponseDto> DesativarUsuario(string id);
        Task<UsuarioResponseDto> BuscarUsuarioPorId(string id);
        Task<UsuarioResponseDto> AtualizarNome(string id, UsuarioUpdateNameDto dto);
        Task<string> DeletarUsuario(string id);
    }
}
