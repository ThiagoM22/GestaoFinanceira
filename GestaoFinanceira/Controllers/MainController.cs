using GestaoFinanceira.Notificacao;
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

        protected bool TemNotificacoes()
        {
            return _notificador.TemNotificacoes();
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            try
            {
                // Verifica se há notificações de erro
                if (result is null)
                {
                    return NotFound(new
                    {
                        sucesso = true,
                        avisos = "Sem registros para exibir",
                        erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem),// Retorna as mensagens de notificação como erros
                        dados = result
                    });
                }
                // Retorna resposta de sucesso se não houver notificações
                if (result is Exception)
                {
                    return BadRequest(new
                    {
                        sucesso = false,
                        avisos = "Ocorreu um erro ao processar a solicitação.",
                    });
                }
                if (_notificador.TemNotificacoes())
                {
                    return BadRequest(new
                    {
                        sucesso = false,
                        erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)// Retorna as mensagens de notificação como erros
                    });
                }
                else
                {
                    return Ok(new
                    {
                        sucesso = true,
                        dados = result
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = new List<string> {ex.ToString() }
                });
            }
        }
        // Notificar erro com opção de log
        protected void NotificarErro(Exception exception, params object[] dados)
        {
            NotificarErro(exception, true, dados);
        }
    }
}
