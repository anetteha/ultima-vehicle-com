using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;
using Ultima.Vehicle.Messages;

namespace Ultima.Vehicle.Client
{
    public class SendVehicleMessages : IWantToRunWhenBusStartsAndStops
    {
        private readonly List<KeyValuePair<string, int>> _vehicles = new List<KeyValuePair<string, int>>();
        public IBus Bus { get; set; }
        private readonly Random _random = new Random(1000);

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to Rent a vehicle.To exit, Ctrl + C");
            string input;
            while ((input = Console.ReadLine()) != null)
            {
                if (!_vehicles.Any())
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
                driverType = GetDriverTypeFromUser(_vehicles[index]);
                if (driverType != null)
                    Bus.Publish(driverType);
            } while (driverType != null);
        }

        private VehicleStatusCheckedEvent GetDriverTypeFromUser(KeyValuePair<string, int> vehicle)
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
                        UpdateVehicleMileage(vehicle, 10);
                        return new VehicleStatusCheckedEvent { Id = vehicle.Key, BrakesNeedService = false, MilesSinceLastStatusCheck = 10 };
                    case "2":
                        UpdateVehicleMileage(vehicle, 50);
                        return new VehicleStatusCheckedEvent { Id = vehicle.Key, BrakesNeedService = false, MilesSinceLastStatusCheck = 50 };
                    case "3":
                        UpdateVehicleMileage(vehicle, 100);
                        return new VehicleStatusCheckedEvent { Id = vehicle.Key, BrakesNeedService = false, MilesSinceLastStatusCheck = 100 };
                    case "4":
                        UpdateVehicleMileage(vehicle, 10000);
                        return new VehicleStatusCheckedEvent { Id = vehicle.Key, BrakesNeedService = false, MilesSinceLastStatusCheck = 10000 };
                    case "5":
                        UpdateVehicleMileage(vehicle, 666);
                        Console.WriteLine("COME ON, FOLKENS!!");
                        Console.ReadLine();
                        return new VehicleStatusCheckedEvent { Id = vehicle.Key, Vip = true, BrakesNeedService = false, MilesSinceLastStatusCheck = 666 };
                }
            } while (true);
        }

        private void UpdateVehicleMileage(KeyValuePair<string, int> vehicle, int mileageToAdd)
        {
            var indexOf = _vehicles.IndexOf(vehicle);
            _vehicles[indexOf] = new KeyValuePair<string, int>(vehicle.Key, vehicle.Value + mileageToAdd);
        }

        private void SendReturnVehicle(string stringIndex)
        {
            var index = GetIndex(stringIndex);
            if (index == -1) return;

            Bus.Send("Ultima.Vehicle.Service", new EndVehicleRentalCommand { Id = _vehicles[index].Key, MileageWhenReturned = _vehicles[index].Value + _random.Next(10, 1000) });
            _vehicles.Remove(_vehicles[index]);
        }

        private int GetIndex(string stringIndex)
        {
            int index;
            int.TryParse(stringIndex, out index);
            if (index != 0 && index <= _vehicles.Count) return index - 1;

            Console.WriteLine("Vehicle number does not exist.");
            return -1;
        }

        private void PrintMenu()
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Number of Vehicles Rented: {0}", _vehicles.Count);
            Console.WriteLine("1: Rent new Vehicle");
            Console.WriteLine("2 #: Return Vehicle");
            Console.WriteLine("3 #: Send Status Update on Vehicle #");
        }

        private void SendStartRental()
        {
            var id = GetNextRentalId();
            var startMileage = _random.Next(3000, 150000);
            Bus.Send("Ultima.Vehicle.Service", new StartVehicleRentalCommand { Id = id, InitialMilage = startMileage });
            _vehicles.Add(new KeyValuePair<string, int>(id, startMileage));
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Sendt a new StartVehicleRentalCommand message with vehicle: {0}", id);
        }

        private string GetNextRentalId()
        {
            return "RENTAL_VIN_" + (_vehicles.Count + 1);
        }

        public void Stop()
        {
        }
    }
}