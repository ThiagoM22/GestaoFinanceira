using GestaoFinanceira.DTOs.Comparacao;

namespace GestaoFinanceira.Services.Comparacao
{
    public interface IComparacaoService
    {
        Task<ComparacaoResponseDTO?> ComparacaoResultado();
    }
}
