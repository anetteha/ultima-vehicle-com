using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleStatusCheckedEvent : IEvent
    {
        [UltimaEncryption]
        public string Id { get; set; }
        public bool Vip { get; set; }
        public bool BrakesNeedService { get; set; }
        public int MilesSinceLastStatusCheck { get; set; }
    }
}
