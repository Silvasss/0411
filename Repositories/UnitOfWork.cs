using _0411.Contracts;
using _0411.Data;

namespace _0411.Repositories
{
    public class UnitOfWork(DataContext dataContext, IConfiguration config) : IUnitOfWork
    {
        protected readonly DataContext _entityFramework = dataContext;
        private readonly IConfiguration _config = config;


        private ITipoQuartoRepository? _tipoQuartoRepository;
        public ITipoQuartoRepository TipoQuartoRepository
        {
            get
            {
                return _tipoQuartoRepository ?? new TipoQuartoRepository(_entityFramework);
            }
        }

        private IQuartoRepository? _quartoRepository;
        public IQuartoRepository QuartoRepository
        {
            get { return _quartoRepository ?? new QuartoRepository(_entityFramework); }
        }

        private IProdutoRepository? _produtoRepository;
        public IProdutoRepository ProdutoRepository
        {
            get { return _produtoRepository ?? new ProdutoRepository(_entityFramework); }
        }

        private IConsumoRepository? _consumoRepository;
        public IConsumoRepository ConsumoRepository
        {
            get { return _consumoRepository ?? new ConsumoRepository(_entityFramework); }
        }

        private IReservaRepository? _reservaRepository;
        public IReservaRepository ReservaRepository
        {
            get { return _reservaRepository ?? new ReservaRepository(_entityFramework); }
        }

        private IPagamentoRepository? _pagamentoRepository;
        public IPagamentoRepository PagamentoRepository
        {
            get { return _pagamentoRepository ?? new PagamentoRepository(_entityFramework); }
        }

        private IClienteRepository? _clienteRepository;
        public IClienteRepository ClienteRepository
        {
            get { return _clienteRepository ?? new ClienteRepository(_entityFramework); }
        }

        private IUsuarioRepository? _usuarioRepository;
        public IUsuarioRepository UsuarioRepository
        {
            get { return _usuarioRepository ?? new UsuarioRepository(_entityFramework); }
        }

        public IAutenticacaoRepository _autenticacaoRepository;
        public IAutenticacaoRepository AutenticacaoRepository
        {
            get { return _autenticacaoRepository ?? new AutenticacaoRepository(_entityFramework, _config); }
        }

        public async Task<int> Commit()
        {
            return await _entityFramework.SaveChangesAsync();
        }

        public void Dispose()
        {
            _entityFramework.Dispose();
        }
    }
}
