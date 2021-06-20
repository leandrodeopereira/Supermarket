namespace SupermarketApi.Configuration
{
    public sealed class StripeSettings
    {
        public string? PublishibleKey { get; set; }

        public string? SecretKey { get; set; }

        public string? WebhookSecret { get; set; }
    }
}
