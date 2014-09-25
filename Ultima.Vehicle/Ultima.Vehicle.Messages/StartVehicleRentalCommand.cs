using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class StartVehicleRentalCommand : ICommand
    {
        /// <summary>
        /// Vehicle Identification Number
        /// </summary>
        public string Id { get; set; } 
    }
}
