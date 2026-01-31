namespace GestaoFinanceira.Repositories.Resumo
{
    public interface IResumoRepository
    {
        Task<Dictionary<string, decimal>> CalcularResumoMensal(string userId, int mes, int ano);
        Task<Dictionary<string, decimal>> CalcularSaldo(string userId);
    }
}
