using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("cliente")]
    [ApiController]
    public class Cliente_Controller(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.ClienteRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.ClienteRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto dto)
        {
            if (await _uof.ClienteRepository.Post(int.Parse(User.Claims.First(x => x.Type == "userId").Value), dto)) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ClienteDto dto)
        {
            if (await _uof.ClienteRepository.Put(int.Parse(User.Claims.First(x => x.Type == "userId").Value), dto)) { return NoContent(); }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cliente? db = await _uof.ClienteRepository.Get(c => c.Id == id);

            if (db != null)
            {
                _uof.ClienteRepository.Delete(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
