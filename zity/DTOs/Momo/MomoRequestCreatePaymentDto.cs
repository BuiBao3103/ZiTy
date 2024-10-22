using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Momo
{
    public class MomoRequestCreatePaymentDto
    {
        [Required(ErrorMessage = "RequestType is required.")]
        [RegularExpression("^(captureWallet|payWithATM|payWithCC)$", ErrorMessage = "Invalid RequestType. Allowed values are captureWallet, payWithATM, payWithCC.")]
        public string RequestType { get; set; } = null!;
    }
}
