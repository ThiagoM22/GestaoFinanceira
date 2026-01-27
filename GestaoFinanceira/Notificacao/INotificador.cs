namespace GestaoFinanceira.Notificacao
{
    public interface INotificador
    {
        void Adicionar(Notificacao notificacao, params object[] dados);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacoes();
    }
}
