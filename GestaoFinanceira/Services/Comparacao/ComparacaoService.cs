using GestaoFinanceira.DTOs.Comparacao;
using GestaoFinanceira.Notificacao;
using GestaoFinanceira.Repositories.Usuario;
using GestaoFinanceira.Services.Resumo;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestaoFinanceira.Services.Comparacao
{
    public class ComparacaoService : IComparacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IResumoService _resumoService;
        private readonly INotificador _notificador;

        public ComparacaoService(IUsuarioRepository usuarioRepository, IResumoService resumoService, INotificador notificador)
        {
            _usuarioRepository = usuarioRepository;
            _resumoService = resumoService;
            _notificador = notificador;
        }

        public async Task<ComparacaoResponseDTO?> ComparacaoResultado()
        {
            var usuarios = await _usuarioRepository.GetUsers();

            if (usuarios == null || !usuarios.Any())
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Nenhum usuário encontrado"));
                return null;
            }

            var data = DateTime.UtcNow.AddHours(-3);

            var listaUsuariosComparados = new List<UsuarioComparacao>();


            foreach (var usuario in usuarios)
            {
                var usuarioDados = await _resumoService.CalcularResumoMensal(usuario.Id, data.Month, data.Year);

                if (usuarioDados == null) continue;

                listaUsuariosComparados.Add(new UsuarioComparacao
                {
                    Nome = usuario.Nome,
                    Gasto = usuarioDados["TotalGastos"],
                    Lucro = usuarioDados["TotalLucros"],
                    Saldo = usuarioDados["SaldoMensal"]
                });
            }

            // Se a lista estiver vazia, barramos aqui
            if (!listaUsuariosComparados.Any())
            {
                _notificador.Adicionar(new Notificacao.Notificacao("Não há dados financeiros suficientes para comparação."));
                return null;
            }

            var resposta = new ComparacaoResponseDTO
            {
                Usuarios = listaUsuariosComparados,
            };

            var muquirana = listaUsuariosComparados.MinBy(u => u!.Gasto);
            var maiorLucro = listaUsuariosComparados.MaxBy(u => u!.Lucro);

            var menorSaldo = listaUsuariosComparados.Min(u => u.Saldo);
            foreach (var user in listaUsuariosComparados.Where(u => u.Saldo == menorSaldo))
                user.Tag = "Fodido";
            
            var maiorSaldo = listaUsuariosComparados.Max(u => u.Saldo);
            foreach (var user in listaUsuariosComparados.Where(u => u.Saldo == maiorSaldo))
                user.Tag = "Rico";

            if (muquirana != null)
                muquirana.Tag = "Muquirana";

            if (maiorLucro != null)
                maiorLucro.Tag = "Ceo";

            return resposta;
        }
    }
}
