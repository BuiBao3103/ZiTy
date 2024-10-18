using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using zity.Configuration;
using zity.DTOs.Momo;
using zity.Models;
using zity.Services.Interfaces;

public class MomoService(MomoSettings momoSettings) : IMomoService
{
    private readonly MomoSettings _momoSettings = momoSettings;

    public async Task<MomoCreatePaymentDto> CreatePaymentAsync(Bill bill, MomoRequestCreatePaymentDto request)
    {
        Console.WriteLine("Creating Momo payment...");
        var billInfo = $"Payment for the bill {bill.Monthly}";
        var requestId = Guid.NewGuid().ToString();
        var orderId = $"{DateTime.Now.Ticks}_{bill.Id}";
        var notifyUrl = _momoSettings.NotifyUrl.Replace("{{id}}", bill.Id.ToString());
        // Tạo extraData
        var extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { bill_id = bill.Id })));

        // Xây dựng rawData với các thuộc tính theo đúng thứ tự a-z
        var rawData =
            $"accessKey={_momoSettings.AccessKey}&amount={bill.TotalPrice}&extraData={extraData}" +
            $"&ipnUrl={notifyUrl}&orderId={orderId}&orderInfo={billInfo}" +
            $"&partnerCode={_momoSettings.PartnerCode}&redirectUrl={_momoSettings.ReturnUrl}" +
            $"&requestId={requestId}&requestType={request.RequestType}";

        // Tạo chữ ký
        var signature = ComputeHmacSha256(rawData, _momoSettings.SecretKey);

        // Tạo RestClient và RestRequest
        var client = new RestClient(_momoSettings.MomoApiUrl);
        var restRequest = new RestRequest() { Method = Method.Post };
        restRequest.AddHeader("Content-Type", "application/json; charset=UTF-8");

        // Dữ liệu gửi lên API
        var requestData = new
        {
            partnerCode = _momoSettings.PartnerCode,
            requestId,
            amount = (long)bill.TotalPrice,
            orderId,
            orderInfo = billInfo,
            redirectUrl = _momoSettings.ReturnUrl,
            ipnUrl = notifyUrl,
            lang = "vi",
            orderExpireTime = 15,
            extraData,
            requestType = request.RequestType,
            signature,
            autoCapture = true
        };

        restRequest.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

        try
        {
            var response = await client.ExecuteAsync(restRequest);
            var momoCreatePaymentDto = JsonConvert.DeserializeObject<MomoCreatePaymentDto>(response.Content);
            if (response.IsSuccessful)
            {
                return momoCreatePaymentDto;
            }
            else
            {
                // Log the error and throw an exception
                Console.WriteLine($"Error: {momoCreatePaymentDto.Message}");
                throw new Exception($"Failed to create Momo payment: {momoCreatePaymentDto.}");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }
    }

    // Phương thức tạo chữ ký HMAC-SHA256
    private static string ComputeHmacSha256(string rawData, string secretKey)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}