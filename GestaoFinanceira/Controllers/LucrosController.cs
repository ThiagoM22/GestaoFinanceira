using GestaoFinanceira.DTOs.Lucro;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.Lucros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LucrosController : MainController
    {
        private readonly ILucroService _service;

        public LucrosController(ILucroService service, INotificador notificador) : base(notificador)
        {
            _service = service;
        }

        //Listar Lucros
        [HttpGet]
        public async Task<ActionResult> ListarLucros([FromQuery] int? mes, [FromQuery] string? nomeCategoria)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var lucros = await _service.ListarLucros(userId.ToString(), mes, nomeCategoria);
            return CustomResponse(lucros);
        }

        //Buscar Lucro por Id
        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarLucroPorId(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var lucro = await _service.BuscarLucroPorId(id, userId.ToString());
            return CustomResponse(lucro);
        }

        //Criar novo Lucro
        [HttpPost]
        public async Task<ActionResult> CriarLucro([FromBody] LucroCreateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var lucroCriado = await _service.CriarLucro(dto, userId.ToString());
            return CustomResponse(lucroCriado);
        }

        //Atualizar Lucro
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarLucro(Guid id, [FromBody] LucroCreateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var lucroAtualizado = await _service.AtualizarLucro(id, dto, userId.ToString());
            return CustomResponse(lucroAtualizado);
        }

        //Deletar Lucro
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarLucro(Guid id)
        {
            var resultado = await _service.DeletarLucro(id);
            return CustomResponse(resultado);
        }
    }

}
