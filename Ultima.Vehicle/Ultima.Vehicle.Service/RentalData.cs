using System;
using NServiceBus.Saga;

namespace Ultima.Vehicle.Service
{
    public class RentalData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        [Unique] 
        public string BusinessId { get; set; }
        public int Mileage { get; set; }
        public bool Vip { get; set; }
        public bool BrakesNeedService { get; set; }
        public int MilesSinceLastStatusCheck { get; set; }
        public int NumberOfStatusChecks { get; set; }
        public int TotalMilageUsedWhileRented { get; set; }
        public int MileageWhenReturned { get; set; }
    }
}