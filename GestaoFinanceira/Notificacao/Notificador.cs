namespace GestaoFinanceira.Notificacao
{
    public class Notificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes;
        public Notificador()
        {
            _notificacoes = new List<Notificacao>(); // Inicializa a lista de notificações
        }
        public void Adicionar(Notificacao notificacao, params object[] dados) // Adiciona uma nova notificação à lista
        {
            _notificacoes.Add(notificacao);
        }
        public List<Notificacao> ObterNotificacoes() // Retorna a lista de notificações
        {
            return _notificacoes;
        }
        public bool TemNotificacoes() // Verifica se há notificações na lista
        {
            return _notificacoes.Any();
        }
    }
}
