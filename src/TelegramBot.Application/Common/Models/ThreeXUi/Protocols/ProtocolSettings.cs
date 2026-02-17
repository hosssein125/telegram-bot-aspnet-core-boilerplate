using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelegramBot.Application.Common.Models.ThreeXUi.Protocols
{
    // --- Clients ---

    public class Client
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty; // UUID or Password

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("limitIp")]
        public int LimitIp { get; set; } = 0;

        [JsonPropertyName("totalGB")]
        public long TotalGB { get; set; } = 0;

        [JsonPropertyName("expiryTime")]
        public long ExpiryTime { get; set; } = 0;

        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("tgId")]
        public string TgId { get; set; } = string.Empty;

        [JsonPropertyName("subId")]
        public string SubId { get; set; } = string.Empty;
    }

    public class VlessClient : Client
    {
        [JsonPropertyName("flow")]
        public string Flow { get; set; } = string.Empty;
    }

    public class VmessClient : Client
    {
        [JsonPropertyName("alterId")]
        public int AlterId { get; set; } = 0;
    }

    public class TrojanClient : Client
    {
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        // Trojan often uses 'password' instead of 'id', but in 3x-ui Client struct, 
        // usually 'id' maps to the main credential. 
        // However, standard Trojan config has 'password'. 
        // 3x-ui often unifies this. Let's check typical usage. 
        // Usually for Trojan in 3x-ui: id=uuid (if used as pass) or password field.
        // Actually for Trojan, the 'password' field is key. 
        // But let's support both just in case.
    }

    // --- Settings ---

    public class VlessSettings
    {
        [JsonPropertyName("clients")]
        public List<VlessClient> Clients { get; set; } = new();

        [JsonPropertyName("decryption")]
        public string Decryption { get; set; } = "none";

        [JsonPropertyName("fallbacks")]
        public List<object> Fallbacks { get; set; } = new();
    }

    public class VmessSettings
    {
        [JsonPropertyName("clients")]
        public List<VmessClient> Clients { get; set; } = new();

        [JsonPropertyName("disableInsecureEncryption")]
        public bool DisableInsecureEncryption { get; set; } = false;
    }

    public class TrojanSettings
    {
        [JsonPropertyName("clients")]
        public List<TrojanClient> Clients { get; set; } = new();

        [JsonPropertyName("fallbacks")]
        public List<object> Fallbacks { get; set; } = new();
    }

    public class ShadowsocksSettings
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = "aes-256-gcm";

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("network")]
        public string Network { get; set; } = "tcp,udp";
    }

    public class SocksSettings
    {
        [JsonPropertyName("auth")]
        public string Auth { get; set; } = "password"; // "noauth" or "password"

        [JsonPropertyName("accounts")]
        public List<Account> Accounts { get; set; } = new();

        [JsonPropertyName("udp")]
        public bool Udp { get; set; } = false;

        [JsonPropertyName("ip")]
        public string Ip { get; set; } = "127.0.0.1";
    }

    public class MixedSettings
    {
        [JsonPropertyName("auth")]
        public string Auth { get; set; } = "noauth";

        [JsonPropertyName("accounts")]
        public List<Account> Accounts { get; set; } = new();

        [JsonPropertyName("udp")]
        public bool Udp { get; set; } = false;

        [JsonPropertyName("ip")]
        public string Ip { get; set; } = "127.0.0.1";
    }

    public class HttpSettings
    {
        [JsonPropertyName("accounts")]
        public List<Account> Accounts { get; set; } = new();
    }

    public class Account
    {
        [JsonPropertyName("user")]
        public string User { get; set; } = string.Empty;

        [JsonPropertyName("pass")]
        public string Pass { get; set; } = string.Empty;
    }

    public class DokodemoDoorSettings
    {
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("network")]
        public string Network { get; set; } = "tcp,udp";
    }

    public class WireGuardSettings
    {
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; } = string.Empty;

        [JsonPropertyName("peers")]
        public List<WireGuardPeer> Peers { get; set; } = new();

        [JsonPropertyName("mtu")]
        public int Mtu { get; set; } = 1420;
    }

    public class WireGuardPeer
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; } = string.Empty;

        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; } = string.Empty;
    }
}
