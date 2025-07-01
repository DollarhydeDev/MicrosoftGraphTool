using System.Net.Http;
using System.Text.Json;
using System.Text;
using System;
using System.Threading.Tasks;
using MicrosoftGraphTool.Models;

namespace MicrosoftGraphTool.Services.Implementations
{
    public class GraphService
    {
        public async Task<OAuthToken> GetOAuthToken(string tenantId, string clientId, string clientSecret)
        {
            using var client = new HttpClient();
            var tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            string scope = "scope=https%3A%2F%2Fgraph.microsoft.com%2F.default";
            clientId = $"client_id={clientId}";
            clientSecret = $"client_secret={clientSecret}";
            string grantType = "grant_type=client_credentials";

            var requestBody = new StringContent($"{clientId}&{scope}&{clientSecret}&{grantType}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(tokenEndpoint, requestBody);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<OAuthToken>(responseContent);

            if (responseObject == null || string.IsNullOrEmpty(responseObject.TokenValue))
            {
                throw new Exception("Failed to obtain access token.");
            }

            return responseObject;
        }

    }
}
