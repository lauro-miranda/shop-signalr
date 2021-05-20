using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Hubs;
using ToSoftware.Shop.SignalR.Api.Hubs.Contracts;
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

        public async Task<IActionResult> SendMessageAsync([FromBody] string identification, string message)
        {
            var customer = await Repository.FindAsync(identification);

            if (!customer.HasValue)
                return NoContent();

            await Hub.Clients.Client(customer.Value.ConnectionId).OnMessage(message);

            return Accepted();
        }
    }
}