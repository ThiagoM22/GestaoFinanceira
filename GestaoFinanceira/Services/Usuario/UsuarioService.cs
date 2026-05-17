using GestaoFinanceira.DTOs.Usuario;
using GestaoFinanceira.Models;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.Usuario;
using Microsoft.AspNetCore.Identity;

namespace GestaoFinanceira.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _crudUser;
        private readonly UserManager<Models.Usuario> _userManager;
        private readonly INotificador _notificador;

        public UsuarioService(IUsuarioRepository crudUser, UserManager<Models.Usuario> userManager, INotificador notificador)
        {
             _crudUser = crudUser;
             _userManager = userManager;
             _notificador = notificador;
        }

        //Listar Usuarios
        public async Task<List<UsuarioResponseDto>> ListarUsuarios()
        {
            var usuarios = await _crudUser.GetUsers(); // Buscando usuarios no banco de dados

            if(usuarios == null || !usuarios.Any())
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Nenhum usuário encontrado"));
                return null;
            }
            // Mapeando para UsuarioResponseDto
            return usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                DataCadastro = u.DataCadastro,
                Ativo = u.Ativo
            }).ToList();
        }

        //Buscar Usuario por Id
        public async Task<UsuarioResponseDto?> BuscarUsuarioPorId(string id)
        {
            var usuario = await _crudUser.GetUserById(id); // Buscando usuario pelo id
            // Verificando se o usuario existe
            if (usuario == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Usuário não encontrado"));
                return null;
            }
            // Mapeando para UsuarioResponseDto e retornando
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };
        }

            //Criar Usuario
        public async Task<UsuarioResponseDto> ResgistrarUsuario(UsuarioCreateDto dto)
        {
            // Verifica se o dto não e nulo
            if (dto == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("DTO não pode ser nulo"));
                return null;
            }
            // Mapeando DTO para Modelo
            var usuario = new Models.Usuario
            {      
                Nome = dto.Nome,
                UserName = dto.Email, // O userName deve ser único por usuario
                Email = dto.Email,
                Ativo = true,
                DataCadastro = DateTime.UtcNow,    

                EmailConfirmed = true
            };
            var usuarioResposta = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Ativo = true,
                DataCadastro = usuario.DataCadastro
            };

            var resultado = await _userManager.CreateAsync(usuario, dto.Senha); // Criando usuario com UserManager
            // Verificando resultado da criação
            if (!resultado.Succeeded)
            {
                foreach (var erro in resultado.Errors)
                {
                    _notificador.Adicionar(new Notificacao.Notificacao(erro.Description));
                }
                return null;
            }

            return usuarioResposta;
        }

        //Desativar Usuario
        public async Task<UsuarioResponseDto?> DesativarUsuario(string id)
        {
            var usuario = await _crudUser.GetUserById(id);

            if (usuario == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Usuário não encontrado"));
                return null;
            }

            usuario.Ativo = false;
            await _crudUser.UpdateUser(usuario);
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };
        }

        public async Task<UsuarioResponseDto> AtualizarNome(string id, UsuarioUpdateNameDto dto)
        {
            //Buscando usuario pelo id
            var usuario = await _crudUser.GetUserById(id);
            //Verificando se o usuario existe
            if (usuario == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Usuário não encontrado"));
                return null;
            }

            usuario.Nome = dto.Nome;
            await _crudUser.UpdateUser(usuario);
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };

        }
        //Deletar Usuario 
        public async Task<string> DeletarUsuario(string id)
        {
            var usuarinho = await _crudUser.GetUserById(id);

            if(usuarinho == null)
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Usuario não encontrado"));
                return null;        
            }
            
            await _crudUser.DeleteUser(usuarinho);
            var resultado = "O usuario " + usuarinho.Nome + " foi apagado";

            return resultado ;          
        }

    }
}
