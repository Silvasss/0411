using _0411.Contracts;
using _0411.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpPost("registro")]
        public async Task<IActionResult> Post(CadastroUsuarioDto dto)
        {
            if (await _uof.AutenticacaoRepository.SetPasswordAsync(dto)) { return Created(); }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post2(LoginDto dto)
        {
            var retorno = await _uof.AutenticacaoRepository.Login(dto);
            
            if (retorno.Item1 == true) { return Ok(retorno.Item2); }

            return Unauthorized();
        }
    }
}
