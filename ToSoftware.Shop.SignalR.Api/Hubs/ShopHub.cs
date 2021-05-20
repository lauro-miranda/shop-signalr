using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Hubs.Contracts;

namespace ToSoftware.Shop.SignalR.Api.Hubs
{
    public class ShopHub : Hub<IShopHub>
    {
        HttpClient HttpClient { get; }

        IUser User { get; }

        public ShopHub(IHttpClientFactory httpClientFactory
            , IUser user)
        {
            HttpClient = httpClientFactory.CreateClient("");
            User = user;
        }

        public override async Task OnConnectedAsync()
        {
            var obj = new
            {
                Context.ConnectionId,
                User.Identification
            };

            var content = new ObjectContent<object>(obj, new JsonMediaTypeFormatter());

            var httpResponse = await HttpClient.PostAsync("auth", content);
        }
    }
}