using GestaoFinanceira.Notificacao;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinanceira.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool TemNotificacoes() => _notificador.TemNotificacoes();

        protected void NotificarErro(string mensagem) => _notificador.Adicionar(new Notificacao.Notificacao(mensagem));

        protected ActionResult CustomResponse(object? result = null)
        {

            if (TemNotificacoes())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
                });
            }

            if (result == null) 
            {
                return NotFound(new
                {
                    sucesso = false,
                    dados = new[] { "Nenhum registro encontrado" },
                    erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
                });
            }

            return Ok(new
            {
                sucesso = true,
                dados = result,
                erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }         

    }
}
