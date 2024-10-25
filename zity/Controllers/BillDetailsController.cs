using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.BillDetails;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/billdetails")]
    [ApiController]
    public class BillDetailsController(IBillDetailService billDetailService) : ControllerBase
    {
        private readonly IBillDetailService _billDetailService = billDetailService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BillDetailQueryDTO query)
        {
            return Ok(await _billDetailService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _billDetailService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillDetailCreateDTO billDetailCreateDTO)
        {
            var createdBillDetail = await _billDetailService.CreateAsync(billDetailCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdBillDetail.Id }, createdBillDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BillDetailUpdateDTO billDetailUpdateDTO)
        {
            return Ok(await _billDetailService.UpdateAsync(id, billDetailUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] BillDetailPatchDTO billDetailPatchDTO)
        {
            return Ok(await _billDetailService.PatchAsync(id, billDetailPatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _billDetailService.DeleteAsync(id);
            return NoContent();
        }
    }
}
