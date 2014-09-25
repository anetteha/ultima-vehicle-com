using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleStatusCheckedEvent : IEvent
    {
        public string Id { get; set; }
        public int Mileage { get; set; }
    }
}
