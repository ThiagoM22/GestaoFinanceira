using System.Runtime.CompilerServices;

namespace GestaoFinanceira.DTOs.Comparacao
{
    public class ComparacaoResponseDTO
    {
        public List<UsuarioComparacao>? Usuarios {  get; set; }
    }    

    public class UsuarioComparacao
    {
        public string Nome { get; set; }
        public decimal Gasto { get; set; }
        public decimal Lucro { get; set; }
        public decimal Saldo { get; set; }        
        public string Tag { get; set; }
    }
}
