using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("produto")]
    [ApiController]
    public class ProdutoController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.ProdutoRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.ProdutoRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDto dto)
        {
            Produto temp = new()
            {
                Nome = dto.Nome,
                Valor = dto.Valor
            };

            await _uof.ProdutoRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProdutoDto dto)
        {
            Produto? db = await _uof.ProdutoRepository.Get(c => c.Id == dto.Id);

            if (db != null)
            {
                db.Nome = dto.Nome;
                db.Valor = dto.Valor;

                _uof.ProdutoRepository.Update(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Produto? db = await _uof.ProdutoRepository.Get(c => c.Id == id);

            if (db != null)
            {
                _uof.ProdutoRepository.Delete(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
