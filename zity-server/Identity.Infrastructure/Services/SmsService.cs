using System.Text;
using Identity.Application.Core.Services;
using Identity.Domain.Configurations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Identity.Infrastructure.Services;

public class SmsService(AppSettings appSettings, HttpClient httpClient) : ISmsService
{
    private readonly AppSettings _appSettings = appSettings;
    private readonly HttpClient _httpClient = httpClient;
    public async Task SendSMSAsync(string phoneNumber, string message)
    {
        var url = "https://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_post_json/";
        var smsRequest = new
        {
            _appSettings.EsmsSettings.ApiKey,
            Content = message,
            Phone = phoneNumber,
            SecretKey = _appSettings.EsmsSettings.ApiSecret,
            SmsType = "2",
            IsUnicode = "0",
            Brandname = _appSettings.EsmsSettings.BrandName
        };

        var content = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            throw new Exception(message: "Failed to send SMS (HTTP error): " + responseBody);
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
