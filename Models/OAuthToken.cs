using System.Text.Json.Serialization;

namespace MicrosoftGraphTool.Models
{
    public class OAuthToken
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; init; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; } = -1;

        [JsonPropertyName("ext_expires_in")]
        public int ExtExpiresIn { get; init; } = -1;

        [JsonPropertyName("access_token")]
        public string TokenValue { get; init; } = string.Empty;
    }
}
