using System;

namespace Ultima.Vehicle.Domain
{
    public class Vehicle
    {
        public Vehicle(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
