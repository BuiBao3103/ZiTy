namespace zity.DTOs.Items
{
    public class ItemPatchDTO
    {
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool? IsReceive { get; set; }
        public int? UserId { get; set; }
    }
}
