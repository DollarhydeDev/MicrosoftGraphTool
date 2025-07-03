using System.Net.Http;
using System.Text.Json;
using System.Text;
using System;
using System.Threading.Tasks;
using MicrosoftGraphTool.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraphTool.Services.Implementations
{
    public class GraphService
    {
        private static HttpClient GetHttpClientFromToken (string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }

        public async Task<OAuthToken> GetOAuthToken(string tenantId, string clientId, string clientSecret)
        {
            using var client = new HttpClient();
            var endpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            string scope = "scope=https%3A%2F%2Fgraph.microsoft.com%2F.default";
            clientId = $"client_id={clientId}";
            clientSecret = $"client_secret={clientSecret}";
            string grantType = "grant_type=client_credentials";

            var requestBody = new StringContent($"{clientId}&{scope}&{clientSecret}&{grantType}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(endpoint, requestBody);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<OAuthToken>(responseContent);

            if (responseObject == null || string.IsNullOrEmpty(responseObject.TokenValue))
            {
                throw new Exception("Failed to obtain access token.");
            }

            return responseObject;
        }

        public async Task<List<MicrosoftGroup>> GetMicrosoftGroups(OAuthToken authToken)
        {
            using var client = GetHttpClientFromToken(authToken.TokenValue);
            var endpoint = "https://graph.microsoft.com/v1.0/groups?$select=id,displayName,mail,mailEnabled,mailNickname,groupTypes,securityEnabled,visibility";

            var response = await client.GetAsync(endpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;

            if (!root.TryGetProperty("value", out JsonElement valueArray)) throw new Exception("Response does not contain 'value'");

            var list = JsonSerializer.Deserialize<List<MicrosoftGroup>>(valueArray.GetRawText());
            if (list == null || list.Count == 0) throw new Exception("No distribution groups found or deserialization failed.");

            return list;
        }
    }
}
