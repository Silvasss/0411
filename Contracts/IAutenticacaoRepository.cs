using _0411.Dtos;
using _0411.Model;

namespace _0411.Contracts
{
    public interface IAutenticacaoRepository : IGenericoRepository<Auth>
    {
        Task<bool> SetPasswordAsync(CadastroUsuarioDto dto);
        Task<(bool, object)> Login(LoginDto dto); 
    }
}
