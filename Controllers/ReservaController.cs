using _0411.Contracts;
using _0411.Dtos;
using _0411.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0411.Controllers
{
    [Authorize]
    [Route("reserva")]
    [ApiController]
    public class ReservaController(IUnitOfWork unitOf) : Controller
    {
        private readonly IUnitOfWork _uof = unitOf;

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var resposta = await _uof.ReservaRepository.Get1(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll()
        {
            return Ok(await _uof.ReservaRepository.GetAll2());
        }

        [HttpPost]
        public async Task<IActionResult> Post(ReservaDto dto)
        {
            Reserva temp = new()
            {
                Status = dto.Status,
                Quantidade_Dias = dto.Quantidade_Dias,
                Numero_Quarto = dto.Numero_Quarto,
                Data_Checkin = dto.Data_Checkin,
                Data_Checkout = dto.Data_Checkout,
                Cliente_Id = dto.Cliente_Id
            };

            await _uof.ReservaRepository.Create(temp);

            if (await _uof.Commit() > 0) { return Created(); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ReservaDto dto)
        {
            Reserva? db = await _uof.ReservaRepository.Get(c => c.Id == dto.Id);

            if (db != null)
            {
                db.Status = dto.Status;
                db.Quantidade_Dias = dto.Quantidade_Dias;
                db.Numero_Quarto = dto.Numero_Quarto;
                db.Data_Checkin = dto.Data_Checkin;
                db.Data_Checkout = dto.Data_Checkout;

                _uof.ReservaRepository.Update(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Reserva? db = await _uof.ReservaRepository.Get(c => c.Id == id);

            if (db != null)
            {
                _uof.ReservaRepository.Delete(db);

                if (await _uof.Commit() > 0) { return NoContent(); }
            }

            return NotFound();
        }
    }
}
