using NServiceBus;

namespace Ultima.Vehicle.Messages
{
    public class StartVehicleRentalCommand : ICommand
    {
        /// <summary>
        /// Vehicle Identification Number
        /// </summary>
        [UltimaEncryption]
        public string Id { get; set; }

        public int InitialMilage { get; set; }
    }
}
