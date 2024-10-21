using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Bills;
using zity.DTOs.Momo;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillController(IBillService billService) : ControllerBase
    {
        private readonly IBillService _billService = billService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BillQueryDTO queryParam)
        {
            var result = await _billService.GetAllAsync(queryParam);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] string? includes)
        {
            var result = await _billService.GetByIdAsync(id, includes);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillCreateDTO billCreateDTO)
        {
            var result = await _billService.CreateAsync(billCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BillUpdateDTO billUpdateDTO)
        {
            var result = await _billService.UpdateAsync(id, billUpdateDTO);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] BillPatchDTO billPatchDTO)
        {
            var result = await _billService.PatchAsync(id, billPatchDTO);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _billService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/payment/vnpay")]
        public async Task<IActionResult> CreatePaymentUrl(int id)
        {

            var paymentUrl = await _billService.CreatePaymentVNPayAsync(id);
            return Ok(new { paymentUrl });
        }

        [HttpPost("{id}/payment/momo")]
        public async Task<IActionResult> CreatePaymentMomo(int id, [FromBody] MomoRequestCreatePaymentDto request)
        {
            var payment = await _billService.CreatePaymentMomoAsync(id, request);
            return Ok(payment);
        }

        [HttpPost("{id}/momo-callback")]
        public async Task<IActionResult> MoMoCallBack([FromRoute] int id, [FromBody] MomoCallBackDto callbackDto)
        {
            await _billService.HandleMoMoCallBackAsync(id, callbackDto);
            return NoContent(); 
        }
    }
}