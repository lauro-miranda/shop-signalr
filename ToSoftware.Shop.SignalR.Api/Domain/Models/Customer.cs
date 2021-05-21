using System;

namespace ToSoftware.Shop.SignalR.Api.Domain.Models
{
    public class Customer
    {
        public Customer(string identification, string connectionId)
        {
            Id = Guid.NewGuid();
            Identification = identification;
            ConnectionId = connectionId;
        }

        public Guid Id { get; set; }

        public string Identification { get; set; }

        public string ConnectionId { get; set; }
    }
}