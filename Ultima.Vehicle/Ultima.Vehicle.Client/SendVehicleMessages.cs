using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Client
{
    public class SendVehicleMessages : IWantToRunWhenBusStartsAndStops
    {
        private readonly List<Guid> _vehicleIds = new List<Guid>();
        public IBus Bus { get; set; }
        private Random _random = new Random(1000);

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to Rent a vehicle.To exit, Ctrl + C");
            string input;
            while ((input = Console.ReadLine()) != null)
            {
                if (!_vehicleIds.Any())
                {
                    SendStartRental();
                }
                else if (string.IsNullOrEmpty(input)) continue;
                else
                {
                    var menuChoice = input.Split(" ".ToCharArray()).Where(x => x.Trim() != "").ToList();
                    if (menuChoice[0] != "1" && menuChoice.Count < 2)
                    {
                        Console.WriteLine("Select message type and vehicle #. (Ex. 3 3)");
                        continue;
                    }
                    switch (menuChoice[0].Trim())
                    {
                        case "1":
                            SendStartRental();
                            break;
                        case "2":
                            SendReturnVehicle(menuChoice[1]);
                            break;
                        case "3":
                            SendVehicleStatusCheck(menuChoice[1]);
                            break;
                        default:
                            Console.WriteLine("Invalid selection. Select message type and vehicle #. (Ex. 3 3)");
                            break;
                    }
                }

                PrintMenu();
            }
        }

        private void SendVehicleStatusCheck(string stringIndex)
        {
            var index = GetIndex(stringIndex);
            if (index == -1) return;
            VehicleStatusCheckedEvent driverType;
            do
            {
                driverType = GetDriverTypeFromUser(_vehicleIds[index].ToString());
                if (driverType != null)
                    Bus.Publish(driverType);
            } while (driverType != null);
        }

        private static VehicleStatusCheckedEvent GetDriverTypeFromUser(string id)
        {
            do
            {
                Console.WriteLine("============== Type of driver since last status check: ==============");
                Console.WriteLine("0: Back");
                Console.WriteLine("1: Sunday Driver");
                Console.WriteLine("2: Soccer Mom Late For Practice");
                Console.WriteLine("3: Race Driver");
                Console.WriteLine("4: Crazy Person");
                Console.WriteLine("5: Kjell Rusti");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        return null;
                    case "1":
                        return new VehicleStatusCheckedEvent { Id = id, BrakesNeedService = false, MilesSinceLastStatusCheck = 10 };
                    case "2":
                        return new VehicleStatusCheckedEvent { Id = id, BrakesNeedService = false, MilesSinceLastStatusCheck = 50 };
                    case "3":
                        return new VehicleStatusCheckedEvent { Id = id, BrakesNeedService = false, MilesSinceLastStatusCheck = 100 };
                    case "4":
                        return new VehicleStatusCheckedEvent { Id = id, BrakesNeedService = false, MilesSinceLastStatusCheck = 10000 };
                    case "5":
                        Console.WriteLine("COME ON, FOLKENS!!");
                        Console.ReadLine();
                        return new VehicleStatusCheckedEvent { Id = id, Vip = true, BrakesNeedService = false, MilesSinceLastStatusCheck = 666 };
                }
            } while (true);
        }

        private void SendReturnVehicle(string stringIndex)
        {
            var index = GetIndex(stringIndex);
            if (index == -1) return;

            Bus.Send("Ultima.Vehicle.Service", new EndVehicleRentalCommand { Id = _vehicleIds[index].ToString(), MileageWhenReturned = _random.Next(50000, 200000) });
            _vehicleIds.Remove(_vehicleIds[index]);
        }

        private int GetIndex(string stringIndex)
        {
            int index;
            int.TryParse(stringIndex, out index);
            if (index != 0 && index <= _vehicleIds.Count) return index - 1;

            Console.WriteLine("Vehicle number does not exist.");
            return -1;
        }

        private void PrintMenu()
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Number of Vehicles Rented: {0}", _vehicleIds.Count);
            Console.WriteLine("1: Rent new Vehicle");
            Console.WriteLine("2 #: Return Vehicle");
            Console.WriteLine("3 #: Send Status Update on Vehicle #");
        }

        private void SendStartRental()
        {
            var id = Guid.NewGuid();
            Bus.Send("Ultima.Vehicle.Service", new StartVehicleRentalCommand { Id = id.ToString(), InitialMilage = _random.Next(3000, 150000) });
            _vehicleIds.Add(id);
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Sendt a new StartVehicleRentalCommand message with id: {0}", id.ToString("N"));
        }

        public void Stop()
        {
        }
    }
}