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
            var billDetail = await _billDetailService.GetByIdAsync(id, includes);
            return billDetail == null ? NotFound() : Ok(billDetail);
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
            var updatedBillDetail = await _billDetailService.UpdateAsync(id, billDetailUpdateDTO);
            return updatedBillDetail == null ? NotFound() : Ok(updatedBillDetail);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] BillDetailPatchDTO billDetailPatchDTO)
        {
            var patchedBillDetail = await _billDetailService.PatchAsync(id, billDetailPatchDTO);
            return patchedBillDetail == null ? NotFound() : Ok(patchedBillDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _billDetailService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}
