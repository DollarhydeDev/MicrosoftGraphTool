using MicrosoftGraphTool.Services.Implementations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    static async Task Main(string[] args)
    {
        var tenantId = "";
        var clientId = "";

        var graphService = new GraphService();

        var delegatedToken = await graphService.GetDelegatedAccessTokenAsync(tenantId, clientId, new[] { "Chat.ReadWrite", "ChatMessage.Send" });

        var chats = await graphService.GetGroupChatIdsAsync(delegatedToken);
        if (!chats.Any())
        {
            Console.WriteLine("No group chats found for this account.");
            return;
        }

        await graphService.SendGroupChatMessageAsync(delegatedToken, chats[0] ?? string.Empty, "TestMessage!");
        Console.WriteLine("Message sent.");
    }
}
