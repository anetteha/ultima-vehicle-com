using System;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Steria.Rentals.CustomerService
{
    public class VehicleIsRentedEventHandler : IHandleMessages<VehicleIsRentedEvent>
    {
        public void Handle(VehicleIsRentedEvent message)
        {
            Console.WriteLine("EVENT FROM ULTIMA: Vehicle {0} is RENTED!", message.Id);
        }
    }
}
