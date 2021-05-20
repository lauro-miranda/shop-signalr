using System;

namespace ToSoftware.Shop.SignalR.Api.Domain.Models
{
    public class Customer
    {
        public Guid Code { get; set; }

        public string Identification { get; set; }

        public string ConnectionId { get; set; }
    }
}