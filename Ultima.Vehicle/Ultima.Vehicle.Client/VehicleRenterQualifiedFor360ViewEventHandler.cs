using System;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Client
{
    public class VehicleRenterQualifiedFor360ViewEventHandler : IHandleMessages<VehicleRenterQualifiedFor360ViewEvent>
    {
        public void Handle(VehicleRenterQualifiedFor360ViewEvent message)
        {
            Console.WriteLine("Vehicle {0} qualified for 360 View", message.Id);
        }
    }
}
