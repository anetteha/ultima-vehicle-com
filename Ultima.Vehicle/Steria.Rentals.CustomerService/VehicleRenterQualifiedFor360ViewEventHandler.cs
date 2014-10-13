using System;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Steria.Rentals.CustomerService
{
    public class VehicleRenterQualifiedFor360ViewEventHandler : IHandleMessages<VehicleRenterQualifiedFor360ViewEvent>
    {
        public void Handle(VehicleRenterQualifiedFor360ViewEvent message)
        {
            Console.WriteLine("EVENT FROM ULTIMA: Vehicle {0} qualified for 360 View", message.Id);
        }
    }
}
