using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class VehicleStatusCheckedEvent : IEvent
    {
        public string Id { get; set; }
        public int Mileage { get; set; }
        public bool Vip { get; set; }
        public bool BrakesNeedService { get; set; }
        public int MilesSinceLastStatusCheck { get; set; }
    }
}
