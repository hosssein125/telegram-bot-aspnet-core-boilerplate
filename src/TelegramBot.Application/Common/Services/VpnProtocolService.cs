using System.Text.Json;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Models.ThreeXUi;
using TelegramBot.Application.Common.Models.ThreeXUi.Protocols;

namespace TelegramBot.Application.Common.Services
{
    public interface IVpnProtocolService
    {
        Task<InboundDto> CreateVlessInboundAsync(string remark, int port, string uuid, string flow = "xtls-rprx-vision", string security = "reality", string publicKey = "", string privateKey = "");
        Task<InboundDto> CreateVmessInboundAsync(string remark, int port, string uuid);
        Task<InboundDto> CreateTrojanInboundAsync(string remark, int port, string password);
        Task<InboundDto> CreateShadowsocksInboundAsync(string remark, int port, string password, string method = "aes-256-gcm");
    }

    public class VpnProtocolService : IVpnProtocolService
    {
        private readonly IThreeXUiService _threeXUiService;

        public VpnProtocolService(IThreeXUiService threeXUiService)
        {
            _threeXUiService = threeXUiService;
        }

        public async Task<InboundDto> CreateVlessInboundAsync(string remark, int port, string uuid, string flow = "xtls-rprx-vision", string security = "reality", string publicKey = "", string privateKey = "")
        {
            var vlessSettings = new VlessSettings
            {
                Clients = new List<VlessClient>
                {
                    new VlessClient
                    {
                        Id = uuid,
                        Flow = flow,
                        Enable = true
                    }
                },
                Decryption = "none"
            };

            var streamSettings = new StreamSettings
            {
                Network = "tcp",
                Security = security,
                RealitySettings = security == "reality" ? new RealitySettings
                {
                    Show = false,
                    Dest = "www.microsoft.com:443",
                    ServerNames = new List<string> { "www.microsoft.com" },
                    PrivateKey = privateKey,
                    ShortIds = new List<string> { "16" },
                    Fingerprint = "chrome"
                } : null
            };

            var inbound = new InboundDto
            {
                Remark = remark,
                Port = port,
                Protocol = "vless",
                Settings = JsonSerializer.Serialize(vlessSettings),
                StreamSettings = JsonSerializer.Serialize(streamSettings),
                Enable = true,
                Sniffing = JsonSerializer.Serialize(new { enabled = true, destOverride = new[] { "http", "tls" } })
            };

            return inbound;
        }

        public async Task<InboundDto> CreateVmessInboundAsync(string remark, int port, string uuid)
        {
            var vmessSettings = new VmessSettings
            {
                Clients = new List<VmessClient>
                {
                    new VmessClient
                    {
                        Id = uuid,
                        AlterId = 0,
                        Enable = true
                    }
                }
            };

            var streamSettings = new StreamSettings
            {
                Network = "tcp",
                Security = "none"
            };

            var inbound = new InboundDto
            {
                Remark = remark,
                Port = port,
                Protocol = "vmess",
                Settings = JsonSerializer.Serialize(vmessSettings),
                StreamSettings = JsonSerializer.Serialize(streamSettings),
                Enable = true
            };

            return inbound;
        }

        public async Task<InboundDto> CreateTrojanInboundAsync(string remark, int port, string password)
        {
            var trojanSettings = new TrojanSettings
            {
                Clients = new List<TrojanClient>
                {
                    new TrojanClient
                    {
                        Password = password,
                        Enable = true
                    }
                }
            };

            var streamSettings = new StreamSettings
            {
                Network = "tcp",
                Security = "tls",
                TlsSettings = new TlsSettings
                {
                    // Assuming certificates are managed centrally or omitted for self-signed
                }
            };

            var inbound = new InboundDto
            {
                Remark = remark,
                Port = port,
                Protocol = "trojan",
                Settings = JsonSerializer.Serialize(trojanSettings),
                StreamSettings = JsonSerializer.Serialize(streamSettings),
                Enable = true
            };

            return inbound;
        }

        public async Task<InboundDto> CreateShadowsocksInboundAsync(string remark, int port, string password, string method = "aes-256-gcm")
        {
            var ssSettings = new ShadowsocksSettings
            {
                Password = password,
                Method = method,
                Network = "tcp,udp"
            };

            var inbound = new InboundDto
            {
                Remark = remark,
                Port = port,
                Protocol = "shadowsocks",
                Settings = JsonSerializer.Serialize(ssSettings),
                StreamSettings = JsonSerializer.Serialize(new StreamSettings { Network = "tcp,udp" }),
                Enable = true
            };

            return inbound;
        }
    }
}
