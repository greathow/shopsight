using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaperID.Services
{
    public class TwilioSmsSender : ISmsSender
    {
        private readonly TwilioSmsAuthMessageSenderOptions smsOptions;

        public TwilioSmsSender(IOptions<TwilioSmsAuthMessageSenderOptions> smsOptionsAccessor)
        {
            this.smsOptions = smsOptionsAccessor.Value;
        }

        public async Task SendSmsAsync(string number, string message)
        {
            var smsClient = new TwilioClient(
                accountSID: this.smsOptions.AccountSID,
                apiKeySID: this.smsOptions.ApiKeySID,
                apiKeySecret: this.smsOptions.ApiKeySecret);

            await smsClient.SendTextAsync(to: number, serviceID: this.smsOptions.SmsServiceID, message: message);
        }
    }
}
