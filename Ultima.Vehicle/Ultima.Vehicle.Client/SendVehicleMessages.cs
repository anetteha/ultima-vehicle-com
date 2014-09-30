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
                    if (menuChoice[0].Trim() == "1") SendStartRental();
                    if (menuChoice[0].Trim() == "2") SendReturnVehicle(menuChoice[1]);
                    if (menuChoice[0].Trim() == "3") SendVehicleStatusCheck(menuChoice[1]);
                }

                PrintMenu();
            }
        }

        private void SendVehicleStatusCheck(string stringIndex)
        {
            var index = GetIndex(stringIndex);
            if (index == -1) return;

            Bus.Publish(new VehicleStatusCheckedEvent { Id = _vehicleIds[index].ToString(), Mileage = 4000 });
        }

        private void SendReturnVehicle(string stringIndex)
        {
            var index = GetIndex(stringIndex);
            if (index == -1) return;

            Bus.Send("Ultima.Vehicle.Service", new EndVehicleRentalCommand { Id = _vehicleIds[index].ToString() });
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
            Console.WriteLine("3 #: Send Status Update on Vehicle -v:#");
        }

        private void SendStartRental()
        {
            var id = Guid.NewGuid();
            Bus.Send("Ultima.Vehicle.Service", new StartVehicleRentalCommand { Id = id.ToString() });
            _vehicleIds.Add(id);
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Sendt a new StartVehicleRentalCommand message with id: {0}", id.ToString("N"));
        }

        public void Stop()
        {
        }
    }
}