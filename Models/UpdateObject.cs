using System.Text.Json.Serialization;

namespace Fuksi.VK.Models
{
    public class UpdateObject
    {
        /// <summary>
        /// ID юзера, который совершил действие или с которым совершили действие
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }
        /// <summary>
        /// Указывает, как именно был добавлен участник
        /// Возможные значения:join,unsure,accepted,approved,request
        /// </summary>
        [JsonPropertyName("join_type")]
        public string? GroupLeave { get; set; }
        /// <summary>
        /// Значение, указывающее, был пользователь удален или вышел самостоятельно
        /// </summary>
        [JsonPropertyName("self")]
        public int LeaveType { get; set; }
    }
}
