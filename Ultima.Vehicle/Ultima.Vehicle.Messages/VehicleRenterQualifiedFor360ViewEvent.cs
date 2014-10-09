using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleRenterQualifiedFor360ViewEvent : IEvent
    {
        [UltimaEncryption]
        public string Id { get; set; }
    }
}
