using GestaoFinanceira.DTOs.Gasto;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.Gastos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GastosController : MainController
    {
        private readonly IGastoService _service;

        public GastosController(IGastoService service, INotificador notificador) : base(notificador)
        {
            _service = service;
        }

        //Listar Gastos
        [HttpGet]
        public async Task<ActionResult> ListarGastos([FromQuery] int? mes, [FromQuery] string? nomeCategoria)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var gastos = await _service.ListarGastos(userId.ToString(), mes, nomeCategoria);
            return CustomResponse(gastos);
        }

        //Buscar Gasto por Id
        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarGastoPorId(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var gasto = await _service.BuscarGastoPorId(id, userId.ToString());
            return CustomResponse(gasto);
        }

        //Criar novo Gasto
        [HttpPost]
        public async Task<ActionResult> CriarGasto([FromBody] GastoCreateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var gastoCriado = await _service.CriarGasto(dto, userId.ToString());
            return CustomResponse(gastoCriado);
        }

        //Atualizar Gasto
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarGasto(Guid id, [FromBody] GastoCreateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var gastoAtualizado = await _service.AtualizarGasto(id, dto, userId.ToString());
            return CustomResponse(gastoAtualizado);
        }

        //Deletar Gasto
        [HttpDelete]
        public async Task<ActionResult> DeletarGasto(Guid id)
        {
            var mensagem = await _service.DeletarGasto(id);
            return CustomResponse(mensagem);
        }

    }
}
