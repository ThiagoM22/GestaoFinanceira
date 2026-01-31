namespace GestaoFinanceira.Services.Resumo
{
    public interface IResumoService
    {
        Task<Dictionary<string, decimal>> CalcularResumoMensal(string userId, int mes, int ano);
        Task<Dictionary<string, decimal>> CalcularSaldo(string userId);
    }
}
