using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("pagamento")]
    [ApiController]
    public class PagamentoController(IUnitOfWork unitOf) : ControllerBase
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.PagamentoRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.PagamentoRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(PagamentoDto dto)
        {
            Pagamento temp = new()
            {
                Valor_Pago = dto.Valor_Pago,
                Valor_Total = dto.Valor_Total,
                Forma_Pagamento = dto.Forma_Pagamento,
                Status = dto.Status,
                Observacoes = dto.Observacoes,
                Data_Pagamento = dto.Data_Pagamento,
                Reserva_Id = dto.Reserva_Id
            };

            await _uof.PagamentoRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PagamentoDto dto)
        {
            Pagamento? db = await _uof.PagamentoRepository.Get(c => c.Id == dto.Id);

            if (db != null)
            {
                db.Valor_Pago = dto.Valor_Pago;
                db.Valor_Total = dto.Valor_Total;
                db.Forma_Pagamento = dto.Forma_Pagamento;
                db.Status = dto.Status;
                db.Observacoes = dto.Observacoes;
                db.Data_Pagamento = dto.Data_Pagamento;

                _uof.PagamentoRepository.Update(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Pagamento? db = await _uof.PagamentoRepository.Get(c => c.Id == id);

            if (db != null)
            {
                _uof.PagamentoRepository.Delete(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
