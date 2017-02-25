using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaperID.Services
{
    public class TwilioClient
    {
        private readonly HttpClient httpClient;

        public TwilioClient(string accountSID, string apiKeySID, string apiKeySecret)
        {
            var encoding = new ASCIIEncoding();
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(encoding.GetBytes($"{apiKeySID}:{apiKeySecret}")));
            this.httpClient.BaseAddress = new Uri($"https://api.twilio.com/2010-04-01/Accounts/{accountSID}/", UriKind.Absolute);
        }

        public async Task SendTextAsync(string to, string serviceID, string message)
        {
            var fields = new Dictionary<string, string>
            {
                { "To", to },
                { "MessagingServiceSid", serviceID },
                { "Body", message }
            };

            var httpcontent = new FormUrlEncodedContent(fields);
            var response = await this.httpClient.PostAsync("Messages.json", httpcontent);
        }
    }
}
