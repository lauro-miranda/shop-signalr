using System.Threading.Tasks;

namespace ToSoftware.Shop.SignalR.Api.Hubs.Contracts
{
    public interface IShopHub
    {
        Task OnMessage(string message);
    }
}