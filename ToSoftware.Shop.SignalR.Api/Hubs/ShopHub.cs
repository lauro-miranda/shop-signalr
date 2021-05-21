using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Domain;
using ToSoftware.Shop.SignalR.Api.Domain.Models;
using ToSoftware.Shop.SignalR.Api.Hubs.Contracts;
using ToSoftware.Shop.SignalR.Api.Repositories.Contracts;

namespace ToSoftware.Shop.SignalR.Api.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShopHub : Hub<IShopHub>
    {
        IUser User { get; }

        ICustomerRepository CustomerRepository { get; }

        public ShopHub(IUser user
            , ICustomerRepository customerRepository)
        {
            User = user;
            CustomerRepository = customerRepository;
        }

        public override async Task OnConnectedAsync()
            => await CustomerRepository.CreateOrUpdateAsync(new Customer(User.Identification, Context.ConnectionId));

        public override async Task OnDisconnectedAsync(Exception exception)
            => await CustomerRepository.DeleteAsync(Context.ConnectionId);
    }
}