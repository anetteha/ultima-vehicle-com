using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleIsRentedEvent:IEvent
    {
        public string Id { get; set; }
    }
}
