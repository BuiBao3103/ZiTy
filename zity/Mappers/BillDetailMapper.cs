using zity.DTOs.BillDetails;
using zity.Models;

namespace zity.Mappers
{
    public class BillDetailMapper
    {
        public static BillDetailDTO ToDTO(BillDetail billDetail)
        {
            return new BillDetailDTO
            {
                Id = billDetail.Id,
                Price = billDetail.Price,
                CreatedAt = billDetail.CreatedAt,
                UpdatedAt = billDetail.UpdatedAt,
                BillId = billDetail.BillId,
                ServiceId = billDetail.ServiceId,
                Bill = BillMapper.ToDTO(billDetail.Bill),
                // Service = ServiceMapper.ToDTO(billDetail.Service)
            };
        }

        public static BillDetail ToModelFromCreate(BillDetailCreateDTO billDetailCreateDTO)
        {
            return new BillDetail
            {
                Price = billDetailCreateDTO.Price,
                BillId = billDetailCreateDTO.BillId,
                ServiceId = billDetailCreateDTO.ServiceId,
                CreatedAt = DateTime.Now,
            };
        }

        public static void UpdateModelFromUpdate(BillDetail billDetail, BillDetailUpdateDTO updateDTO)
        {
            billDetail.Price = updateDTO.Price;
            billDetail.BillId = updateDTO.BillId;
            billDetail.ServiceId = updateDTO.ServiceId;
            billDetail.UpdatedAt = DateTime.Now;
        }

        public static void PatchModelFromPatch(BillDetail billDetail, BillDetailPatchDTO patchDTO)
        {
            if (patchDTO.Price.HasValue)
                billDetail.Price = patchDTO.Price.Value;
            if (patchDTO.BillId.HasValue)
                billDetail.BillId = patchDTO.BillId.Value;
            if (patchDTO.ServiceId.HasValue)
                billDetail.ServiceId = patchDTO.ServiceId.Value;
            billDetail.UpdatedAt = DateTime.Now;
        }
    }
}
