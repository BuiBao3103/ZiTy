using System.Text;
using Application.Core.Services;
using MyApp.Domain.Configurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services;

public class SmsService(AppSettings appSettings) : ISmsService
{
    private readonly AppSettings _appSettings = appSettings;

    public async Task SendSMSAsync(string phoneNumber, string message)
    {
        using var client = new HttpClient();
        var url = "https://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_post_json/";
        var smsRequest = new
        {
            ApiKey = _appSettings.EsmsSettings.ApiKey,
            Content = message,
            Phone = phoneNumber,
            SecretKey = _appSettings.EsmsSettings.ApiSecret,
            SmsType = "2",
            IsUnicode = "0",
            Brandname = _appSettings.EsmsSettings.BrandName
        };

        var content = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            //var responseBody = await response.Content.ReadAsStringAsync();
            //throw new AppError(message: "Failed to send SMS (HTTP error): " + responseBody, statusCode: StatusCodes.Status500InternalServerError, errorCode: "SEND_SMS_FAILED");
        }

        var responseBodyString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JObject.Parse(responseBodyString);

        var codeResult = (string?)jsonResponse["CodeResult"];
        if (codeResult != "100")
        {
            var errorMessage = (string?)jsonResponse["ErrorMessage"] ?? "Unknown error";
            throw new Exception(message: "Failed to send SMS (API error): " + errorMessage);
        }
    }
}
