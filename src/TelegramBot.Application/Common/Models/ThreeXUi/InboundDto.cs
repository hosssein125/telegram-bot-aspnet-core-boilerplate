using System.Text.Json.Serialization;

namespace TelegramBot.Application.Common.Models.ThreeXUi
{
    public class InboundDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("up")]
        public long Up { get; set; }

        [JsonPropertyName("down")]
        public long Down { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }

        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;

        [JsonPropertyName("enable")]
        public bool Enable { get; set; }

        [JsonPropertyName("expiryTime")]
        public long ExpiryTime { get; set; }

        [JsonPropertyName("listen")]
        public string Listen { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; } = string.Empty;

        /// <summary>
        /// Protocol specific settings (VMess, VLESS, Trojan, etc.)
        /// </summary>
        [JsonPropertyName("settings")]
        public string Settings { get; set; } = "{}";

        /// <summary>
        /// Transport settings (TCP, WS, etc.)
        /// </summary>
        [JsonPropertyName("streamSettings")]
        public string StreamSettings { get; set; } = "{}";

        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;

        [JsonPropertyName("sniffing")]
        public string Sniffing { get; set; } = "{}";
    }

    public class LoginResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;
    }

    public class GenericResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;

        [JsonPropertyName("obj")]
        public T? Obj { get; set; }
    }
}
