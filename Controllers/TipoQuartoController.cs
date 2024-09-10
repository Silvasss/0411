using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("tipoquarto")]
    [ApiController]
    public class TipoQuartoController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;
            
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.TipoQuartoRepository.Get(c => c.Id == id);
            
            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.TipoQuartoRepository.GetSelectAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TipoQuartoDto tipoQuartoDto)
        {
            Tipo_Quarto temp = new()
            {
                Descricao = tipoQuartoDto.Descricao,
                Valor_Diaria = tipoQuartoDto.Valor_Diaria
            };

            await _uof.TipoQuartoRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TipoQuartoDto dto)
        {
            Tipo_Quarto? quartoDb = await _uof.TipoQuartoRepository.Get(c => c.Id == dto.Id);

            if (quartoDb != null)
            {
                quartoDb.Descricao = dto.Descricao;
                quartoDb.Valor_Diaria = dto.Valor_Diaria;

                _uof.TipoQuartoRepository.Update(quartoDb);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Tipo_Quarto? quartoDb = await _uof.TipoQuartoRepository.Get(c => c.Id == id);

            if (quartoDb != null)
            {
                _uof.TipoQuartoRepository.Delete(quartoDb);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }
                       
            return NotFound();
        }
    }
}
