using System;
using NServiceBus;
using NServiceBus.Audit;
using NServiceBus.Saga;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Service
{
    public class RentAVehicleSaga : Saga<RentalData>,
        IAmStartedByMessages<StartVehicleRentalCommand>,
        IHandleMessages<EndVehicleRentalCommand>,
        IHandleMessages<VehicleStatusCheckedEvent>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<StartVehicleRentalCommand>(m => m.Id).ToSaga(s => s.BusinessId);
            ConfigureMapping<VehicleStatusCheckedEvent>(m => m.Id).ToSaga(s => s.BusinessId);
            ConfigureMapping<EndVehicleRentalCommand>(m => m.Id).ToSaga(s => s.BusinessId);
        }

        public void Handle(StartVehicleRentalCommand message)
        {
            Console.WriteLine(@"Vehicle is rented with Id:{0}", message.Id);
            Data.BusinessId = message.Id;
        }
        
        public void Handle(EndVehicleRentalCommand message)
        {
            MarkAsComplete();
            Console.WriteLine(@"Vehicle with Id is returned:{0}", message.Id);
        }

        public void Handle(VehicleStatusCheckedEvent message)
        {
            Data.Mileage = message.Mileage;
            Console.WriteLine(@"Vehicle with Id ({0} has mileage:{1}", message.Id, message.Mileage);
        }
    }
}
