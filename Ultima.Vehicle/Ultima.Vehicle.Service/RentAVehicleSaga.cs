using System;
using System.Configuration;
using NServiceBus;
using NServiceBus.Saga;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Service
{
    public class RentAVehicleSaga : Saga<RentalData>,
        IAmStartedByMessages<StartVehicleRentalCommand>,
        IHandleMessages<EndVehicleRentalCommand>,
        IHandleMessages<VehicleStatusCheckedEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RentalData> mapper)
        {
            mapper.ConfigureMapping<StartVehicleRentalCommand>(m => m.Id).ToSaga(s => s.BusinessId);
            mapper.ConfigureMapping<VehicleStatusCheckedEvent>(m => m.Id).ToSaga(s => s.BusinessId);
            mapper.ConfigureMapping<EndVehicleRentalCommand>(m => m.Id).ToSaga(s => s.BusinessId);
        }

        public void Handle(StartVehicleRentalCommand message)
        {
            Console.WriteLine(@"Vehicle is rented with Id:{0}. Initial mileage: {1}", message.Id, message.InitialMilage);
            Data.BusinessId = message.Id;
            Data.Mileage = message.InitialMilage;
        }

        public void Handle(EndVehicleRentalCommand message)
        {
            Data.MileageWhenReturned = message.MileageWhenReturned;
            MarkAsComplete();
            Console.WriteLine(@"Vehicle with Id is returned:{0}. Mileage: {1}", message.Id, message.MileageWhenReturned);
        }

        public void Handle(VehicleStatusCheckedEvent message)
        {
            Data.Mileage += message.MilesSinceLastStatusCheck;
            Data.Vip = message.Vip;
            Data.BrakesNeedService = message.BrakesNeedService;
            Data.MilesSinceLastStatusCheck = message.MilesSinceLastStatusCheck;
            Data.TotalMilageUsedWhileRented += message.MilesSinceLastStatusCheck;
            Data.NumberOfStatusChecks++;

            var da360ViewLimit = int.Parse(ConfigurationManager.AppSettings["360ViewLimitInMiles"]);
            if (message.Vip || (Data.NumberOfStatusChecks >= 3 && Data.TotalMilageUsedWhileRented <= da360ViewLimit))
            {
                Console.WriteLine("Vehicle with Id {0} has qualified for 360View! :)", message.Id);
                Bus.Publish(new VehicleRenterQualifiedFor360ViewEvent { Id = message.Id });
            }
                
            Console.WriteLine("Vehicle with Id {0} has mileage:{1}", message.Id, Data.Mileage);
            Console.WriteLine("Vehicle with Id {0} has mileage since last status check:{1}", message.Id, message.MilesSinceLastStatusCheck);
        }

    }
}
