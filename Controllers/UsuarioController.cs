using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("usuario")]
    [ApiController]
    public class UsuarioController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            return Ok(await _uof.UsuarioRepository.Get1(int.Parse(User.Claims.First(x => x.Type == "userId").Value)));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UsuarioDto dto)
        {
            if (await _uof.UsuarioRepository.Put(int.Parse(User.Claims.First(x => x.Type == "userId").Value), dto)) { return NoContent(); }

            return BadRequest();
        }
    }
}
