using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("quarto")]
    [ApiController]
    public class QuartoController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.QuartoRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.QuartoRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(QuartoDto dto)
        {
            Quarto temp = new()
            {
                Numero_Quarto = dto.Numero_Quarto,
                Status = dto.Status,
                TipoQuarto_Id = dto.TipoQuarto_Id
            };

            await _uof.QuartoRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(QuartoDto dto)
        {
            Quarto? quartoDb = await _uof.QuartoRepository.Get(c => c.Id == dto.Id);

            if (quartoDb != null)
            {
                quartoDb.Numero_Quarto = dto.Numero_Quarto;
                quartoDb.Status = dto.Status;
                quartoDb.TipoQuarto_Id = dto.TipoQuarto_Id;

                _uof.QuartoRepository.Update(quartoDb);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Quarto? quartoDb = await _uof.QuartoRepository.Get(c => c.Id == id);

            if (quartoDb != null)
            {
                _uof.QuartoRepository.Delete(quartoDb);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
