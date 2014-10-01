using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleRenterQualifiedFor360ViewEvent : IEvent
    {
        public string Id { get; set; }
    }
}
