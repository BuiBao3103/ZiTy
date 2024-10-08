using zity.DTOs.Bills;
using zity.Models;

namespace zity.Mappers
{
    public class BillMapper
    {
        public static BillDTO ToDTO(Bill bill)
        {
            return new BillDTO
            {
                Id = bill.Id,
                Monthly = bill.Monthly,
                TotalPrice = bill.TotalPrice,
                OldWater = bill.OldWater,
                NewWater = bill.NewWater,
                WaterReadingDate = bill.WaterReadingDate,
                Status = bill.Status,
                CreatedAt = bill.CreatedAt,
                UpdatedAt = bill.UpdatedAt,
                RelationshipId = bill.RelationshipId,
                Relationship = RelationshipMapper.ToDTO(bill.Relationship),
                BillDetails = bill.BillDetails.Select(BillDetailMapper.ToDTO).ToList()
            };
        }

        public static Bill ToModelFromCreate(BillCreateDTO billCreateDTO)
        {
            return new Bill
            {
                Monthly = billCreateDTO.Monthly,
                TotalPrice = billCreateDTO.TotalPrice,
                OldWater = billCreateDTO.OldWater,
                NewWater = billCreateDTO.NewWater,
                WaterReadingDate = billCreateDTO.WaterReadingDate,
                Status = billCreateDTO.Status,
                RelationshipId = billCreateDTO.RelationshipId,
                CreatedAt = DateTime.Now,
            };
        }

        public static void UpdateModelFromUpdate(Bill bill, BillUpdateDTO updateDTO)
        {
            bill.Monthly = updateDTO.Monthly;
            bill.TotalPrice = updateDTO.TotalPrice;
            bill.OldWater = updateDTO.OldWater;
            bill.NewWater = updateDTO.NewWater;
            bill.WaterReadingDate = updateDTO.WaterReadingDate;
            bill.Status = updateDTO.Status;
            bill.RelationshipId = updateDTO.RelationshipId;
            bill.UpdatedAt = DateTime.Now;
        }

        public static void PatchModelFromPatch(Bill bill, BillPatchDTO patchDTO)
        {
            if (patchDTO.Monthly != null)
                bill.Monthly = patchDTO.Monthly;
            if (patchDTO.TotalPrice.HasValue)
                bill.TotalPrice = patchDTO.TotalPrice.Value;
            if (patchDTO.OldWater.HasValue)
                bill.OldWater = patchDTO.OldWater.Value;
            if (patchDTO.NewWater.HasValue)
                bill.NewWater = patchDTO.NewWater.Value;
            if (patchDTO.WaterReadingDate.HasValue)
                bill.WaterReadingDate = patchDTO.WaterReadingDate.Value;
            if (patchDTO.Status != null)
                bill.Status = patchDTO.Status;
            if (patchDTO.RelationshipId.HasValue)
                bill.RelationshipId = patchDTO.RelationshipId.Value;
            bill.UpdatedAt = DateTime.Now;
        }
    }
}
