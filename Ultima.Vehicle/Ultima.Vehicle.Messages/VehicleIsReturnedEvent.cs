using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleIsReturnedEvent : IEvent
    {
        public string Id { get; set; }
    }
}
