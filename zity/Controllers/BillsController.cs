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
            return Ok(await _billService.GetAllAsync(queryParam));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _billService.GetByIdAsync(id, includes));
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
            return Ok(await _billService.UpdateAsync(id, billUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] BillPatchDTO billPatchDTO)
        {
            return Ok(await _billService.PatchAsync(id, billPatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _billService.DeleteAsync(id);
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
            return Ok(await _billService.CreatePaymentMomoAsync(id, request));
        }

        [HttpPost("{id}/momo-callback")]
        public async Task<IActionResult> MoMoCallBack([FromRoute] int id, [FromBody] MomoCallBackDto callbackDto)
        {
            await _billService.HandleMoMoCallBackAsync(id, callbackDto);
            return NoContent();
        }
    }
}