using System.Diagnostics;
using zity.Configuration;
using zity.Constants.Parameters;
using zity.Models;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class VNPayService(IHttpContextAccessor httpContextAccessor, VNPaySettings vnpaySettings) : IVNPayService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly VNPaySettings _vnpaySettings = vnpaySettings;

        public string CreatePaymentUrl (Bill bill)
        {
            Debug.WriteLine(_vnpaySettings.Url);
            Debug.WriteLine(_vnpaySettings.HashSecret);
            Debug.WriteLine(_vnpaySettings.TmnCode);
            var vnpay = new VnPayLibrary();

            // Lấy địa chỉ IP từ HttpContext thông qua HttpContextAccessor
            var ipAddress = Utils.GetIpAddress(_httpContextAccessor.HttpContext);

            // Thêm dữ liệu vào request
            vnpay.AddRequestData(VNPayParameter.VERSION, "2.1.0");
            vnpay.AddRequestData(VNPayParameter.COMMAND, "pay");
            vnpay.AddRequestData(VNPayParameter.TMN_CODE, _vnpaySettings.TmnCode); // Thay bằng mã TMN thực tế
            vnpay.AddRequestData(VNPayParameter.AMOUNT, (bill.TotalPrice * 100).ToString()); // VNPAY yêu cầu giá trị nhân 100
            vnpay.AddRequestData(VNPayParameter.CREATE_DATE, DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData(VNPayParameter.CURR_CODE, "VND");
            vnpay.AddRequestData(VNPayParameter.IP_ADDR, ipAddress); // Sử dụng địa chỉ IP lấy được
            vnpay.AddRequestData(VNPayParameter.LOCALE, "vn");
            vnpay.AddRequestData(VNPayParameter.ORDER_INFO, $"Thanh toan hoa don thang {bill.Monthly}");
            vnpay.AddRequestData(VNPayParameter.ORDER_TYPE, "250006"); //Mã danh mục hàng hóa do VNPAY quy định.
            vnpay.AddRequestData(VNPayParameter.RETURN_URL, $"http://localhost:5173/bills/{bill.Id}"); // URL để VNPAY trả kết quả về
            vnpay.AddRequestData(VNPayParameter.TXN_REF, bill.Id.ToString()); // Số tham chiếu giao dịch là Id của hóa đơn
            vnpay.AddRequestData(VNPayParameter.EXPIRE_DATE, DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            string paymentUrl = vnpay.CreateRequestUrl(_vnpaySettings.Url, _vnpaySettings.HashSecret);

            return paymentUrl;
        }
    }
}
