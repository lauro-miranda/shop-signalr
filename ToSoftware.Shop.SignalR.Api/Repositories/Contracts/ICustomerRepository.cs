using LM.Responses;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Domain.Models;

namespace ToSoftware.Shop.SignalR.Api.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        Task<Maybe<Customer>> FindAsync(string identification);

        Task CreateOrUpdateAsync(Customer customer);
    }
}