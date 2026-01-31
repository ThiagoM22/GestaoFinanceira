using GestaoFinanceira.Repositories.Resumo;

namespace GestaoFinanceira.Services.Resumo
{
    public class ResumoService : IResumoService
    {
        private readonly IResumoRepository _resumo;

        public ResumoService(IResumoRepository resumoRepository)
        {
            _resumo = resumoRepository;
        }

        //Calcular Resumo Mensal
        public async Task<Dictionary<string, decimal>> CalcularResumoMensal(string userId, int mes, int ano)
        {
            return await _resumo.CalcularResumoMensal(userId, mes, ano);
        }

        //Calcular Saldo
        public async Task <Dictionary<string, decimal>> CalcularSaldo(string userId)
        {
            return await _resumo.CalcularSaldo(userId);
        }
    }
}
