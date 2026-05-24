using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Services.Comparacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ComparacoesController : MainController
    {
        private readonly IComparacaoService _comparacaoService;

        public ComparacoesController(IComparacaoService comparacaoService, INotificador notificador) : base(notificador) 
        {
            _comparacaoService = comparacaoService;
        }


        [HttpGet]
        public async Task<IActionResult> ObterComparacao()
        {
            var comparacaoDados = await _comparacaoService.ComparacaoResultado();
            return CustomResponse(comparacaoDados);
        }
    }
}
