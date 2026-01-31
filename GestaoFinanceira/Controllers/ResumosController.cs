using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.Resumo;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResumosController : MainController
    {
       private readonly IResumoService _service;

        public ResumosController(IResumoService service, INotificador notificador) : base(notificador)
        {
            _service = service;
        }
        //Calcular Resumo Mensal
        [HttpGet("mensal/{mes}/{ano}")]
        public async Task<ActionResult> CalcularResumoMensal(int mes, int ano)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var resumoMensal = await _service.CalcularResumoMensal(userId, mes, ano);
            return CustomResponse(resumoMensal);
        }
        //Calcular Saldo
        [HttpGet("saldo")]
        public async Task<ActionResult> CalcularSaldo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var saldo = await _service.CalcularSaldo(userId);
            return CustomResponse(saldo);
        }
    }
}
