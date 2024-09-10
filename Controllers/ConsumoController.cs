using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("consumo")]
    [ApiController]
    public class ConsumoController(IUnitOfWork unitOf) : Controller
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.ConsumoRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.ConsumoRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConsumoDto dto)
        {
            Consumo temp = new()
            {
                Quantidade = dto.Quantidade,
                Produto_Id = dto.Produto_Id,
                Reserva_Id = dto.Reserva_Id
            };

            await _uof.ConsumoRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ConsumoDto dto)
        {
            Consumo? db = await _uof.ConsumoRepository.Get(c => c.Id == dto.Id);

            if (db != null)
            {
                db.Quantidade = dto.Quantidade;
                db.Produto_Id = dto.Produto_Id;

                _uof.ConsumoRepository.Update(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Consumo? db = await _uof.ConsumoRepository.Get(c => c.Id == id);

            if (db != null)
            {
                _uof.ConsumoRepository.Delete(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
