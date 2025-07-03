using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicrosoftGraphTool.Models
{
    public class MicrosoftGroup
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("displayName")]
        public string DisplayName { get; init; } = string.Empty;

        [JsonPropertyName("mail")]
        public string Mail { get; init; } = string.Empty;

        [JsonPropertyName("mailEnabled")]
        public bool MailEnabled { get; init; } = false;

        [JsonPropertyName("mailNickname")]
        public string MailNickname { get; init; } = string.Empty;

        [JsonPropertyName("groupTypes")]
        public List<string> GroupTypes { get; init; } = new List<string>();

        [JsonPropertyName("securityEnabled")]
        public bool SecurityEnabled { get; init; } = false;

        [JsonPropertyName("visibility")]
        public string Visibility { get; init; } = string.Empty;
    }
}
