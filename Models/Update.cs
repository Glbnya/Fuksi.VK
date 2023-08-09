using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Fuksi.VK.Models
{
    public class Update
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Объект, инициировавший событие
        /// Структура объекта зависит от типа уведомления
        /// </summary>
        [JsonPropertyName("object")]
        public UpdateObject? Object { get; set; }

        /// <summary>
        /// Цветочный Id
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }
    }
}
