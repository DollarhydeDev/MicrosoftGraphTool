using MicrosoftGraphTool.Models;
using System.Threading.Tasks;

namespace MicrosoftGraphTool.Services.Interfaces
{
    public interface IGraphService
    {
        Task<OAuthToken> GetOAuthToken(string tenantId, string clientId, string clientSecret);
    }
}
