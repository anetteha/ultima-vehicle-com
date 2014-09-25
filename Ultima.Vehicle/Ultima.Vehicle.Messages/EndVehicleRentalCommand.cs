using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class EndVehicleRentalCommand : ICommand
    {
        public string Id { get; set; }
    }
}