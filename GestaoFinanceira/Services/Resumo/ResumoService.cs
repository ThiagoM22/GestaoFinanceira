using GestaoFinanceira.Models;
using GestaoFinanceira.Repositories.CateGasto;
using GestaoFinanceira.Repositories.Gastos;
using GestaoFinanceira.Repositories.Lucros;
using GestaoFinanceira.Services.Gastos;

namespace GestaoFinanceira.Services.Resumo
{
    public class ResumoService : IResumoService
    {
        private readonly IGastoRepository _gastoRepository;
        private readonly ILucroRepository _lucroRepository;

        public ResumoService(IGastoService gastoService, IGastoRepository gastoRepository, ILucroRepository lucroRepository)
        {
            _gastoRepository = gastoRepository;
            _lucroRepository = lucroRepository;
        }

        //Calcular Resumo Mensal
        public async Task<Dictionary<string, decimal>> CalcularResumoMensal(string userId, int mes, int ano)
        {
            var gastos = await _gastoRepository.GetGastoa(g => g.UsuarioId == userId && g.Data.Month == mes && g.Data.Year == ano);
            var lucros = await _lucroRepository.GetLucros(l => l.UsuarioId == userId && l.Data.Month == mes && l.Data.Year == ano);
            decimal totalGastos = gastos.Sum(g => g.Valor);
            decimal totalLucros = lucros.Sum(l => l.Valor);
            decimal saldoMensal = totalLucros - totalGastos;
            return new Dictionary<string, decimal>
            {
                { "TotalGastos", totalGastos },
                { "TotalLucros", totalLucros },
                { "SaldoMensal", saldoMensal }
            };
        }

        //Calcular Saldo
        public async Task<Dictionary<string, decimal>> CalcularSaldo(string userId)
        {
            var gastos = await _gastoRepository.GetGastoa(g => g.UsuarioId == userId);
            var lucros = await _lucroRepository.GetLucros(l => l.UsuarioId == userId);
            decimal totalGastos = gastos.Sum(g => g.Valor);
            decimal totalLucros = lucros.Sum(g => g.Valor);
            decimal saldo = totalLucros - totalGastos;
            return new Dictionary<string, decimal>
            {
                {"TotalGastos", totalGastos  },
                {"TotalLucros", totalLucros  },
                {"Saldo", saldo }
            };
        }
    }
}
