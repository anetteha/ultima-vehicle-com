using System;
using NServiceBus.Saga;

namespace Ultima.Vehicle.Service
{
    public class RentalData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        public string BusinessId { get; set; }
        public int Mileage { get; set; }
    }
}