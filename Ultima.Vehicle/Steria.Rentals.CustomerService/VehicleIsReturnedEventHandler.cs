using System;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Steria.Rentals.CustomerService
{
    public class VehicleIsReturnedEventHandler : IHandleMessages<VehicleIsReturnedEvent>
    {
        public void Handle(VehicleIsReturnedEvent message)
        {
            Console.WriteLine("EVENT FROM ULTIMA: Vehicle {0} is returned", message.Id);
        }
    }
}
