using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelegramBot.Application.Common.Models.ThreeXUi.Protocols
{
    public class StreamSettings
    {
        [JsonPropertyName("network")]
        public string Network { get; set; } = "tcp"; // tcp, kcp, ws, http, quic, grpc

        [JsonPropertyName("security")]
        public string Security { get; set; } = "none"; // none, tls, reality

        [JsonPropertyName("tlsSettings")]
        public TlsSettings? TlsSettings { get; set; }

        [JsonPropertyName("xtlsSettings")]
        public TlsSettings? XtlsSettings { get; set; }

        [JsonPropertyName("realitySettings")]
        public RealitySettings? RealitySettings { get; set; }

        [JsonPropertyName("wsSettings")]
        public WsSettings? WsSettings { get; set; }

        [JsonPropertyName("tcpSettings")]
        public TcpSettings? TcpSettings { get; set; }

        [JsonPropertyName("grpcSettings")]
        public GrpcSettings? GrpcSettings { get; set; }

        [JsonPropertyName("xhttpSettings")]
        public XhttpSettings? XhttpSettings { get; set; }
    }

    public class TlsSettings
    {
        [JsonPropertyName("serverName")]
        public string ServerName { get; set; } = string.Empty;

        [JsonPropertyName("certificates")]
        public List<Certificate> Certificates { get; set; } = new();

        [JsonPropertyName("alpn")]
        public List<string> Alpn { get; set; } = new();
    }

    public class RealitySettings
    {
        [JsonPropertyName("show")]
        public bool Show { get; set; } = false;

        [JsonPropertyName("xver")]
        public int Xver { get; set; } = 0;

        [JsonPropertyName("dest")]
        public string Dest { get; set; } = "example.com:443";

        [JsonPropertyName("serverNames")]
        public List<string> ServerNames { get; set; } = new();

        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; } = string.Empty;

        [JsonPropertyName("minClient")]
        public string MinClient { get; set; } = "";

        [JsonPropertyName("maxClient")]
        public string MaxClient { get; set; } = "";

        [JsonPropertyName("maxTimediff")]
        public int MaxTimediff { get; set; } = 0;

        [JsonPropertyName("shortIds")]
        public List<string> ShortIds { get; set; } = new();

        [JsonPropertyName("fingerprint")]
        public string Fingerprint { get; set; } = "chrome";
    }

    public class WsSettings
    {
        [JsonPropertyName("path")]
        public string Path { get; set; } = "/";

        [JsonPropertyName("headers")]
        public Dictionary<string, string> Headers { get; set; } = new();
    }

    public class TcpSettings
    {
        [JsonPropertyName("header")]
        public TcpHeader Header { get; set; } = new();
    }

    public class TcpHeader
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "none";
    }

    public class GrpcSettings
    {
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; } = "";
    }

    public class Certificate
    {
        [JsonPropertyName("certificateFile")]
        public string CertificateFile { get; set; } = "";

        [JsonPropertyName("keyFile")]
        public string KeyFile { get; set; } = "";
    }

    public class XhttpSettings
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; } = "auto"; // auto, packet-up, packet-down, stream-up, stream-down

        [JsonPropertyName("path")]
        public string Path { get; set; } = "/";

        [JsonPropertyName("host")]
        public string Host { get; set; } = "";

        [JsonPropertyName("extra")]
        public Dictionary<string, string> Extra { get; set; } = new();
    }
}
