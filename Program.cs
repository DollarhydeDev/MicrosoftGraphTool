using System.Threading.Tasks;
using System;
using MicrosoftGraphTool.Services.Implementations;
using MicrosoftGraphTool.Models;

internal class Program
{
    static async Task Main(string[] args)
    {
        var graphService = new GraphService();

        var tenantId = "YOUR_TENANT_ID";
        var clientId = "YOUR_CLIENT_ID";
        var clientSecret = "YOUR_CLIENT_SECRET";

        OAuthToken oauthToken = new OAuthToken();

        try
        {
            oauthToken = await graphService.GetOAuthToken(tenantId, clientId, clientSecret);

            Console.WriteLine("Access Token:");
            Console.WriteLine(oauthToken.TokenValue);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting auth token: {ex.Message}");
        }

        try
        {
            var distributionGroups = await graphService.GetMicrosoftGroups(oauthToken);
            
            foreach (var group in distributionGroups)
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: {group.Id}");
                Console.WriteLine($"Display Name: {group.DisplayName}");
                Console.WriteLine($"Mail Nickname: {group.MailNickname}");
                Console.WriteLine($"Mail: {group.Mail}");
                Console.WriteLine("-----------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting distribution groups: {ex.Message}");
        }
    }
}