using _0411.Contracts;
using _0411.Data;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace _0411.Repositories
{
    public class AutenticacaoRepository(DataContext dataContext, IConfiguration _config) : GenericoRepository<Auth>(dataContext), IAutenticacaoRepository
    {
        public async Task<bool> SetPasswordAsync(CadastroUsuarioDto dto)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = GetPasswordHash(dto.Password, passwordSalt);

            if (await _entityFramework.Set<Auth>().Where(a => a.User == dto.Usuario).FirstOrDefaultAsync() == null)
            {
                Auth novoAuth = new()
                {
                    User = dto.Usuario,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    TipoConta_Id = 2,
                    Usuario = new Usuario()
                    {
                        Nome = dto.Nome,
                        InformacoesEmprego = new Informacoes_Emprego()
                        {
                            Data_Admissao = DateTime.UtcNow,
                            Salario = 1000.00m,
                            Status = "ativo"
                        },
                        InformacoesUsuario = new Informacoes_Usuario() { }
                    }
                };

                await _entityFramework.AddAsync(novoAuth);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<(bool, object)> Login(LoginDto dto)
        {
            Auth? dadosLogin = await _entityFramework.Set<Auth>()
                .Where(a => a.User == dto.Usuario)
                .Include("Usuario")
                .Include(a => a.TipoConta)
                .FirstOrDefaultAsync();

            if (dadosLogin != null)
            {
                byte[] passwordHash = GetPasswordHash(dto.Password, dadosLogin.PasswordSalt);

                for (int index = 0; index < passwordHash.Length; index++)
                {
                    if (passwordHash[index] != dadosLogin.PasswordHash[index])
                    {
                        return (false, null);
                    }
                }

                return (true, new
                {
                    token = CreateToken(dadosLogin.Usuario.Id, dadosLogin.TipoConta.Nome, dto.Usuario),
                    role = dadosLogin.TipoConta.Nome,
                    nome = dadosLogin.Usuario.Nome
                });
            }

            return (false, null);
        }

        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);

            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000000,
                numBytesRequested: 256 / 8
            );
        }

        private string CreateToken(int userId, string role, string userName)
        {

            Claim[] claims = [new Claim("userId", userId.ToString()), new Claim("nome", userName), new Claim(ClaimTypes.Role, role)];

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new(Encoding.UTF8.GetBytes(tokenKeyString ?? ""));

            SigningCredentials credentials = new(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
