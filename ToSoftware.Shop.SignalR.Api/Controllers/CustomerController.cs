using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Hubs;
using ToSoftware.Shop.SignalR.Api.Hubs.Contracts;
using ToSoftware.Shop.SignalR.Api.Messages;
using ToSoftware.Shop.SignalR.Api.Repositories.Contracts;

namespace ToSoftware.Shop.SignalR.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CustomerController : Controller
    {
        IHubContext<ShopHub, IShopHub> Hub { get; }

        ICustomerRepository Repository { get; }

        public CustomerController(IHubContext<ShopHub, IShopHub> hub
            , ICustomerRepository repository)
        {
            Hub = hub;
            Repository = repository;
        }

        [HttpPost, Route("message")]
        public async Task<IActionResult> SendMessageAsync([FromBody] MessageRequestMessage requestMessage)
        {
            var customer = await Repository.FindAsync(requestMessage.Identification);

            if (!customer.HasValue)
                return NoContent();

            await Hub.Clients.Client(customer.Value.ConnectionId).OnMessage(requestMessage.Message);

            return Accepted();
        }
    }
}