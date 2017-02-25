namespace PaperID.Services
{
    public class TwilioSmsAuthMessageSenderOptions
    {
        public string AccountSID { get; set; }

        public string ApiKeySID { get; set; }

        public string ApiKeySecret { get; set; }

        public string SmsServiceID { get; set; }
    }
}
