namespace zity.DTOs.Apartments
{
    public class ApartmentPatchDTO
    {
        public float Area { get; set; }
        public string? Description { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string? Status { get; set; }
    }
}
