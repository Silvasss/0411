namespace _0411.Contracts
{
    public interface IUnitOfWork
    {
        ITipoQuartoRepository TipoQuartoRepository { get; }
        IQuartoRepository QuartoRepository { get; }
        IProdutoRepository ProdutoRepository { get; }
        IConsumoRepository ConsumoRepository { get; }
        IReservaRepository ReservaRepository { get; }
        IPagamentoRepository PagamentoRepository { get; }
        IClienteRepository ClienteRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IAutenticacaoRepository AutenticacaoRepository { get; }


        Task<int> Commit();
    }
}
