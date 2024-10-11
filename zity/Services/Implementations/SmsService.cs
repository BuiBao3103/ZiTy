using System.Diagnostics;
using Vonage;
using Vonage.Request;
using zity.Configuration;
using zity.ExceptionHandling;
using zity.Services.Interfaces;

namespace zity.Services.Implementations
{
    public class SmsService(VonageSettings vonageSettings) : ISmsService
    {
        private readonly VonageSettings _vonageSettings = vonageSettings;
        public async Task SendSMSAsync(string phoneNumber, string message)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
               _vonageSettings.ApiKey,
               _vonageSettings.ApiSecret
            );

            var vonageClient = new VonageClient(credentials);

            var smsRequest = new Vonage.Messaging.SendSmsRequest
            {
                To = phoneNumber,
                From = _vonageSettings.BrandName,
                Text = message
            };

            var response = await vonageClient.SmsClient.SendAnSmsAsync(smsRequest);
            if (response.Messages[0].Status != "0")
            {
                throw new AppError(message: "Failed to send SMS with error: " + response.Messages[0].ErrorText);
            }

        }
    }

}
