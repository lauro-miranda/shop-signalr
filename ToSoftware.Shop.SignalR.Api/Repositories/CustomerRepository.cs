using LM.Responses;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using ToSoftware.Shop.SignalR.Api.Domain.Models;
using ToSoftware.Shop.SignalR.Api.Domain.Settings;
using ToSoftware.Shop.SignalR.Api.Repositories.Contracts;

namespace ToSoftware.Shop.SignalR.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected IMongoCollection<Customer> Collection { get; }

        public CustomerRepository(IOptions<NoSQLSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            Collection = database.GetCollection<Customer>(settings.Value.CustomerCollectionName);
        }

        public async Task CreateOrUpdateAsync(Customer customer)
        {
            await DeleteAsync(customer);
            await Collection.InsertOneAsync(customer);
        }

        public async Task<Maybe<Customer>> FindAsync(string identification)
            => await (await Collection
                .FindAsync(a => a.Identification.Equals(identification)))
                .FirstOrDefaultAsync();

        public async Task DeleteAsync(string identification)
            => await Collection.DeleteOneAsync(x => x.Identification.Equals(identification));

        async Task DeleteAsync(Customer customer)
            => await DeleteAsync(customer.Identification);
    }
}