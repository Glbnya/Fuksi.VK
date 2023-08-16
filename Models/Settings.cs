namespace Fuksi.VK.Models
{
    public class Settings
    {
        public string AccessToken { get; set; }
        public ulong GroupId { get; set; }
        public string CallbackUrl { get; set; }
        public string RabbitConnectionString { get; set; }
    }
}
