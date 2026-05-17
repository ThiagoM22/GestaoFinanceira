using Microsoft.IdentityModel.Tokens;

namespace GestaoFinanceira.Middlewares
{
    public class GlobalMiddleware
    {
        private readonly RequestDelegate _next; // Delegate para o próximo middleware na pipeline
        private readonly ILogger<GlobalMiddleware> _logger; // Logger para registrar informações e erros

        public GlobalMiddleware(RequestDelegate next, ILogger<GlobalMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeTask(HttpContext context)
        {
            try
            {
                await _next(context); // Chama o próximo middleware na pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, mensagem) = exception switch
            {
                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "Acesso não autorizado."
                ),
                InvalidOperationException e => (
                    StatusCodes.Status422UnprocessableEntity,
                    e.Message
                ),
                ArgumentException e => (
                    StatusCodes.Status400BadRequest,
                    e.Message
                ),
                TimeoutException => (
                    StatusCodes.Status408RequestTimeout,
                    "A requisição demorou demais. Tente novamente."
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro interno. Tente novamente mais tarde."
                )
            };
        
            var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(new
            {
                sucesso = false,
                erro = result
            });
        }
    }
}
