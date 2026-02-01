using Microsoft.AspNetCore.Mvc;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.Usuario;
using GestaoFinanceira.DTOs.Usuario;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : MainController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService, INotificador notificador) : base(notificador)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDto>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuarios();
            return CustomResponse(usuarios);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuarioPorId(string id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorId(id);
            return CustomResponse(usuario);
        }
        [Authorize]
        //GET: api/Usuarios/me
        [HttpGet("me")]
        public async Task<ActionResult<UsuarioResponseDto>> GetMe()
        {
            var logadinhoId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var logadinho = await _usuarioService.BuscarUsuarioPorId(logadinhoId);
            return CustomResponse(logadinho);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> PostUsuario([FromBody] UsuarioCreateDto dto)
        {
            var usuarioCriado = await _usuarioService.ResgistrarUsuario(dto);
            return CustomResponse(usuarioCriado);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> PutUsuario(string id, [FromBody] UsuarioUpdateNameDto nome)
        {
            var usuarioAtualizado = await _usuarioService.AtualizarNome(id, nome);
            return CustomResponse(usuarioAtualizado);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut ("Desativar/{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> DesativarUsuario(string id)
        {
            var usuarioDeletado = await _usuarioService.DesativarUsuario(id);
            return CustomResponse(usuarioDeletado);
        }

        // DELETE: api/Usuario/{id}
        [HttpDelete ("{id}")]
        public async Task<ActionResult<string>> DeletarUsuario(string id)
        {
            var usuarioApagado = await _usuarioService.DeletarUsuario(id);
            return CustomResponse(usuarioApagado);
        }


    }
}
