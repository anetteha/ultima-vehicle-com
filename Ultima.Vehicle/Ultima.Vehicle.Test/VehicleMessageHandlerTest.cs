using Simple.Testing.ClientFramework;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Service.Test
{
    public class VehicleMessageHandlerTest
    {
        public Specification when_recieving_a_register_new_vehicle_message = new ActionSpecification<StartVehicleRentalCommandHandler>
        {
            //Before = ()=> ; //TODO check if vehicle exists
            On = () => new StartVehicleRentalCommandHandler(),
            When = messageHandler => messageHandler.Handle(new StartVehicleRentalCommand()),
            Expect = { }
            /*
             When = account => account.Deposit(new Money(50)),
           Expect =
               {
                   account => account.CurrentBalance == new Money(50),
                   account => account.Transactions.Count() == 1,
                   account => account.Transactions.First().Amount ==
new Money(50),
                   account => account.Transactions.First().Type ==
TransactionType.Deposit,
                   account => account.Transactions.First().Timestamp
== new DateTime(2011,1,1),
               },
           Finally = SystemTime.Clear
             */
        };
    }
}
