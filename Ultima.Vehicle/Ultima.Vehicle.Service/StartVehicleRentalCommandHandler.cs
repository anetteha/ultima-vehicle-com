using System;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Service
{
    public class StartVehicleRentalCommandHandler : IHandleMessages<StartVehicleRentalCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(StartVehicleRentalCommand message)
        {
            Console.WriteLine(@"Vehicle is rented with Id:{0}", message.Id);
        }
    }
}
